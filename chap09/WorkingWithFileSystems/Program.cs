using Spectre.Console; // To use Table

#region Handling cross-platform environments and filesystems

SectionTitle("Handling cross-platform environments and filesystems");

// Create a Spectre.Console table.
Table table = new();

// add 2 columns with markup for colors
table.AddColumn("[blue]MEMBER[/]");
table.AddColumn("[blue]VALUE[/]");

// Add some rows
table.AddRow("Path.PathSeparator", PathSeparator.ToString());
table.AddRow("Path.DirectorySeparatorChar", DirectorySeparatorChar.ToString());
table.AddRow("Directory.GetCurrentDirectory()", GetCurrentDirectory());
table.AddRow("Environment.CurrentDirectory", CurrentDirectory);
table.AddRow("Environment.SystemDirectory", SystemDirectory);
table.AddRow("Path.GetTempPath()", GetTempPath());
table.AddRow("");
table.AddRow("GetFolderPath(SpecialFolder", "");
table.AddRow(" .System)", GetFolderPath(SpecialFolder.System));
table.AddRow(" .ApplicationData)", GetFolderPath(SpecialFolder.ApplicationData));
table.AddRow(" .MyDocuments)", GetFolderPath(SpecialFolder.MyDocuments));
table.AddRow(" .Personal)", GetFolderPath(SpecialFolder.Personal));

// Render the table to the console
AnsiConsole.Write(table);

// handling drives

SectionTitle("Managing drives");

Table drives = new();

drives.AddColumn("[blue]NAME[/]");
drives.AddColumn("[blue]TYPE[/]");
drives.AddColumn("[blue]FORMAT[/]");
drives.AddColumn(new TableColumn("[blue]SIZE (BYTES[/])").RightAligned());
drives.AddColumn(new TableColumn("[blue] FREE SPACE[/]").RightAligned());

foreach (DriveInfo drive in DriveInfo.GetDrives())
{
    if (drive.IsReady)
    {
        drives.AddRow(drive.Name, drive.DriveType.ToString(), drive.DriveFormat, drive.TotalSize.ToString("N0"), drive.AvailableFreeSpace.ToString("N0"));
    }
    else
    {
        drives.AddRow(drive.Name, drive.DriveType.ToString(), string.Empty, string.Empty, string.Empty);
    }
}

AnsiConsole.Write(drives);
#endregion

#region Creating folders and files

SectionTitle("Managing directories");

string newFolder = Combine(GetFolderPath(SpecialFolder.Personal), "NewFolder");

WriteLine($"Working with: {newFolder}");

// We must explicitly say which Exists method to use 
// because we statically imported both Path and Directory
WriteLine($"Does it exist? {Path.Exists(newFolder)}");

WriteLine("Creating it...");
CreateDirectory(newFolder);

// Let's use the Directory.Exists method this time
WriteLine($"Does it exist? {Directory.Exists(newFolder)}");
Write("Confirm the directory exists, and then press any key...");
ReadKey(intercept: true);

WriteLine("Deleting it...");
Delete(newFolder, recursive: true);
WriteLine($"Does it exist? {Path.Exists(newFolder)}\n");

#endregion

#region Working with files
/*
a. Check for the existence of a file.

b. Create a text file.

c. Write a line of text to the file.

d. Close the file to release system resources and file locks (this would normally be done
    inside a try-finally statement block to ensure that the file is closed even if an exception occurs when writing to it).

e. Copy the file to a backup.

f. Delete the original file.

g. Read the backup file’s contents and then close it:
*/
SectionTitle("Managing files");

// Define a directory path to ouput files starting in the user's folder
string dir = Combine(GetFolderPath(SpecialFolder.Personal), "OutputFiles");

CreateDirectory(dir);

// Define file paths
string textFile = Combine(dir, "Dummy.txt");
string backupFile = Combine(dir, "dummy.bak");
WriteLine($"Working with: {textFile}");

WriteLine($"Does it exist? {File.Exists(textFile)}");

// Create a new text file and write a line to it
StreamWriter textWriter = File.CreateText(textFile);
textWriter.WriteLine("Hello, C#!!");
textWriter.Close();
WriteLine($"Does it exist? {File.Exists(textFile)}");

// Copy the file and overwrite if it already exists
File.Copy(sourceFileName: textFile, destFileName: backupFile, overwrite: true);

WriteLine($"Does {backupFile} exist? {File.Exists(backupFile)}");

WriteLine("Confirm the files exist, and then press any key...");
ReadKey(intercept: true);

// Delete the original file
File.Delete(textFile);
WriteLine($"Does it exist? {File.Exists(textFile)}\n");

// Read from the text file backup
StreamReader textReader = File.OpenText(backupFile);
WriteLine(textReader.ReadToEnd());
textReader.Close();

#endregion

#region Managing paths

SectionTitle("Managing paths");

WriteLine($"Folder Name: {GetDirectoryName(textFile)}");
WriteLine($"File Name: {GetFileName(textFile)}");
WriteLine("file Name without extension: {0}", GetFileNameWithoutExtension(textFile));
WriteLine($"File Extension: {GetExtension(textFile)}");
WriteLine($"Random file name: {GetRandomFileName()}");
WriteLine($"Temp file name: {GetTempFileName()}");

#endregion

#region Getting File Information
/*
Class                                   Members
_________________________________________________________________________________________________________________________________

FileSystemInfo                          Fields: FullPath, OriginalPath

                                        Properties: Attributes, CreationTime, CreationTimeUtc, Exists,
                                        Extension, FullName, LastAccessTime, LastAccessTimeUtc,
                                        LastWriteTime, LastWriteTimeUtc, Name

                                        Methods: Delete, GetObjectData, Refresh
___________________________________________________________________________________________________________________________________
DirectoryInfo                           Properties: Parent, Root

                                        Methods: Create, CreateSubdirectory, EnumerateDirectories,
                                        EnumerateFiles, EnumerateFileSystemInfos, GetAccessControl,
                                        GetDirectories, GetFiles, GetFileSystemInfos, MoveTo,
                                        SetAccessControl
____________________________________________________________________________________________________________________________________
FileInfo                                Properties: Directory, DirectoryName, IsReadOnly, Length

                                        Methods: AppendText, CopyTo, Create, CreateText, Decrypt, Encrypt,
                                        GetAccessControl, MoveTo, Open, OpenRead, OpenText, OpenWrite, Replace,
                                        SetAccessControl
*/

SectionTitle("Getting File Information");

FileInfo info = new(backupFile);
WriteLine($"{backupFile}:");
WriteLine($"  Contains {info.Length} bytes");
WriteLine($"  Last accessed {info.LastAccessTime}");
WriteLine($"  Has readonly attribute: {info.IsReadOnly}");
#endregion