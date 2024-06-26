using System.Globalization;

partial class Program
{
    private static void ConfigureConsole(string culuture = "en-US", bool useComputerCulture = false, bool showCulture = true)
    {
        OutputEncoding = System.Text.Encoding.UTF8;

        if (!useComputerCulture)
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo(culuture);
        }

        if (showCulture)
        {
            WriteLine($"Current culture: {CultureInfo.CurrentCulture.DisplayName}");
        }
    }
}