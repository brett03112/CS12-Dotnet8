using System.Text.RegularExpressions;

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

WriteLine("The default regualar expression checks for at least one digit.");
Write("Enter a regualar expression.  Press enter to use the default: ");
while (ReadKey(intercept: true).Key != ConsoleKey.Escape)
{
    Write("Enter a regualar expression.  Press enter to use the default: ");
    string? pattern = ReadLine();

    if (string.IsNullOrWhiteSpace(pattern))
    {
        pattern = @"^\d+$";
    }

    WriteLine();
    Write("Enter some input: ");
    string? input = ReadLine()!;  // will never be null

    Regex r = new(pattern);

    WriteLine($"{input} matches {pattern}: {r.IsMatch(input)}");

    WriteLine("Press ESC to end or any other key to try again.");

}
