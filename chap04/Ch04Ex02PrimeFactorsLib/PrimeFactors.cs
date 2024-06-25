namespace Ch04Ex02PrimeFactorsLib;

public class PrimeFactors
{
    /// <summary>
    /// Returns a string of a Prime Factors of a given int n
    /// </summary>
    /// <param name="n"></param>
    /// <returns>
    /// PrimeFactorsArray(n).toString()
    /// </returns>
    public static string Primes(int n)
    {
        return ConvertToString(PrimeFactorsArray(n));
    }
    
    /// <summary>
    /// Returns true if n is a prime number
    /// </summary>
    /// <param name="n"></param>
    /// <returns>
    /// true if n is a prime number else false
    /// </returns>
    public static bool IsPrime(int n)
    {
        int x = 0;
        
        if (IsEven(n))
        {
            x = (int)n / 2;
        }

        if (!IsEven(n))
        {
            x = (int) (n - 1) / 2;
        }

        for (int i = 2; i <= x; i++)
        {
            if (n == 2)
            {
                return true;
            }
            else if (n % i == 0)
            {
                return false;
            }
        }
        return true;
    }
    /// <summary>
    /// Returns true if n is an even number
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static bool IsEven(int n)
    {
        if (n % 2 == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    /// <summary>
    /// Returns an array of prime factors of n
    /// </summary>
    /// <param name="n"></param>
    /// <returns>
    /// int[ ] of prime factors
    /// </returns>
    public static int[] PrimeFactorsArray(int n)
    {
        
        List<int> list = new List<int>();
        for (int i = 2; i <= n; i++)
        {
            if (n % i == 0 && IsPrime(i))
            {
                list.Add(i);
            }

        }

        int[] factors = list.ToArray();
        return factors;
    }
    /// <summary>
    /// Returns a string of a given int[ ]
    /// </summary>
    /// <param name="n"></param>
    /// <returns>
    /// string of int[ ]
    /// </returns>
    public static string ConvertToString(int[] n)
    {
        string s = "";
        for (int i = 0; i < n.Length; i++)
        {
            if(i == n.Length - 1)
            {
                s += n[i];
            }
            else
            {
                s += n[i] + " ";
            }
        }

        return s;
    }
}
