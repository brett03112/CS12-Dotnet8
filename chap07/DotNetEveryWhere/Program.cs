WriteLine("I can run everywhere!");

WriteLine($"OS version is {Environment.OSVersion}");

if (OperatingSystem.IsMacOS())
{
    WriteLine("I'm running on macOS!");
}

else if (OperatingSystem.IsWindowsVersionAtLeast(major: 10, build: 22000))
{
    WriteLine("I'm running on Windows 11!");
}

else if (OperatingSystem.IsWindowsVersionAtLeast(major: 10))
{
    WriteLine("I'm running on Windows 10!");
}

else
{
    WriteLine("I'm running some other OS!");
}
WriteLine("Press any key to exit...");
ReadKey(intercept: true); // do not output the key pressed

