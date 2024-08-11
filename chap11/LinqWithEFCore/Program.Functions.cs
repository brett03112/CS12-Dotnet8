using Northwind.EntityModels; // To use NorthwindDb, Category, Product
using Microsoft.EntityFrameworkCore; // To use DbSet<T>

partial class Program
{
    private static void FilterAndSort()
    {
        SectionTitle("Filter and sort");

        using NorthwindDb db = new();
        DbSet<Product> allProducts = db.Products;

        IQueryable<Product> filteredProducts = 
            allProducts.Where(product => product.UnitPrice< 10M);

        IOrderedQueryable<Product> sortedAndFilteredProducts = 
            filteredProducts.OrderByDescending(product => product.UnitPrice);


        var projectedProducts = sortedAndFilteredProducts.Select(product => new // new anonymous type
            {
                product.ProductID,
                product.ProductName,
                product.UnitPrice
            });

        WriteLine("Products that cost less than $10:");
        WriteLine(projectedProducts.ToQueryString());

        foreach (var p in projectedProducts)
        {
            WriteLine("{0}: {1} costs {2:$#,##0.00}",
                p.ProductID, p.ProductName, p.UnitPrice);
        }
        WriteLine();

        /*
        • DbSet<T> implements IEnumerable<T>, so LINQ can be used to query and manipulate
            sequences of entities in models built for EF Core. (Actually, I should say TEntity instead of
            T, but the name of this generic type has no functional effect. The only requirement is that
            the type is a class. The name just indicates the class is expected to be an entity model.)

        • The sequences implement IQueryable<T> (or IOrderedQueryable<T> after a call to
            an ordering LINQ method) instead of IEnumerable<T> or IOrderedEnumerable<T>.
            This is an indication that we are using a LINQ provider that builds the query using
            expression trees. They represent code in a tree-like data structure and enable the creation of dynamic queries, which is useful for building LINQ queries for external data
            providers like SQLite.

        • The LINQ expression will be converted into another query language, such as SQL.
            Enumerating the query with foreach or calling a method such as ToArray will force
            the execution of the query and materialize the results.

        */    
    }

    /*
    • Join: This method has four parameters: the sequence that you want to join with, the property
        or properties on the left sequence to match on, the property or properties on the right sequence
        to match on, and a projection.

    • GroupJoin: This method has the same parameters, but it combines the matches into a group
        object with a Key property for the matching value and an IEnumerable<T> type for the multiple
        matches. 

    • ToLookup: This method creates a new data structure with the sequence grouped by a key.

    */

    private static void JoinCategoriesAndProducts()
    {
        SectionTitle("Join categories and products");

        using NorthwindDb db = new();

        // Join every product with its category to return 77 matches
        var queryJoin = db.Categories.Join(
            inner: db.Products,
            outerKeySelector: category => category.CategoryID,
            innerKeySelector: product => product.CategoryID,
            resultSelector: (c, p) => 
                new { c.CategoryName, p.ProductName, p.ProductID })
                .OrderBy(cp => cp.CategoryName); // Sort by CategoryName
        
        foreach (var p in queryJoin)
        {
            WriteLine($"{p.ProductID}: {p.ProductName} in {p.CategoryName}.");
        }
        /*
        In a join, there are two sequences, outer and inner. In the preceding example,
        categories is the outer sequence and products is the inner sequence.
        */
    }

    private static void GroupJoinCategoriesAndProducts()
    {
        SectionTitle("Group join categories and products");

        using NorthwindDb db = new();

        // Group all products by their category to return 8 matches

        var queryGroup = db.Categories.AsEnumerable().GroupJoin(
            inner: db.Products,
            outerKeySelector: category => category.CategoryID,
            innerKeySelector: product => product.CategoryID,
            resultSelector: (c, matchingProducts) => new
            {
                c.CategoryName,
                Products = matchingProducts.OrderBy(p => p.ProductName)
            });
        
        foreach (var c in queryGroup)
        {
            WriteLine($"{c.CategoryName} has {c.Products.Count()} products.");

            foreach (var product in c.Products)
            {
                WriteLine($"  {product.ProductName}");
            }
        }
        /*
        If we had not called the AsEnumerable method, then a runtime exception would have been
        thrown

        This is because not all LINQ extension methods can be converted from expression trees into
        some other query syntax like SQL. In these cases, we can convert from IQueryable<T> to
        IEnumerable<T> by calling the AsEnumerable method, which forces query processing to use
        LINQ to EF Core only to bring the data into the application, and then use LINQ to Objects to
        execute more complex processing in memory. But, often, this is less efficient.
        */
    }

    private static void ProductsLookup()
    {
        SectionTitle("Products lookup");

        using NorthwindDb db = new();

        // Join all products to their category to return 77 matches
        var productQuery = db.Categories.Join(
            inner: db.Products,
            outerKeySelector: category => category.CategoryID,
            innerKeySelector: product => product.CategoryID,
            resultSelector: (c, p) => new { c.CategoryName, Product = p });
        
        ILookup<string, Product> productLookup = productQuery.ToLookup(
            keySelector: cp => cp.CategoryName,
            elementSelector: cp => cp.Product);
        
        foreach (IGrouping<string, Product> group in productLookup)
        {
            // Key is Beverages, Condiments, and so on
            WriteLine($"{group.Key} has {group.Count()} products");

            foreach (Product product in group)
            {
                WriteLine($" {product.ProductName}");
            }
        }

        // We can look up the products by a category name
        Write("Enter a category name: ");
        string categoryName = ReadLine()!;
        WriteLine();
        WriteLine($"Products in {categoryName}: ");

        IEnumerable<Product> productsInCategory = productLookup[categoryName];
        foreach (Product product in productsInCategory)
        {
            WriteLine($" {product.ProductName}");
        }

        /*
        Selector parameters are lambda expressions that select sub-elements for different
        purposes. For example, ToLookup has a keySelector to select the part of each
        item that will be the key and an elementSelector to select the part of each item
        that will be the value.
        */
    }

    private static void AggregateProducts()
    {
        SectionTitle("Aggregate products");

        using NorthwindDb db = new();

        // Try to get an efficient count from EF Core EbSet<T>
        if (db.Products.TryGetNonEnumeratedCount(out int countDbSet))
        {
            WriteLine($"{"Product count from DbSet: ", -25} {countDbSet,10}");
        }
        else
        {
            WriteLine("Products DbSet does not have a Count property.");
        }

        // Try to get an efficient count from a List<T>
        List<Product> products = db.Products.ToList();

        if (products.TryGetNonEnumeratedCount(out int countList))
        {
            WriteLine($"{"Product count from list: ", -25} {countList,10}");
        }
        else
        {
            WriteLine("Products list does not have a Count property.");
        }

        WriteLine($"{"Product count: ", -25} {db.Products.Count(),10}");

        WriteLine($"{"Discontinued product count: ", -27} {db.Products.Count(product => product.Discontinued), 8}");

        WriteLine($"{"Highest product price: ", -25} {db.Products.Max(p => p.UnitPrice), 10:$#,##0.00}");

        WriteLine($"{"Sum of units in stock: ", -25} {db.Products.Sum(p => p.UnitsInStock), 10:N0}");

        WriteLine($"{"Sum of units on order: ", -25} {db.Products.Sum(p => p.UnitsOnOrder), 10:N0}");

        WriteLine($"{"Average price: ", -25} {db.Products.Average(p => p.UnitPrice), 10:$#,##0.00}");

        WriteLine($"{"Value of units in stock: ", -25} {db.Products.Sum(p => p.UnitPrice * p.UnitsInStock), 10:$#,##0.00}");


    }
    /*
    ********************What does this code do?************************************************
    IEnumerable<Task> tasks = Enumerable.Range(0, 2)
        .Select(_ => Task.Run(() => Console.WriteLine("*")));

    await Task.WhenAll(tasks);

    Console.WriteLine($"{tasks.Count()} stars!");
        ------------->>>>>>>>>>>>   **2 stars!
                                    **2 stars!**
                                    ****2 stars!
                                    <Something else> ------------->>>> the correct answer

    */
    /*
    Code                                Description
    ___________________________________________________________________________________________________

    Enumerable.Range(0, 2)              Returns a sequence of two integers, 0 and 1. In production code, you
                                        should add named parameters to make this clearer, as shown in the
                                        following code: Enumerable.Range(start: 0, count: 2).

    Select(_ => Task.                   Creates a task with its own thread for each of the two numbers. The _
    Run(...)                            parameter discards the number value. Each task outputs a star * to the console.

    await Task.                         Blocks the main thread until both of the two tasks have completed. So,
    WhenAll(tasks);                     at this point, we know that two stars ** have been output to the console.

    tasks.Count()                       For the LINQ Count() extension method to work in this scenario, it must
                                        enumerate the sequence.  THis triggers the two tasks to execute again!
                                        But we do not know when they will execute.  The value 2 is returned from the
                                        method call.

    Console.WriteLine($"... stars!");   The "2 stars!" are output to the console.                                                   
    */

    // a method to output to the console a table of products passed as an array
    private static void OutputTableOfProducts(Product[] products,
        int currentPage, int totalPages)
    {
        string line = new('-', count: 73);
        string lineHalf =  new('-', count: 30);

        WriteLine(line);

        WriteLine("{0,4} {1,-40} {2,12} {3, -15}", "ID", "Product Name",
            "Unit Price", "Discontinued");

        WriteLine(line);
        foreach (Product p in products)
        {
            WriteLine("{0,4} {1,-40} {2,12:C} {3, -15}",
                p.ProductID, p.ProductName, p.UnitPrice, p.Discontinued);
        }
        WriteLine("{0} Page {1} of {2} {3}", lineHalf, currentPage + 1,
                totalPages + 1, lineHalf);
    }

    // Add a method to create a LINQ query that creates a page of products,
    // outputs the SQL generated from it, and then passes the results as an array of 
    // products to the method that outputs the table of products
    private static void OutputPageOfProducts(IQueryable<Product> products,
        int pageSize, int currentPage, int totalPages)
    {
        // We must order data before skipping and taking to ensure 
        // the data is not randomly sorted in each page
        var pagingQuery = products.OrderBy(p => p.ProductID)
            .Skip(currentPage * pageSize).Take(pageSize);

        Clear(); 

        //SectionTitle(pagingQuery.ToQueryString());

        OutputTableOfProducts(pagingQuery.ToArray(), currentPage, totalPages);    
    }
    /*
    In Program.Functions.cs, add a method to loop while the user presses either the left or right
    arrow to page through the products in the database, showing one page at a time
    */
     private static void PagingProducts()
    {
        SectionTitle("Paging products");
        using NorthwindDb db = new();
        int pageSize = 10;
        int currentPage = 0;
        int productCount = db.Products.Count();
        int totalPages = productCount / pageSize;
        while (true) // Use break to escape this infinite loop.
        {
            OutputPageOfProducts(db.Products, pageSize, currentPage, totalPages);

            Write("Press <- to page back, press -> to page forward, any key to exit.");

            ConsoleKey key = ReadKey().Key;
            
            if (key == ConsoleKey.LeftArrow)
            {
                currentPage = currentPage == 0 ? totalPages : currentPage - 1;
            }
            else if (key == ConsoleKey.RightArrow)
            {
                currentPage = currentPage == totalPages ? 0 : currentPage + 1;
            }
            else
            {
                break;
            }

            WriteLine();
        }
    }
}