using System.Xml.Serialization; // for XmlSerializer
using FastJson = System.Text.Json.JsonSerializer;
using Packt.Shared; // for Person

#region Serializing as XML

List<Person> people = new()
{
    new(initialSalary: 30_000M)
    {
        FirstName = "Alice",
        LastName = "Smith",
        DateOfBirth = new(year: 1974, month: 3, day: 14)
    },

    new(initialSalary: 40_000M)
    {
        FirstName = "Bob",
        LastName = "Jones",
        DateOfBirth = new(year: 1969, month: 11, day: 23)
    },

    new(initialSalary: 20_000M)
    {
        FirstName = "Charlie",
        LastName = "Cox",
        DateOfBirth = new(year: 1984, month: 5, day: 4),
        Children = new()
        {
            new(initialSalary: 0M)
            {
                FirstName = "Sally",
                LastName = "Cox",
                DateOfBirth = new(year: 2012, month: 7, day: 12)
            }
        }
    }
};

SectionTitle("Serializing as XML");

    // Create serializer to format a "List of Person" as XML
    XmlSerializer xs = new(type: people.GetType());

    string path = Combine(CurrentDirectory, "people.xml");

    using (FileStream stream = File.Create(path))
    {
        // Serialize the object graph to the stream
        xs.Serialize(stream, people);
    }// close the stream

    OutputFileInfo(path);

#endregion

#region Deserializing as XML

SectionTitle("Deserializing as XML");

using (FileStream xmlLoad = File.Open(path, FileMode.Open))
{
    // Deserialize and cast the object graph into a "List of Person"
    List<Person>? loadedPeople = xs.Deserialize(xmlLoad) as List<Person>;

    if (loadedPeople is not null)
    {
        foreach (Person p in loadedPeople)
        {
            WriteLine("{0} has {1} children.",
                p.LastName, p.Children?.Count ?? 0);
        }
    }
}

/*
More Information: There are many other attributes defined in the System.Xml.
Serialization namespace that can be used to control the XML generated. A good place
to start is the official documentation for the XmlAttributeAttribute class found here:
https://learn.microsoft.com/en-us/dotnet/api/system.xml.serialization.
xmlattributeattribute. Do not get this class confused with the XmlAttribute class
in the System.Xml namespace. That is used to represent an XML attribute when reading
and writing XML, using XmlReader and XmlWriter.


Good Practice: When using XmlSerializer, remember that only the public fields and
properties are included, and the type must have a parameterless constructor. You can
customize the output with attributes.
*/
#endregion

#region Serialization as JSON

SectionTitle("Serializing with JSON");

// Create a file to write to
string jsonPath = Combine(CurrentDirectory, "people.json");

using (StreamWriter jsonStream = File.CreateText(jsonPath))
{
    Newtonsoft.Json.JsonSerializer jss = new();

    // Serialize the object graph into a string
    jss.Serialize(jsonStream, people);
} // Close the file stream and release resources

OutputFileInfo(jsonPath);
WriteLine();
#endregion

#region Deserializing as JSON

SectionTitle("Deserializing JSON files");

await using (FileStream jsonLoad = File.Open(jsonPath, FileMode.Open))
{
    // Deserialize object graph into a "List of Person"
    List<Person>? loadedPeople = 
        await FastJson.DeserializeAsync(utf8Json: jsonLoad,
        returnType: typeof(List<Person>)) as List<Person>;

    if (loadedPeople is not null)
    {
        foreach (Person p in loadedPeople)
        {
            WriteLine("{0} has {1} children.",
                p.LastName, p.Children?.Count ?? 0);
        }
    }
}

/*
Good Practice: Choose Json.NET for developer productivity and a large feature set, or
System.Text.Json for performance. You can review a list of the differences at the following link: 
https://learn.microsoft.com/en-us/dotnet/standard/serialization/
system-text-json-migrate-from-newtonsoft-how-to#table-of-differencesbetween-newtonsoftjson-and-systemtextjson.
*/
#endregion