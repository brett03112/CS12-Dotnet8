using System.Text; // To use Encoding


#region Encoding and Decoding Text

/*
Encoding        Description
________________________________________________________________________________________________________________
ASCII           This encodes a limited range of characters using the lower seven bits of a byte.

UTF-8           This represents each Unicode code point as a sequence of one to four bytes.

UTF-7           This is designed to be more efficient over 7-bit channels than UTF-8, but it has
                security and robustness issues, so UTF-8 is recommended over UTF-7.

UTF-16          This represents each Unicode code point as a sequence of one or two 16-bit
                integers.

UTF-32          This represents each Unicode code point as a 32-bit integer and is, therefore, a    
                fixed-length encoding unlike the other Unicode encodings, which are all variable length encodings.

ANSI/ISO        This provides support for a variety of code pages that are used to support a specific
encodings       language or group of languages.

*/


/*
To specify an encoding, pass the encoding as a second parameter to the helper type’s constructor, as
shown in the following code:

    StreamReader reader = new(stream, Encoding.UTF8);
    
    StreamWriter writer = new(stream, Encoding.UTF8);

*/
WriteLine("Encodings");
WriteLine("[1] ASCII");
WriteLine("[2] UTF-7");
WriteLine("[3] UTF-8");
WriteLine("[4] UTF-16 (Unicode)");
WriteLine("[5] UTF-32 (Unicode)");
WriteLine("[6] Latin1");
WriteLine("[any other key] Default encoding");
WriteLine();

Write("Press a number to choose an encoding: ");
ConsoleKey number = ReadKey(intercept: true).Key;
WriteLine(); WriteLine();

Encoding encoder = number switch
{
    ConsoleKey.D1 or ConsoleKey.NumPad1 => Encoding.ASCII,
    ConsoleKey.D2 or ConsoleKey.NumPad2 => Encoding.UTF7,
    ConsoleKey.D3 or ConsoleKey.NumPad3 => Encoding.UTF8,
    ConsoleKey.D4 or ConsoleKey.NumPad4 => Encoding.Unicode,
    ConsoleKey.D5 or ConsoleKey.NumPad5 => Encoding.UTF32,
    ConsoleKey.D6 or ConsoleKey.NumPad6 => Encoding.Latin1,
    _ => Encoding.Default
};

// Define a string to encode
string message = "Café £4.39";
WriteLine($"Text to encode: {message}. Characters: {message.Length}");

// Encode the string into a byte array
byte[] encoded = encoder.GetBytes(message);

// Check how many bytes the encoding needed
WriteLine("{0} used {1:N0} bytes.",
    encoder.GetType().Name, encoded.Length);
WriteLine();

// Enumerate each byte
WriteLine("BYTE | HEX | CHAR");
foreach (byte b in encoded)
{
    WriteLine($"{b,4} | {b,3:X} | {(char)b,4}");
}

// Decode the byte array back into a string and display it
string decoded = encoder.GetString(encoded);
WriteLine($"Decoded: {decoded}");


#endregion