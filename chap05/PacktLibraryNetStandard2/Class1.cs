// All types in this file will be defined in this file-scoped namespace.
namespace Packt.Shared;
public class Person : object
{
    #region Fields: Data or state for this person (class)

    public string? Name; // ? means it can be null
    public DateTimeOffset Born;
    /*
        We have multiple choices for the data type of the Born field. .NET 6 introduced
        the DateOnly type. This would store only the date without a time value. DateTime
        stores the date and time of when the person was born, but it varies between local
        and UTC time. The best choice is DateTimeOffset, which stores the date, time,
        and hours offset from Universal Coordinated Time (UTC), which is related to the
        time zone. The choice depends on how much detail you need to store.
    */

    #endregion

    /*
        Member Access
        Modifier                          Description
        ________________________________________________________________________________________________________________________
        private----->                   The member is accessible inside the type only. This is the default.
                                            internal The member is accessible inside the type and any type in the same assembly.

        protected--->                   The member is accessible inside the type and any type that inherits from the type.

        public------>                   The member is accessible everywhere.

        internal protected-->           The member is accessible inside the type, any type in the same assembly, and any
                                            type that inherits from the type. Equivalent to a fictional access modifier named
                                            internal_or_protected.
        
        private protected-->            The member is accessible inside the type and any type that inherits from the
                                            type and is in the same assembly. Equivalent to a fictional access modifier named
                                            internal_and_protected. This combination is only available with C# 7.2 or later.
    */        
}