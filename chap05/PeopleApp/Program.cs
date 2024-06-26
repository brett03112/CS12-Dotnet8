using Packt.Shared;

ConfigureConsole(); // Sets current culture to "en-US"
// Alternatives:
// ConfigureConsole(useComputerCulture: true); // Use your culture.
// ConfigureConsole(culture: "fr-FR"); // Use French culture.

Person bob = new(); // C#9 or later
bob.Name = "Bob Smith";

bob.Born = new DateTimeOffset(
    year: 1965, month: 12, day: 22,
    hour: 16, minute: 28, second: 0,
    offset: TimeSpan.FromHours(-5));  //US Eastern Standard Time
// WriteLine(bob);  Implicit call to ToString()  WriteLine(bob.ToString()) does the same thing

WriteLine(format: "{0} was born on {1:D}.", arg0: bob.Name, arg1: bob.Born);
/*

The format code for arg1 is one of the standard date and time formats. D means
a long date format and d would mean a short date format. You can learn more
about standard date and time format codes at the following link: https://learn.
microsoft.com/en-us/dotnet/standard/base-types/standard-date-andtime-format-strings.

*/