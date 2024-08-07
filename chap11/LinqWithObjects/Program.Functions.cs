
partial class Program
{
    /// <summary>
    /// Changes values in the array or list as we search and enumerate over them
    /// </summary>
    /// <param name="names"></param> 
    private static void DeferredExecution(string[] names)
    {
        SectionTitle("Deferred execution");

        // Question: Which names end with an M?
        // (using a LINQ extension method)

        var query1 = names.Where(name => name.EndsWith("m"));

        // Question: Which names end with an M?
        // (using a LINQ query expression)
        var query2 = from name in names where name.EndsWith("m") select name;

        // Answer returned as an array of strings containing Pam and Jim
        string[] result1 = query1.ToArray();

        // Answer returned as a list of strings containing Pam and Jim
        List<string> result2 = query2.ToList();

        // Answer returned as we enumerate over the results
        foreach (string name in query1)
        {
            WriteLine(name); // outputs Pam
            names[2] = "Jimmy"; // changes Jim to Jimmy
            // On the second iteration, Jimmy does not end with an "m"
            // so it does not get output
        }
    }
    /// <summary>
    /// Fiters an array or list using the Where method
    /// </summary>
    /// <param name="names"></param>
    private static void FilteringUsingWhere(string[] names)
    {
        SectionTitle("Filtering entities using Where");

        // Func<string, bool> delegate tells us that for each string variable
        // passed to the method, the method must return a boolean value.
        // If the method returns true, the string will be included in the
        // results and if the method returns false, the string will not be
        // included in the results.
        //var query = names.Where(
         //   new Func<string, bool> (NameLongerThanFour));
        
        // The compiler creates the delegate for us
        //var query = names.Where(NameLongerThanFour);

        // Using a lambda expression instead of a names method
        /*
        Note that the syntax for a lambda expression includes all the important parts of the
        NameLongerThanFour method, but nothing more. A lambda expression only needs to define
        the following:

            • The names of input parameters: name
            • A return value expression: name.Length > 4
        */
        IOrderedEnumerable<string> query = names
            .Where(name => name.Length > 4)
            .OrderBy(name => name.Length) // will print shortest names first
            .ThenBy(name => name); // sort names by length, then alphabetically 

        foreach (string name in query)
        {
            WriteLine(name);
        }
    }

    static bool NameLongerThanFour(string name)
    {
        // returns true for a name longer than four characters
        return name.Length > 4;
    }
    /// <summary>
    /// Filters by type of exception
    /// </summary>
    static void FilteringByType()
    {
        SectionTitle ("Filtering by type");

        List<Exception> exceptions = new()
        {
            new ArgumentException(), new SystemException(),
            new IndexOutOfRangeException(), new InvalidOperationException(),
            new NullReferenceException(), new InvalidCastException(),
            new OverflowException(), new DivideByZeroException(),
            new ApplicationException()
        };

        IEnumerable<ArithmeticException> arithmeticExceptionsQuery =
            exceptions.OfType<ArithmeticException>();

        foreach (ArithmeticException exception in arithmeticExceptionsQuery)
        {
            WriteLine(exception);
        }    
    }

    /// <summary>
    /// Writes a description and a list of names
    /// </summary>
    /// <param name="cohort"></param>
    /// <param name="description"></param> 
    static void Output(IEnumerable<string> cohort, string description = "")
    {
        if (!string.IsNullOrEmpty(description))
        {
            WriteLine(description);
        }
        Write(" ");
        WriteLine(string.Join(", ", cohort.ToArray()));
        WriteLine();
    }
    /// <summary>
    /// Executes a series of operations on three cohorts to demonstrate set operations.
    /// Set operations: distinct, distinct by name.substring(0, 2), union, concat, intersect, except,
    /// and zip 
    /// </summary>
    static void WorkingWithSets()
    {
        string[] cohort1 =
            { "Rachel", "Gareth", "Jonathan", "George" };
            
        string[] cohort2 = 
            { "Jack", "Stephen", "Daniel", "Jack", "Jared" };

        string[] cohort3 =
            { "Declan", "Jack", "Jack", "Jasmine", "Conor" };

        SectionTitle("The cohorts");

        Output(cohort1, "Cohort 1");
        Output(cohort2, "Cohort 2");
        Output(cohort3, "Cohort 3");

        SectionTitle("Set operations");

        Output(cohort2.Distinct(), "cohort2.Distinct()");
        Output(cohort2.DistinctBy(name => name.Substring(0,2)), "cohort2.DistinctBy(name => name.Substring(0, 2))");
        Output(cohort2.Union(cohort3), "cohort2.Union(cohort3)");
        Output(cohort2.Concat(cohort3), "cohort2.Concat(cohort3)");
        Output(cohort2.Intersect(cohort3), "cohort2.Intersect(cohort3)"); 
        Output(cohort2.Except(cohort3), "cohort2.Except(cohort3)");
        Output(cohort1.Zip(cohort2, (c1, c2) => $"{c1} matched with {c2}"),
            "cohort1.Zip(cohort2)");        
    }
}