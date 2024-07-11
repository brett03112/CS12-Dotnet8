using System.Numerics;  // For BigInteger


#region BigIntegers
const int width = 40;

WriteLine($"ulong.MaxValue vs. a 30-digit BigInteger:");
WriteLine(new string('-', width));

ulong big = ulong.MaxValue;
WriteLine($"{big,width:N0}");

BigInteger bigger = BigInteger.Parse("123456789012345678901234567890");
WriteLine($"{bigger,width:N0}\n");

#endregion

#region Complex Numbers


#endregion