namespace Ch04Ex02PrimeFactorsLibUnitTests;

using Xunit;

using Ch04Ex02PrimeFactorsLib;

public class PrimeFactorsLibUnitTests
{
    /// <summary>
    /// Tests for IsEven
    /// </summary>
    [Fact]
    public void TestIsEven()
    {
        Assert.True(PrimeFactors.IsEven(2));
        Assert.False(PrimeFactors.IsEven(3));
        Assert.True(PrimeFactors.IsEven(4));
        Assert.False(PrimeFactors.IsEven(5));
        Assert.True(PrimeFactors.IsEven(6));
        Assert.False(PrimeFactors.IsEven(7));
        Assert.True(PrimeFactors.IsEven(8));
        Assert.False(PrimeFactors.IsEven(9));
        Assert.True(PrimeFactors.IsEven(10));
        Assert.False(PrimeFactors.IsEven(11));
        Assert.True(PrimeFactors.IsEven(12));
        Assert.False(PrimeFactors.IsEven(13));
        Assert.True(PrimeFactors.IsEven(14));
        Assert.False(PrimeFactors.IsEven(15));

    }
    /// <summary>
    /// Tests for IsPrime
    /// </summary>
    [Fact]
    public void TestIsPrime()
    {
        Assert.True(PrimeFactors.IsPrime(2));
        Assert.True(PrimeFactors.IsPrime(3));
        Assert.True(PrimeFactors.IsPrime(5));
        Assert.False(PrimeFactors.IsPrime(6));
        Assert.True(PrimeFactors.IsPrime(7));
        Assert.False(PrimeFactors.IsPrime(8));
        Assert.False(PrimeFactors.IsPrime(9));
        Assert.False(PrimeFactors.IsPrime(10));
        Assert.True(PrimeFactors.IsPrime(11));
        Assert.False(PrimeFactors.IsPrime(12));
        Assert.True(PrimeFactors.IsPrime(13));
        Assert.False(PrimeFactors.IsPrime(14));
        Assert.False(PrimeFactors.IsPrime(15));
        Assert.True(PrimeFactors.IsPrime(23));
        Assert.False(PrimeFactors.IsPrime(39));
    }
    /// <summary>
    /// Tests for PrimeFactorsArray
    /// </summary>
    [Fact]
    public void TestPrimeFactorsArray()
    {
        Assert.Equal(new int[] { 2, 3 }, PrimeFactors.PrimeFactorsArray(12));
        Assert.Equal(new int[] { 2, 3, 5 }, PrimeFactors.PrimeFactorsArray(30));
        Assert.Equal(new int[] { 2, 3 }, PrimeFactors.PrimeFactorsArray(54));
        Assert.Equal(new int[] {2, 5}, PrimeFactors.PrimeFactorsArray(20));
        Assert.Equal(new int[] {3, 13}, PrimeFactors.PrimeFactorsArray(39));
    }
    /// <summary>
    /// Tests for ConvertToString
    /// </summary>
    [Fact]
    public void TestConvertToString()
    {
        Assert.Equal("2 2 3", PrimeFactors.ConvertToString(new int[] { 2, 2, 3 }));
        Assert.Equal("2 2 3 5", PrimeFactors.ConvertToString(new int[] { 2, 2, 3, 5 }));
        Assert.Equal("2 3 3 5 8 7 15 32", PrimeFactors.ConvertToString(new int[] { 2, 3, 3, 5, 8, 7, 15, 32 }));
    }

}