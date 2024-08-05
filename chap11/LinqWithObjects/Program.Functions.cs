
partial class Program
{
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
        var query = names.Where(name => name.Length > 4);

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
}