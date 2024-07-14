using System.Text.RegularExpressions; // To use Regex

#region Pattern matching with Regular Expressions

// Checking for digits entered as text
Write("Enter your age: ");
string input = ReadLine()!;
Regex ageChecker = DigitsOnly(); // or new(DigitsOnlyText);
WriteLine(ageChecker.IsMatch(input) ? "Thank you!" : $"This is not a valid age: {input}\n");

// Regular expression performance improvements

/*
Some common symbols that you can use in regular expressions are shown in Table 8.5:

Symbol          Meaning                         Symbol      Meaning
________________________________________________________________________________________________________
^               Start of input                  $           End of input

\d              A single digit                  \D          A single non-digit

\s              Whitespace                      \S          Non-whitespace

\w              Word characters                 \W          Non-word characters

[A-Za-z0-9]     Range(s) of characters          \^          ^ (caret) character

[aeiou]         Set of characters               [^aeiou]    Not in a set of characters

.               Any single character            \.          . (dot) character

*               0 or more of previous           \*          * (asterisk)
_______________________________________________________________________________________________________________

                        Common regular expression quantifiers
                        ______________________________________
Symbol      Meaning                 Symbol      Meaning

+           One or more             ?           One or none

{3}         Exactly three           {3,5}       Three to five

{3,}        At least three          {,3}        Up to three

*/

#endregion

#region Examples of Regular Expressions
/*
Expression              Meaning
________________________________________________________________________________________________________

\d                      A single digit somewhere in the input

a                       The character “a” somewhere in the input

Bob                     The word “Bob” somewhere in the input

^Bob                    The word “Bob” at the start of the input

Bob$                    The word “Bob” at the end of the input

^\d{2}$                 Exactly two digits

^[0-9]{2}$              Exactly two digits

^[A-Z]{4,}$             At least four uppercase English letters in the ASCII character set only

^[A-Za-z]{4,}$          At least four upper or lowercase English letters in the ASCII character set only

^[A-Z]{2}\d{3}$         Two uppercase English letters in the ASCII character set and three digits only

^[A-Za-z\u00c0-\        At least one uppercase or lowercase English letter in the ASCII character set or
u017e]+$                European letters in the Unicode character set, as shown in the following list:
                        ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖ×ØÙÚÛÜÝ
                        Þßàáâãäåæçèéêëìíîïðñòóôõö÷øùúûüýþÿıŒœŠšŸŽž

^d.g$                   The letter d, then any character, and then the letter g, so it would match both
                        dig and dog or any single character between the d and g

^d\.g$                  The letter d, then a dot ., and then the letter g, so it would match d.g only

*/


#endregion

#region Splitting a complex comma-separated string

string films = """
        "Monsters, Inc.","I, Tonya","Lock, Stock and Two Smoking Barrels"
        """;

WriteLine($"Films to split: {films}");

// Splitting with string.Split.  The dumb way
string[] filmsDumb = films.Split(',');

WriteLine("Splitting with string.Split method:  ");
foreach(string film in filmsDumb)
{
    WriteLine($" {film}");
}
WriteLine();

// Splitting with regular expressions
Regex csv = CommaSeparator();  // or new(CommaSeparatorText)

MatchCollection filmsSmart = csv.Matches(films);

WriteLine("Splitting with regular expressions:  ");
foreach(Match film in filmsSmart)
{
    WriteLine($" {film.Groups[2].Value}");
}

#endregion
