

namespace Packt.Shared;

public class BankAccount
{
    public string? AccountName;  // Instance member.  It could be null
    public decimal Balance;      // Instance member.  Default value is 0.0m

    public static decimal InterestRate;  // Shared member.  Defaults to 0.0m  
}
