using System.Globalization;  // To use CultureInfo

OutputEncoding = System.Text.Encoding.UTF8; // Enable Euro symbol


#region Working with Text
/*
Namespace                                   Type                Description
_____________________________________________________________________________________________________
System                                      Char                Storage for a single text character

System                                      String              Storage for multiple text characters

System.Text                                 StringBuilder       Efficiently manipulates strings

System.Text.RegularExpressions              Regex               Efficiently pattern-matches strings

*/

string city = "London";
WriteLine($"{city} is {city.Length} characters long.");

WriteLine($"The first char is {city[0]} and the fourth is {city[3]}.");

// Splitting strings
string cities = "Paris,Tehran,Chaennai,Sydney,New York,Medellin";

string[] citiesArray = cities.Split(',');
WriteLine($"There are {citiesArray.Length} cities in the array.");
foreach (string item in citiesArray)
{
    WriteLine($" {item}");
}

// Getting part of a string
string fullName = "Alan Shore";

int indexOfTheSpace = fullName.IndexOf(' ');

string firstName = fullName.Substring(startIndex: 0, length: indexOfTheSpace);
string lastName = fullName.Substring(indexOfTheSpace + 1);

WriteLine($"Original: {fullName}");
WriteLine($"First: {firstName}");
WriteLine($"Last: {lastName}");

//Checking a string for content
string company = "Microsoft";
WriteLine($"Tect: {company}");
WriteLine("Starts with M: {0}, contains an N: {1}",
    arg0: company.StartsWith("M"),
    arg1: company.Contains("N"));
WriteLine();

// Comparing string values
CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");

string text1 = "Mark";
string text2 = "MARK";

WriteLine($"text1: {text1}, text2: {text2}");
WriteLine($"Compare: {0}.", string.Compare(text1, text2));

WriteLine("Compare (ignoreCase): {0}.", string.Compare(text1, text2, ignoreCase: true));

WriteLine("Compare (InvariantCultureIgnoreCase):  {0}.\n",
    string.Compare(text1, text2, StringComparison.InvariantCultureIgnoreCase));

/*
OTHER STRING METHODS

Member                                                      Description
______________________________________________________________________________________________________________________

Trim, TrimStart, TrimEnd                                    These methods trim whitespace characters such as space, tab,
                                                            and carriage return from the start and/or end.

ToUpper, ToLower                                            These convert all the characters into uppercase or lowercase.

Insert, Remove                                              These methods insert or remove some text.

Replace                                                     This replaces some text with other text.

string.Empty                                                This can be used instead of allocating memory each time you
                                                            use a literal string value using an empty pair of double quotes("").

string.Concat                                               This concatenates two string variables. The + operator does the
                                                            equivalent when used between string operands.

string.Join                                                 This concatenates one or more string variables with a character
                                                            in between each one.

string.IsNullOrEmpty                                        This checks whether a string variable is null or empty.

string.IsNullOrWhiteSpace                                   This checks whether a string variable is null or whitespace;
                                                            that is, a mix of any number of horizontal and vertical spacing
                                                            characters, for example, tab, space, carriage return, line feed,
                                                            and so on.

string.Format                                               An alternative method to string interpolation for outputting
                                                            formatted string values, which uses positioned instead of
                                                            named parameters

*/

string recombined = string.Join(" => ", citiesArray);
WriteLine(recombined);
WriteLine();

string fruit = "Apples";
decimal price = .39M;
DateTime when = DateTime.Today;

WriteLine($"Interpolated: {fruit} cost {price:C} on {when:dddd}");
WriteLine(string.Format("string.Format: {0} cost {1:C} on {2:dddd}.\n", arg0: fruit, arg1: price, arg2: when));


#endregion

