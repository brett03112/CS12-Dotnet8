using System.Globalization;

partial class Program
{
    static void TimesTable(byte number, byte size = 12)
    {
        WriteLine($"This is the {number} times table with {size} rows:");
        WriteLine();
        for (int row = 1; row <= size; row++)
        {
            WriteLine($"{row} x {number} = {row * number}");
        }
        WriteLine();
    }

    static decimal CalculateTax(decimal amount, string twoLetterRegionCode)
    {
        decimal rate = twoLetterRegionCode switch
        {
            "CH" => 0.08M, // Switzerland
            "DK" or "NO" => 0.25M, // Denmark and Norway
            "GB" or "FR" => 0.2M, // United Kingdom and France
            "HU" => 0.27M, // Hungary
            "OR" or "AK" or "MT" => 0.0M, //Oregon, Alaska, and Montana
            "ND" or "WI" or "ME" or "VA" => 0.05M, // North Dakota, Wisconsin, Maine, and Virginia
            "CA" => 0.13M, // California
            _ => 0.06M, // All other countries
        };
        return amount * rate;
    }
    // This function enables UTF-8 encoding for the console output for some special symbols like the Euro symbol
    // This function also controls the current culture used to format dates, times, and currency amounts
    static void ConfigureConsole(string culture = "en-US", bool useComputerCulture = true)
    {
        // To enable Unicode characters like the Euro symbol in the console
        OutputEncoding = System.Text.Encoding.UTF8;

        if (useComputerCulture)
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo(culture);
        }
        WriteLine($"CurrentCulture: {CultureInfo.CurrentCulture.DisplayName}");
    }


    
    /// <summary>
    /// Pass a 32-bit unsigned integer and it will be converted into its ordinal equivalent
    /// </summary>
    /// <param name="number">Number as a cardinal value e.g. 1,2,3, and so on.</param>
    /// <returns>Number as a ordinal value e.g. 1st, 2nd, 3rd, and so on.</returns>
    static string CardinalToOrdinal(uint number)
    {
        uint lastTwoDigits = number % 100;

        switch (lastTwoDigits)
        {
            case 11:
            case 12:
            case 13:
                return $"{number}th";
            default:
                uint lastDigit = number % 10;
                string suffix = lastDigit switch
                {
                    1 => "st",
                    2 => "nd",
                    3 => "rd",
                    _ => "th"
                };
                return $"{number:N0}{suffix}";
        }
    }
    /// <summary>
    /// Demonstrate the use of the CardinalToOrdinal function
    /// </summary>
    /// <returns>Ordinal numbers from 1 to 1500</returns>
    static void RunCardinalToOrdinal()
    {
        for (uint number = 1; number <= 1500; number++)
        {
            Write($"{CardinalToOrdinal(number)}  ");
            if (number % 10 == 0)
            {
                WriteLine();
            }
        }
        WriteLine();
    }
        /// <summary>
        /// Demonstrate the use of the Factorial function
        /// </summary>
        /// <param name="number">Number is a positive integer</param>
        /// <returns>The factorial of the number so long as the number is positive and less than 2147483647</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        static int Factorial(int number)
    {
        if (number < 0)
        {
            throw new ArgumentOutOfRangeException(message: $"The factorial is not defined for negative numbers. {number} is not a positive number.",
                paramName: nameof(number));
        }
        else if (number == 0)
        {
            return 1;
        }
        else
        checked // check for overflow
        {
            return number * Factorial(number - 1);
        }
        
    }
    /// <summary>
    /// Demonstrate the use of the Factorial function
    /// </summary>
    /// <returns>Displays the factorial of numbers from -2 to 15
    /// The factorial is not defined for negative numbers.
    /// The factorial is too big for a 32-bit integer (int beyond 13!).
    /// </returns>
    static void RunFactorial()
    {
        for (int i = -2; i <= 15; i++)
        {
            try
            {
                WriteLine($"{i}! = {Factorial(i):N0}");
            }
            catch (OverflowException)
            {
                WriteLine($"{i}! is too big for a 32-bit integer.");
            }
            catch (Exception ex)
            {
                WriteLine($"{i}! throws {ex:GetType()}: {ex.Message}");
            }
        }
    }

}
