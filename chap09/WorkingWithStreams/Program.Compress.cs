using Packt.Shared;
using System.IO.Compression;  // To use BrotliStream, GZipStream
using System.Xml; // To use XmlWriteer and XmlReader

partial class Program
{
    private static void Compress(string algorithm = "gzip")
    {
        // Define a file path using the algorithm as a file extension
        string filePath = Combine(CurrentDirectory, $"streams.{algorithm}");

        FileStream file = File.Create(filePath);

        Stream compressor;
        if (algorithm == "gzip")
        {
            compressor  = new GZipStream(file, CompressionMode.Compress);
        }
        else
        {
            compressor = new BrotliStream(file, CompressionMode.Compress);
        }

        using (compressor)
        {
            using (XmlWriter xml = XmlWriter.Create(compressor))
            {
                xml.WriteStartDocument();
                xml.WriteStartElement("callsigns");
                foreach (string item in Viper.Callsigns)
                {
                    xml.WriteElementString("callsign", item);
                }
            }
        } // Also close the underlying stream

        OutputFileInfo(filePath);

        // Read the compressed file
        WriteLine("Reading the compressed XML file:  ");
        file = File.Open(filePath, FileMode.Open);
        Stream decompressor;
        if (algorithm == "gzip")
        {
            decompressor = new GZipStream(file, CompressionMode.Decompress);
        }
        else
        {
            decompressor = new BrotliStream(file, CompressionMode.Decompress);
        }

        using (decompressor)
        
        using (XmlReader reader = XmlReader.Create(decompressor))
        
            while (reader.Read())
            {
                // Check if we are on an element node named callsign
                if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "callsign"))
                {
                    reader.Read(); // Move to the text inside the element
                    WriteLine($"{reader.Value}"); // Read its value
                }

                // Alternative syntax with property patterm matching:
                // if (reader is { NodeType: XmlNodeType.Element, Name: "callsign" })
            }
    }

    /*
    To summarize:
        • Uncompressed: 320 bytes
        • GZIP-compressed: 151 bytes
        • Brotli-compressed: 117 bytes
    */
}