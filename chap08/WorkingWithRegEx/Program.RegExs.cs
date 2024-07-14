using System.Text.RegularExpressions; // To use [GeneratedRegex]

partial class Program
{
    /// <summary>
    /// Regex to match digits only in text 
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(DigitsOnlyText, RegexOptions.IgnoreCase)]
    private static partial Regex DigitsOnly();

    /// <summary>
    /// Regex to match comma separated text 
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(CommaSeparatorText, RegexOptions.IgnoreCase)]
    private static partial Regex CommaSeparator();
}