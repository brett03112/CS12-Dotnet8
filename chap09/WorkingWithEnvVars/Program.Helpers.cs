

partial class Program
{
    /// <summary>
    /// Write a section title to the console changing color as we go.
    /// </summary>
    /// <param name="title"></param>
    private static void SectionTitle(string title)
    {
        ConsoleColor previousColor = ForegroundColor;
        ForegroundColor = ConsoleColor.DarkYellow;
        WriteLine($"***{title}***");
        ForegroundColor = previousColor;
    }

    /// <summary>
    /// Convert a dictionary to a table and write it to the console
    /// </summary>
    /// <param name="dictionary"></param>
    private static void DisctionaryToTable(IDictionary dictionary)
    {
        Table table = new();
        table.AddColumn("Key");
        table.AddColumn("Value");

        foreach (string key in dictionary.Keys)
        {
            table.AddRow(key, dictionary[key]!.ToString()!);
        }

        AnsiConsole.Write(table);
    }
}