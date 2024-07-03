// All types in this file will be defined in this file-scoped namespace.
namespace Packt.Shared;
public partial class Person : object // Inherits from System.Object
{
    #region Fields: Data or state for this person (class)

    public string? Name; // ? means it can be null
    public const string Species = "Homo Sapiens"; // Constant field:  Values that are fixed at compile time

    public readonly string HomePlanet = "Earth"; // Readonly field:  Values that can be set at runtime.

    public readonly DateTime Instantiated;

    #region Constructors: Called when using new to create an instance of a type
    public Person()
    {
        // Constructors can set default values for fields
        // including any read-only fields like Instatiated.
        Name = "Unknown";
        Instantiated = DateTime.Now;
    }

    public Person(string initialName, string homePlanet)
    {
        Name = initialName;
        HomePlanet = homePlanet;
        Instantiated = DateTime.Now;
    }
    #endregion
    public DateTimeOffset Born;
    /*
        We have multiple choices for the data type of the Born field. .NET 6 introduced
        the DateOnly type. This would store only the date without a time value. DateTime
        stores the date and time of when the person was born, but it varies between local
        and UTC time. The best choice is DateTimeOffset, which stores the date, time,
        and hours offset from Universal Coordinated Time (UTC), which is related to the
        time zone. The choice depends on how much detail you need to store.
    */

    public List<Person> Children = [];
    //public WondersOfTheAncientWorld FavoriteAncientWonder;
    public WondersOfTheAncientWorld BucketList;

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
    #region Methods: actions the type can perform
        /// <summary>
        /// Writes the name and day of the week the person was born to the console.
        /// </summary>
    public void WriteToConsole()
    {
        WriteLine($"{Name} was born on a {Born:dddd}.");
    }
        /// <summary>
        /// Returns a string describing the origin of the person, including their name and home planet.
        /// </summary>
        /// <returns>A string in the format "{Name} was born on {HomePlanet}".</returns>
    public string GetOrigin()
    {
        return $"{Name} was born on {HomePlanet}.";
    }
        /// <summary>
        /// Returns a string containing a greeting from the person.
        /// </summary>
        /// <returns>A string in the format "{Name} says 'Hello!'".</returns>
    public string SayHello()
    {
        return $"{Name} says 'Hello!'";
    }

        /// <summary>
        /// returns a string containing a greeting from the person to the specified name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>A string in the format "{Name} says 'Hello, {name}!'".</returns> 
    public string SayHelloTo(string name)
    {
        return $"{Name} says 'Hello, {name}!'";
    }
        /// <summary>
        /// returns a string containing a  string command and a double number with a boolean value for active.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="number"></param>
        /// <param name="active"></param>
        /// <returns></returns>
    public string OptionalParameters(int count, string command = "Run!",
      double number = 0.0, bool active = true)
    {
        return string.Format(format: "command is {0}, number is {1}, active is {2}",
            arg0: command,
            arg1: number,
            arg2: active);
    }
        /// <summary>
        /// This method is used to illustrate the use of ref, in, and out parameters.
        /// </summary>
        /// <param name="w"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param> <summary>
    public void PassingParameters(int w, in int x, ref int y, out int z)
    {
        // out parameters cannot have a default value and 
        // they must be initialized inside the method.
        z = 100;

        // Increment each parameter except the read-only x.
        w++;
        // x++ gives a compiler error. 
        // x is a read-only parameter.

        y++; // y is a reference parameter that cannot have default values, but can already be set 
             // outside the method.

        z++; // Out only parameters cannot have default values and cannot be left uninitialized.

        WriteLine($"In the method: w = {w}, x = {x}, y = {y}, z = {z}");
    }
    #endregion

    #region Tuples
        /// <summary>
        /// Retrieves the name and quantity of fruit.
        /// </summary>
        /// <returns>A tuple containing the fruit name (string) and quantity (int).</returns>
    public (string, int) GetFruit()
    {
        return ("Apples", 5);
    }

    public (string Name, int Number) GetNamedFruit()
    {
        return (Name: "Apples", Number: 5);
    }

    //Deconstructors:  Break down this object into parts
    public void Deconstruct(out string? name, out DateTimeOffset dob)
    {
        name = Name;
        dob = Born;
    }

    public void Deconstruct(out string? name, out DateTimeOffset? dob, out WondersOfTheAncientWorld fav)
    {
        name = Name;
        dob = Born;
        fav = FavoriteAncientWonder;
    }
    
    // Method with local function
    public static int Factorial(int number)
    {
        if (number < 0)
        {
            throw new ArgumentException($"{nameof(number)} cannot be less than zero!");
        }
        return localFactorial(number);

        int localFactorial(int localNumber) // local function
        {
            if (localNumber == 0) return 1;
            return localNumber * localFactorial(localNumber - 1);
        }
    }
   
    #endregion       
}