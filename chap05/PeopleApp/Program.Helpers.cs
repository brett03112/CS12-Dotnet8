using System.Globalization;

partial class Program
{
    /// <summary>
    /// Sets the current culture to "en-US" or the specified culture.
    /// </summary>
    /// <param name="culuture"></param>
    /// <param name="useComputerCulture"></param>
    /// <param name="showCulture"></param>
    private static void ConfigureConsole(string culture = "en-US", bool useComputerCulture = false, bool showCulture = true)
    {
        OutputEncoding = System.Text.Encoding.UTF8;

        if (!useComputerCulture)
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo(culture);
        }

        if (showCulture)
        {
            WriteLine($"Current culture: {CultureInfo.CurrentCulture.DisplayName}");
        }
    }
}