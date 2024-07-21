using System.Xml; // TO use XmlWriteer and so on

using Packt.Shared;


#region Reading and writing files using StreamReader and StreamWriter

/*
There is an abstract class named Stream that represents any type of stream. Remember that an
abstract class cannot be instantiated using new; it can only be inherited. This is because it is only
partially implemented.

There are many concrete classes that inherit from this base class, including FileStream, MemoryStream,
BufferedStream, GZipStream, and SslStream. They all work the same way. All streams implement
IDisposable, so they have a Dispose method to release unmanaged resources.

Member                 Description
______________________________________________________________________________________________________________________________________
CanRead,                These properties determine if you can read from and write to the stream.
CanWrite

Length,                 These properties determine the total number of bytes and the current position
Position                within the stream. These properties may throw a NotSupportedException for
                        some types of streams, for example, if CanSeek returns false.

Close, Dispose          This method closes the stream and releases its resources. You can call either
                        method since the implementation of Dispose calls Close!

Flush                   If the stream has a buffer, then this method writes the bytes in the buffer to the
                        stream, and the buffer is cleared.

CanSeek                 This property determines if the Seek method can be used.

Seek                    This method moves the current position to the one specified in its parameter.

Read, ReadAsync         These methods read a specified number of bytes from the stream into a byte
                        array and advance the position.

ReadByte                This method reads the next byte from the stream and advances the position.

Write,                  These methods write the contents of a byte array into the stream.
WriteAsync

WriteByte               This method writes a byte to the stream.

*/

/*
                Storage of streams and their location

Namespace                   Class                       Description
_______________________________________________________________________________________________________________
System.IO                   FileStream                  Bytes stored in the filesystem

System.IO                   MemoryStream                Bytes stored in memory in the current process

System.Net.Sockets          NetworkStream               Bytes stored at a network location
*/

/*
Understanding function streams
****Function streams cannot exist on their own but can only be “plugged into” other streams to add functionality. 

Namespace                               Class                       Description
____________________________________________________________________________________________________________________
System.Security.Cryptography            CryptoStream                This encrypts and decrypts the stream.

System.IO.Compression                   GZipStream,DeflateStream    These compress and decompress the stream.

System.Net.Security                     AuthenticatedStream         This sends credentials across the stream.

*/

/*
Understanding stream helpers

Although there will be occasions where you need to work with streams at a low level, most often, you
can plug helper classes into the chain to make things easier. All the helper types for streams implement
IDisposable, so they have a Dispose method to release unmanaged resources.

Namespace                   Class               Description
______________________________________________________________________________________________________________________
System.IO                   StreamReader        This reads from the underlying stream as plain text.

System.IO                   StreamWriter        This writes to the underlying stream as plain text.

System.IO                   BinaryReader        This reads from streams as .NET types. For example, the
                                                ReadDecimal method reads the next 16 bytes from the underlying
                                                stream as a decimal value, and the ReadInt32 method reads the
                                                next 4 bytes as an int value.

System.IO                   BinaryWriter        This writes to streams as .NET types. For example, the Write
                                                method with a decimal parameter writes 16 bytes to the
                                                underlying stream, and the Write method with an int parameter
                                                writes 4 bytes.

System.Xml                  XmlReader           This reads from the underlying stream using the XML format.

System.Xml                  XmlWriter           This writes to the underlying stream using the XML format.

*/

SectionTitle("Writing to text streams");

// Define a file to write to
string textFile = Combine(CurrentDirectory, "streams.txt");

// Create a text file and return a helper writer
StreamWriter text = File.CreateText(textFile);

// Enumerate the strings, writing each one to the stream on a separate line
foreach (string item in Viper.Callsigns)
{
    text.WriteLine(item);
}

text.Close(); // Release unmanaged file resources

OutputFileInfo(textFile);

WriteLine();
/*________________________________________________________________________________________________________*/

SectionTitle("Writing to XML streams");

// Define a file path to write to
string xmlFile = Combine(CurrentDirectory, "streams.xml");

// Declare variables for the filestream and XML writer
FileStream? xmlFileStream = null;
XmlWriter? xml = null;

try
{
    xmlFileStream = File.Create(xmlFile);

    // Wrap the file stream in an XML writer helper and tell it 
    // to automatically indent nested elements

    xml = XmlWriter.Create(xmlFileStream, new XmlWriterSettings { Indent = true });

    // Write the XML declaration
    xml.WriteStartDocument();

    // Write a root element
    xml.WriteStartElement("callsigns");

    // Enumerate the strings, writing each one to the stream
    foreach (string item in Viper.Callsigns)
    {
        xml.WriteElementString("callsign", item);
    }

    // Write the close root element
    xml.WriteEndElement();
}

catch (Exception ex)
{
    // If the path doesn't exist the exception will be caught
    WriteLine($"{ex.GetType()} says {ex.Message}");
}

finally
{
    if (xml is not null)
    {
        xml.Close();
        WriteLine("The XML writer's unmanaged resources have been disposed.");
    }

    if (xmlFileStream is not null)
    {
        xmlFileStream.Close();
        WriteLine("The file stream's unmanaged resources have been disposed.");
    }
}

OutputFileInfo(xmlFile);

WriteLine();
/*________________________________________________________________________________________________________*/

SectionTitle("Compressing streams");

Compress(algorithm: "gzip");

Compress(algorithm: "brotli");
#endregion