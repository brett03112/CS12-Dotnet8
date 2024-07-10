using System.Text.RegularExpressions; // To use the Regex class

namespace Packt.Shared;

public static class StringExtensions
{
    /// <summary>
    /// Extension method to validate an email using a regular expression
    /// </summary>
    /// <param name="input"></param>
    /// <returns>True if the email is valid. False if not.</returns>
    public static bool IsValidEmail(this string input)
    {
        // Use a simple regular expression to validate the email
        return Regex.IsMatch(input, @"[a-zA-Z0-9_.-]+@[a-zA-Z0-9]+\.[a-zA-Z0-9-.]+$");
    }
}