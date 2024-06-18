string password = "ninja";

if (password.Length < 8)
{
    WriteLine("Your password is too short. Use at least 8 characters.");
}
else
{
    WriteLine("Your password is strong enough.");
}

// Add and remove the "" to change between string and int.
object o = 3;
int x = 4;
if (o is int i)
{
    WriteLine($"{i} x {x} = {i * x}");
}
else
{
    WriteLine("o is not an int so it cannot multiply!");
}

#region switch statements
int number =  Random.Shared.Next(minValue: 1, maxValue: 7);
WriteLine($"The number is {number}");

switch (number)
{
    case 1:
        WriteLine("One");
        break; // Jumps to the end of the switch statement.
    case 2:
        WriteLine("Two");
        goto case 1;
    case 3: // Multiple case sections can use the same label.
    case 4:
        WriteLine("Three or four");
        goto case 1;
    case 5:
    case 6:
        WriteLine("Five or six");
        goto case 1;
    case 7:
        WriteLine("Seven");
        break;
    default:
        WriteLine("Default case.");
        break;
}

WriteLine("After the end of the switch.");
#pragma warning disable CS0164 // This label has not been referenced
A_label:
WriteLine($"After A_label");
#pragma warning restore CS0164 // This label has not been referenced
#endregion



#region using the Animal.cs class file to create an array of animals and use switch statements and switch expressions
var animals = new Animal?[]
{
    new Cat { Name = "Karen", Born = new(year: 2022, month: 8,
    day: 23), Legs = 4, IsDomestic = true },
    null,

    new Cat { Name = "Mufasa", Born = new(year: 1994, month: 6,
    day: 12) },

    new Spider { Name = "Sid Vicious", Born = DateTime.Today,
    IsPoisonous = true},

    new Spider { Name = "Captain Furry", Born = DateTime.Today }
};
foreach (Animal? animal in animals)
{
    string message;
    switch (animal)
    {
        case Cat fourLeggedCat when fourLeggedCat.Legs == 4:
        message = $"The cat named {fourLeggedCat.Name} has four legs.";
        break;

        case Cat wildCat when wildCat.IsDomestic == false:
        message = $"The non-domestic cat is named {wildCat.Name}.";
        break;

        case Cat cat:
        message = $"The cat is named {cat.Name}.";
        break;

        default: // default is always evaluated last.
        message = $"{animal.Name} is a {animal.GetType().Name}.";
        break;

        case Spider spider when spider.IsPoisonous:
        message = $"The {spider.Name} spider is poisonous. Run!";
        break;

        case null:
        message = "The animal is null.";
        break;
    }
    

    WriteLine($"switch statement: {message}");
}

foreach (Animal? animal in animals)
{
    // switch expression using lambda expression
    string message = animal switch
    {
        Cat fourLeggedCat when fourLeggedCat.Legs == 4 => $"The cat named {fourLeggedCat.Name} has four legs.",

        Cat wildCat when wildCat.IsDomestic == false => $"The non-domestic cat is named {wildCat.Name}",

        Cat cat => $"The cat is named {cat.Name}",

        Spider spider when spider.IsPoisonous => $"The {spider.Name} spider is poisonous. Run!",

        null => "The animal is null.",

        _ => $"{animal.Name} is a {animal.GetType().Name}." // default value.  _ is a discard.
    };

    WriteLine($"switch expression: {message}");
}

#endregion

