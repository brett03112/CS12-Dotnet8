/*
    Write a program that prints the numbers from 1 to 100. But for multiples of 3 print "Fizz".
    For multiples of 5 print "Buzz". For multiples of both 3 and 5 print "FizzBuzz".
*/
int count = 0;
for (int i = 1; i <= 100; i++)
{
    if (i % 3 == 0 && i % 5 == 0)
    {
        Write("FizzBuzz ");
    }
    else if (i % 3 == 0)
    {
        Write("Fizz ");
    }
    else if (i % 5 == 0)
    {
        Write("Buzz ");
    }
    else
    {
        Write($"{i} ");
    }
    count++;
    if (count % 10 == 0)
    {
        WriteLine();
        count = 0;
    }
}