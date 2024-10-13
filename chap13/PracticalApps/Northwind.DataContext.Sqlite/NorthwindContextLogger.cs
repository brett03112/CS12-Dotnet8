using static System.Environment;

namespace Northwind.EntityModels;

public class NorthwindContextLogger
{
    /// <summary>
    /// Writes the given message to the file 'northwindlog.txt' in the user's desktop directory.
    /// </summary>
    /// <param name="message">The message to be written to the log file.</param>
    public static void WriteLine(string message)
    {
        string path = Path.Combine(GetFolderPath(SpecialFolder.DesktopDirectory), "northwindlog.txt");

        StreamWriter textFile = File.AppendText(path);
        textFile.WriteLine(message);
        textFile.Close();
    }
}