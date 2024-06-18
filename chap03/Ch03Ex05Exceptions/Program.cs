/*
    An example of throwing and catching an exception
*/ 

Write("Enter a number between 0 and 255: ");
string? x = ReadLine()!;

Write("Enter a number between 0 and 255: ");
string? y = ReadLine()!;



try
{
    byte a = byte.Parse(x);
    byte b = byte.Parse(y);
    int ans = a / b;
    int rem = a % b;
    
    WriteLine($"{a} divided by {b} is {ans} with a remainder of {rem}.");
}
catch (DivideByZeroException ex)
{
    WriteLine($"{ex.GetType()} says {ex.Message}");
    WriteLine("Cannot divide by zero.");
}

catch (OverflowException ex)
{
    WriteLine($"{ex.GetType()} says {ex.Message}");
    WriteLine("The number was too big or too small.");
}
catch (Exception ex)
{
    WriteLine($"{ex.GetType()} says {ex.Message}");
}