using System.Diagnostics;
using Microsoft.Extensions.Configuration; // To use ConfigurationBuilder


string logPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "log.txt");
Console.WriteLine($"Writing to: {logPath}");

TextWriterTraceListener logFile = new(File.CreateText(logPath));
Trace.Listeners.Add(logFile);

#if DEBUG
// Text writer is buffered, so this option call Flush() on all listeners after writing
Trace.AutoFlush = true;
#endif

/*
    Good Practice: Any type that represents a file usually implements a buffer to
    improve performance. Instead of writing immediately to the file, data is written
    to an in-memory buffer, and only once the buffer is full will it be written in one
    chunk to the file. This behavior can be confusing while debugging because we do
    not immediately see the results! Enabling AutoFlush means the Flush method is
    called automatically after every write. This reduces performance, so you should
    only set it on during debugging and not in production.

    Good Practice: When running with the Debug configuration, both Debug and Trace are
    active and will write to any trace listeners. When running with the Release configuration,
    only Trace will write to any trace listeners. You can therefore use Debug.WriteLine calls
    liberally throughout your code, knowing they will be stripped out automatically when you
    build the release version of your application and will therefore not affect performance.
*/

Debug.WriteLine("Debug says, I am watching!");
Trace.WriteLine("Trace says, I am watching!");

string settingsFile = "appsettings.json";

string settingsPath = Path.Combine(Directory.GetCurrentDirectory(), settingsFile);

Console.WriteLine("Processing: {0}", settingsPath);
Console.WriteLine("--{0} contents--", settingsFile);
Console.WriteLine(File.ReadAllText(settingsPath));
Console.WriteLine("----");


ConfigurationBuilder builder = new();
builder.SetBasePath(Directory.GetCurrentDirectory());

// Add the settings file to the processed configuration and make it mandatory so an exception with be thrown if the file is not found
builder.AddJsonFile(settingsFile, optional: false, reloadOnChange: true);

IConfigurationRoot configuration = builder.Build();

TraceSwitch ts = new(displayName: "PacktSwitch",
    description: "This switch is set via a JSON config.");

configuration.GetSection("PacktSwitch").Bind(ts);

Console.WriteLine($"Trace switch value: {ts.Value}");
Console.WriteLine($"Trace switch level: {ts.Level}");

Trace.WriteLineIf(ts.TraceError, "Trace Error");
Trace.WriteLineIf(ts.TraceWarning, "Trace Warning");
Trace.WriteLineIf(ts.TraceInfo, "Trace Info");
Trace.WriteLineIf(ts.TraceVerbose, "Trace verbose");
/*
    If the appsettings.json file is not found, then the following exception will be
    thrown: System.IO.FileNotFoundException: The configuration file
    'appsettings.json' was not found and is not optional. The expected
    physical path was 'C:\cs12dotnet8\Chapter04\Instrumenting\bin\
    Debug\net8.0\appsettings.json'.
*/

int unitsInStock = 12;
LogSourceDetails(unitsInStock > 10);

// Close the text file (also flushes) and release the resources
Debug.Close();
Trace.Close();

Console.WriteLine("Press enter to exit");
Console.ReadLine();