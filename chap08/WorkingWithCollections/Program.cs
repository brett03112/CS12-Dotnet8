using System.Collections.Frozen; // To use FrozenDictionary<T,T>
// Define an alias for a dictionary with string key and string value
using StringDictionary = System.Collections.Generic.Dictionary<string, string>;
using System.Collections.Immutable;  // To us ImmutableDictionary<T,T>

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
WriteLine($"The definition of {key} is: {keywords[key]}\n");

#endregion

#region Sets, Stacks, Queues
/*
COMMON SET METHODS:

Method                      Description
_______________________________________________________________________________________________________________________
Add                         If the item does not already exist in the set, then it is added. Returns
                            true if the item was added, and false if it was already in the set.

ExceptWith                  Removes the items in the set passed as the parameter from the set.

IntersectWith               Removes the items not in the set passed as the parameter and in the set.

IsProperSubsetOf,           A subset is a set whose items are all in the other set. A proper subset is
IsProperSupersetOf,         a set whose items are all in the other set but there is at least one item
IsSubsetOf, IsSupersetOf    in the other set that is not in the set. A superset is a set that contains all
                            the items in the other set. A proper superset is a set that contains all
                            the items in the other set and at least one more not in the other set.

Overlaps                    The set and the other set share at least one common item.

SetEquals                   The set and the other set contain exactly the same items.

SymmetricExceptWith         Removes the items not in the set passed as the parameter from the set
                            and adds any that are missing.

UnionWith                   Adds any items in the set passed as the parameter to the set that are
                            not already in the set.
*/

HashSet<string> names = new();

foreach (string name in new[] {"Adam", "Barry", "Charlie", "Barry"})
{
    bool added = names.Add(name);
    WriteLine($"{name} was added: {added}");
}

WriteLine($"names set: {string.Join(',', names)}\n");


Queue<string> coffee = new();

coffee.Enqueue("Damir"); // Front of the queue
coffee.Enqueue("Andrea");
coffee.Enqueue("Ronald");
coffee.Enqueue("Amir");
coffee.Enqueue("Irina"); // Back of the queue

OutputCollection("Initial queue from front to back", coffee);

// Server handles next person in queue
string served = coffee.Dequeue();
WriteLine($"Served: {served}");

// Server handles next person in queue
served = coffee.Dequeue();
WriteLine($"Served: {served}");
OutputCollection("Current queue from front to back", coffee);

WriteLine($"{coffee.Peek()} is next in line to be served.");
OutputCollection("Current queue from front to back\n", coffee);
WriteLine();

// Priority queue
PriorityQueue<string, int> vaccine = new();
// Add some people.
// 1 = High priority people in their 70s or poor health.
// 2 = Medium priority e.g. middle-aged.
// 3 = Low priority e.g. teens and twenties.

vaccine.Enqueue("Pamela", 1);
vaccine.Enqueue("Rebecca", 3);
vaccine.Enqueue("Juliet", 2);
vaccine.Enqueue("Ian", 1);

OutputPQ("Vaccine priority queue", vaccine.UnorderedItems);

WriteLine($"{vaccine.Dequeue()} has been vaccinated.");
WriteLine($"{vaccine.Dequeue()} has been vaccinated.");
OutputPQ("Current queue for vaccination", vaccine.UnorderedItems);

WriteLine($"{vaccine.Dequeue()} has been vaccinated.");

WriteLine("Adding Mark to queue with priority 2.");
vaccine.Enqueue("Mark", 2);

WriteLine($"{vaccine.Peek()} will be next to be vaccinated.");
OutputPQ("Current queue for vaccination", vaccine.UnorderedItems);
WriteLine();

/*
COLLECTION ADD AND REMOVE METHODS:
________________________________________________________________________________________________________
COLLECTION          ADD METHOD              REMOVE METHOD               DESCRIPTION

List                Add, Insert             Remove,RemoveAt             Lists are ordered so items have an integer index
                                                                        position. Add will add a new item at the end of
                                                                        the list. Insert will add a new item at the index
                                                                        position specified.

Dictionary          Add                     Remove                      Dictionaries are not ordered so items do not have
                                                                        integer index positions. You can check if a key has
                                                                        been used by calling the ContainsKey method. 

Stack               Push                    Pop                         Stacks always add a new item at the top of the
                                                                        stack using the Push method. The first item is at
                                                                        the bottom. Items are always removed from the
                                                                        top of the stack using the Pop method. Call the
                                                                        Peek method to see this value without removing it.
                                                                        Stacks are LIFO.

Queue               Enqueue                 Dequeue                     Queues always add a new item at the back of
                                                                        the queue using the Enqueue method. The first item
                                                                        is at the front. Items are always removed from the
                                                                        front of the queue using the Dequeue method. Call
                                                                        the Peek method to see this value without removing
                                                                        it. Queues are FIFO.

*/


/*
COMMONE SORTING AUTO-SORTING METHODS:
______________________________________________________________________________________________________________________________
SortedDictionary<TKey, TValue>              This represents a collection of key/value pairs that are sorted by
                                            key. Internally it maintains a binary tree for items.

SortedList<Tkey, TValue>                    This represents a collection of key-value pairs that are sorted by
                                            key. The name is misleading because this is not a list. Compared
                                            to SortedDictionary<TKey, TValue>, retrieval performance is
                                            similar, it uses less memory, and insert and remove operations are
                                            slower for unsorted data. If it is populated from sorted data, then it
                                            is faster. Internally, it maintains a sorted array with a binary search
                                            to find elements.

SortedSet<T>                                This represents a collection of unique objects that are maintained
                                            in a sorted order.                                   

*/

// UseDictionary(keywords); not immutable

// UseDictionary(keywords.AsReadOnly()); immutable, but sloppy

UseDictionary(keywords.ToImmutableDictionary());

ImmutableDictionary<string,string> immutableKeywords = keywords.ToImmutableDictionary();
// Call the Add method with a return value
ImmutableDictionary<string, string> newDictionary = immutableKeywords.Add(key: Guid.NewGuid().ToString(), value: Guid.NewGuid().ToString());

OutputCollection("Immutable keywords dictionary", immutableKeywords);
OutputCollection("New immutable keywords dictionary", newDictionary);
WriteLine();

// Creating a frozen collection has an overhead to perform the sometimes complex optimizations
FrozenDictionary<string, string> frozenKeywords = keywords.ToFrozenDictionary();

OutputCollection("Frozen keywords dictionary", frozenKeywords);

// Lookups are faster in a frozen collection
WriteLine($"Define long: {frozenKeywords["long"]}");

/*
WHAT THE ADD MEOD DOES DEPENDS ON THE TYPE:
________________________________________________________________________________________________________________________________
• List<T>: Adds a new item to the end of the existing list.

• Dictionary<TKey, TValue>: Adds a new item to the existing dictionary in a position
    determined by its internal structure.

• ReadOnlyCollection<T>: Throws a not-supported exception.

• ImmutableList<T>: Returns a new list with the new item in it. Does not affect the
    original list.

• ImmutableDictionary<TKey, TValue>: Returns a new dictionary with the new item
    in it. Does not affect the original dictionary.

• FrozenDictionary<TKey, TValue>: Does not exist.
*/


WriteLine();
#endregion



