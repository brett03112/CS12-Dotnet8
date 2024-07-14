
// Define an alias for a dictionary with string key and string value
using StringDictionary = System.Collections.Generic.Dictionary<string, string>;


#region Collections List<T>
// Simple syntax for creating a list and adding three items
List<string> cities = new();
cities.Add("London");
cities.Add("Paris");
cities.Add("Milan");

/* Alternative syntax that is converted by the compiler into
    the three Add method calls above.

    List<string> cities = new()
        { "London", "Paris", "Milan" }; */

/* Alternative syntax that passes an array
of string values to AddRange method.

List<string> cities = new();
cities.AddRange(new[] { "London", "Paris", "Milan" }); */

OutputCollection("Initial list", cities);
WriteLine($"The first city is {cities[0]}.");
WriteLine($"The first city is {cities[cities.Count - 1]}.");

cities.Insert(0, "Sydney");
OutputCollection("After inserting Sydney at index 0", cities);

cities.RemoveAt(1);
cities.Remove("Milan");

OutputCollection("After removing cities\n", cities);

#endregion

#region Dictionaries<T>

/*
namespace System.Collections.Generic;

[DefaultMember("Item")] // aka "this" indexer.

public interface IDictionary<TKey, TValue>
: ICollection<KeyValuePair<TKey, TValue>>,
IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
{
    TValue this[TKey key] { get; set; }
    ICollection<TKey> Keys { get; }
    ICollection<TValue> Values { get; }
    void Add(TKey key, TValue value);
    bool ContainsKey(TKey key);
    bool Remove(TKey key);
    bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value);
}

EXAMPLES:

Key                     Value
________________________________________

BSA                     Bob Smith
MW                      Max Williams
BSB                     Bob Smith
AM                      Amir Mohammed
*/


// Declare a dictionary without the aliaas
// Dictionary<string, string> keywords = new();
WriteLine();
// Use the alias to declare a dictionary
StringDictionary keywords = new();

// Add using named parameters
keywords.Add(key: "int", value: "32-bit integer data type");

// Add using positional parameters
keywords.Add("long", "64-bit integer data type");
keywords.Add("float", "Single-precision floating-point data type");

/* Alternative syntax; compiler converts this to calls to Add method.

Dictionary<string, string> keywords = new()
{
    { "int", "32-bit integer data type" },
    { "long", "64-bit integer data type" },
    { "float", "Single precision floating point number" },
}; */

/* Alternative syntax; compiler converts this to calls to Add method.

Dictionary<string, string> keywords = new()
{
    ["int"] = "32-bit integer data type",
    ["long"] = "64-bit integer data type",
    ["float"] = "Single precision floating point number",
}; */

OutputCollection("Dictionary keys", keywords.Keys);
OutputCollection("Dictionary values", keywords.Values);

WriteLine("Keywords and their definitions: ");
foreach (KeyValuePair<string, string> item in keywords)
{
    WriteLine($"  {item.Key}: {item.Value}");
}

// Lookup a value using a key
string key = "long";
WriteLine($"The definition of {key} is: {keywords[key]}");

#endregion
