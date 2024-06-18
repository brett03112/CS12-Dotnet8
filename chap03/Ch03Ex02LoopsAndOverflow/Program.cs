/*
    The checked keyword is used to throw an exception for overflow exceptions
        rather than allowing the program to continue.
*/
checked
{
    try
    {
        int max = 500;
        for (byte i = 0; i < max; i++)
        {
        WriteLine(i);
        }
    }
    catch (OverflowException ex)
    {
        WriteLine($"{ex.GetType()} says {ex.Message}");
    }
}