#region Error prone code wrapped in a Try block
WriteLine("Before parsing");
Write("What is your age? ");
string? input = ReadLine();
if (input is null)
{
    WriteLine("you did not enter a value so the app will exit");
    return;
}
try
{
    int age =  int.Parse(input!);  // ! null-forgiving operator
    
    WriteLine($"You are {age} years old.");
}
catch (OverflowException) // catch a specific exception
{
    WriteLine("The age you entered was either too big or too small for an Int32.");
}
catch (FormatException) // catch a specific exception
{
    WriteLine("You did not enter a valid number.");
}
catch (Exception ex) // catch all exceptions
{
    WriteLine($"{ex.GetType()} says {ex.Message}");
}

WriteLine("After parsing");

#endregion

#region Catching with filters

Write("Enter an amount: ") ;
string? amount = ReadLine();

if (string.IsNullOrEmpty(amount)) return;

try
{
    decimal amountValue = decimal.Parse(amount);
    WriteLine($"Amount formatted as currency: {amountValue:C}");
}
catch (FormatException) when (amount.Contains('$'))
{
    WriteLine("Amount must be in the format 0.00");
}
catch (FormatException)
{
    WriteLine("Amount must be a number");
}
#endregion

#region Throwing overflow exceptions with the checked keyword

// The checked keyword is used to throw an exception for overflow exceptions rather than allowing the program to continue.
checked
{
    try
    {
        int x = int.MaxValue - 1;
        WriteLine($"Initial value: {x}");
        x++;
        WriteLine($"After incrementing: {x}");
        x++;
        WriteLine($"After incrementing: {x}");
        x++;
        WriteLine($"After incrementing: {x}");
    }
    catch (OverflowException)
    {
        WriteLine("An overflow occurred, but I caught it.");
    }
}
#endregion