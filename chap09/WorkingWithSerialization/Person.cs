using System.Xml.Serialization; // To use [XmlAttribute] and reduce the size of the generated XML file
                                // Will reduce the size of the generated XML file from 793 bytes to 488 bytes
namespace Packt.Shared;

public class Person
{
    public Person(decimal initialSalary)
    {
        Salary = initialSalary;
    }

    public Person() { } // A parameterless constructor is required for XML serialization

    [XmlAttribute("fname")]
    public string? FirstName { get; set; }

    [XmlAttribute("lname")]
    public string? LastName { get; set; }

    [XmlAttribute("dob")]
    public DateTime DateOfBirth { get; set; }
    public HashSet<Person>? Children { get; set; }
    protected decimal Salary { get; set; }
}