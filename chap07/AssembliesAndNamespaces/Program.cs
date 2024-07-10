
/*
Namespaces and types in assemblies:
________________________________________________________________________________________________________
Many common .NET types are in the System.Runtime.dll assembly. There is not always a one-to-one
mapping between assemblies and namespaces. A single assembly can contain many namespaces
and a namespace can be defined in many assemblies. You can see the relationship between some
assemblies and the namespaces that they supply types for in Table 7.1:

Assembly                    Example namespaces                          Example types
_________________________________________________________________________________________________________
System.Runtime.dll          System, System.Collections,                 Int32, String,
                            System.Collections.Generic                  IEnumerable<T>

System.Console.dll          System                                      Console

System.Threading.dll        System.Threading                            Interlocked, Monitor, Mutex

System.Xml.XDocument.dll    System.Xml.Linq                             XDocument, XElement, XNode


NUGET PACKAGES

• Packages can be easily distributed on public feeds.

• Packages can be reused.

• Packages can ship on their own schedule.

• Packages can be tested independently of other packages.

• Packages can support different OSes and CPUs by including multiple versions of the same
    assembly built for different OSes and CPUs.

• Packages can have dependencies specific to only one library.

• Apps are smaller because unreferenced packages aren’t part of the distribution. Table 7.2 lists
    some of the more important packages and their important types:

    Package                                         Important types
__________________________________________________________________________________________________________________
System.Runtime                                      Object, String, Int32, Array

System.Collections                                  List<T>, Dictionary<TKey, TValue>

System.Net.Http                                     HttpClient, HttpResponseMessage

System.IO.FileSystem                                File, Directory

System.Reflection                                   Assembly, TypeInfo, MethodInfo
____________________________________________________________________________________________________________________

*/

using System.Xml.Linq; // To use XDocument
using System;  // To use String

XDocument doc = new();

string s1 = "Hello";
String s2 = "World";
Console.WriteLine(s1 + s2); //"HelloWorld"

/*

MAPPING C# ALIASES TO .NET TYPES

Keyword     .NET type      
___________________________
string      System.String   
char        System.Char
sbyte       System.SByte 
byte        System.Byte
short       System.Int16    
0ushort     System.UInt16
int         System.Int32 
uint        System.UInt32
long        System.Int64 
ulong       System.UInt64
nint        System.IntPtr 
nuint       System.UIntPtr
float       System.Single 
double      System.Double
decimal     System.Decimal 
bool        System.Boolean
object      System.Object 
dynamic     System.Dynamic.DynamicObject
*/

WriteLine($"Environment.Is64BitProcess = {Environment.Is64BitProcess}");
WriteLine($"int.MaxValue = {int.MaxValue:N0}");
WriteLine($"nint.MaxValue = {nint.MaxValue:N0}");