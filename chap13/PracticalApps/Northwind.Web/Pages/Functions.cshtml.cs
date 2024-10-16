using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Northwind.Web.Pages;
/*
Class Definition:

This is a C# class named FunctionsModel that inherits from PageModel. It is a part of 
an ASP.NET Core Razor Pages application.

Class Methods:

--OnGet: Handles HTTP GET requests and populates the model's properties based 
    on query string parameters. It performs the following operations:

--Parses timesTableNumberInput and assigns it to the TimesTableNumberInput property.

--Parses calculateTaxAmountInput and calculateTaxRegionCodeInput and calculates the 
    tax using the CalculateTax method.

--Parses factorialNumberInput and calculates the factorial using the Factorial method.

--Parses fibonacciNumberInput and calculates the Fibonacci number using the FibImperative method.

--CalculateTax: Calculates the tax amount based on the provided amount and twoLetterRegionCode. 
    It uses a switch statement to determine the tax rate based on the region code.

--Factorial: Calculates the factorial of a given number. It throws an ArgumentException if the 
    input is negative.

--FibImperative: Calculates the Fibonacci number for a given term using an imperative approach.

--Note that the OnGet method is the entry point for handling HTTP GET requests, and 
    it calls the other methods to perform the necessary calculations.
*/
public class FunctionsModel : PageModel
{
    public int? TimesTableNumberInput { get; set; }

    public decimal? Amount { get; set; }
    public string? RegionCode { get; set; }
    public decimal? TaxToPay { get; set; }

    public int? FactorialNumber { get; set; }
    public int? FactorialResult { get; set; }
    public Exception? FactorialException { get; set; }

    public int? FibonacciNumber { get; set; }
    public int? FibonacciResult { get; set; }

        /// <summary>
        /// Handles HTTP GET requests and populates the model's properties based on query string parameters.
        /// </summary>
        /// <remarks>
        /// It performs the following operations:
        /// 
        /// --Parses timesTableNumberInput and assigns it to the TimesTableNumberInput property.
        /// 
        /// --Parses calculateTaxAmountInput and calculateTaxRegionCodeInput and calculates the tax using the CalculateTax method.
        /// 
        /// --Parses factorialNumberInput and calculates the factorial using the Factorial method.
        /// 
        /// --Parses fibonacciNumberInput and calculates the Fibonacci number using the FibImperative method.
        /// </remarks>
    public void OnGet()
    {
        // Times Table
        if (int.TryParse(HttpContext.Request.Query["timesTableNumberInput"], out int i))
        {
            TimesTableNumberInput = i;
        }

        // Calculate Tax
        if (decimal.TryParse(HttpContext.Request.Query["calculateTaxAmountInput"], out decimal amount))
        {
            Amount = amount;
            RegionCode = HttpContext.Request.Query["calculateTaxRegionCodeInput"];
            TaxToPay = CalculateTax(amount: amount, twoLetterRegionCode: RegionCode);
        }

        // Factorial
        if (int.TryParse(HttpContext.Request.Query["factorialNumberInput"], out int fact))
        {
            FactorialNumber = fact;
            try
            {
                FactorialResult = Factorial(fact);
            }
            catch (Exception ex)
            {
                FactorialException = ex;
            }
        }

        // Fibonacci
        if (int.TryParse(HttpContext.Request.Query["fibonacciNumberInput"], out int fib))
        {
            FibonacciNumber = fib;
            FibonacciResult = FibImperative(term: fib);
        }
    }

    /// <summary>
    /// Calculates the tax amount based on the provided amount and twoLetterRegionCode.
    /// </summary>
    /// <param name="amount">The amount to calculate the tax for.</param>
    /// <param name="twoLetterRegionCode">The two-letter region code to base the tax calculation on.</param>
    /// <returns>The tax amount.</returns>
    /// <remarks>
    /// For the US states, the following rates are used:
    /// 
    /// --AK, MT, OR: 0%
    /// --ND, WI, ME, VA: 5%
    /// --CA: 8.25%
    /// --all other states: 6%
    /// 
    /// For regions outside of the US, the following rates are used:
    /// 
    /// --CH (Switzerland): 8%
    /// --DK (Denmark) and NO (Norway): 25%
    /// --GB (United Kingdom) and FR (France): 20%
    /// --HU (Hungary): 27%
    /// </remarks>
    static decimal CalculateTax(
        decimal amount, string? twoLetterRegionCode)
    {
        decimal rate = 0.0M;

        // since we are matching string values a switch
        // statement is easier than a switch expression

        switch (twoLetterRegionCode)
        {
            case "CH": // Switzerland 
                rate = 0.08M;
                break;
            case "DK": // Denmark 
            case "NO": // Norway
                rate = 0.25M;
                break;
            case "GB": // United Kingdom
            case "FR": // France
                rate = 0.2M;
                break;
            case "HU": // Hungary
                rate = 0.27M;
                break;
            case "OR": // Oregon
            case "AK": // Alaska
            case "MT": // Montana
                rate = 0.0M;
                break;
            case "ND": // North Dakota
            case "WI": // Wisconsin
            case "ME": // Maine
            case "VA": // Virginia
                rate = 0.05M;
                break;
            case "CA": // California
                rate = 0.0825M;
                break;
            default: // most US states 
                rate = 0.06M;
                break;
        }

        return amount * rate;
    }

    /// <summary>
    /// Calculates the factorial of a given number.
    /// </summary>
    /// <param name="number">The number to calculate the factorial of.</param>
    /// <returns>The factorial of the given number.</returns>
    /// <exception cref="ArgumentException">The factorial function is defined for non-negative integers only.</exception>
    static int Factorial(int number)
    {
        if (number < 0)
        {
            throw new ArgumentException(
                message: "The factorial function is defined for non-negative integers only.",
                paramName: "number");
        }
        else if (number == 0)
        {
            return 1;
        }
        else
        {
            checked // for overflow
            {
                return number * Factorial(number - 1);
            }
        }
    }

    /// <summary>
    /// Calculates the Fibonacci number for a given term using an imperative approach.
    /// </summary>
    /// <param name="term">The term of the Fibonacci number to calculate.</param>
    /// <returns>The Fibonacci number for the given term.</returns>
    /// <exception cref="ArgumentOutOfRangeException">The term must be a positive integer.</exception>
    static int FibImperative(int term)
    {
        if (term == 1)
        {
            return 0;
        }
        else if (term == 2)
        {
            return 1;
        }
        else
        {
            return FibImperative(term - 1) + FibImperative(term - 2);
        }
    }

}