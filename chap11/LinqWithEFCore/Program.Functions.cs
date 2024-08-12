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
}