#region Using while loop
int x = 0;

while (x < 10) 
{
    WriteLine(x);
    x++;
}
#endregion

#region Using a do-while loop

string? actualPassword = "Pa$$w0rd";
string? password;

do
{
    Write("Enter your password: ");
    password = ReadLine();
}
while (password != actualPassword); // This loop will run at least once and until the password is correct.

WriteLine("Correct!");

#endregion

#region Using a for loop

for (int i = 0; i < 10; i++)
{
    WriteLine(i);
}

for (int i = 0; i <=10; i+= 3)
{
    WriteLine(i);
}
#endregion

#region Using a foreach loop

string[] names = {"Adam", "Barry", "Charlie"};

foreach (string name in names)
{
    WriteLine($"{name} has {name.Length} characters in their name");
}

#endregion