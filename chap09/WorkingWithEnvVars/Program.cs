
#region Working with Environment Variables

/*
Method                              Description
________________________________________________________________________________________________________________
GetEnvironmentVariables             Returns an IDictionary of all environment variables at a
                                    specified scope level or for the current process by default.

GetEnvironmentVariable              Returns the value for a named environment variable.

SetEnvironmentVariable              Sets the value for a named environment variable.

ExpandEnvironmentVariables          Converts any environment variables in a string to their values
                                    identified with %%. For example, "My computer is named %COMPUTER_NAME%".
*/
#endregion

#region Reading environment variables

SectionTitle("Reading all environment variables for process");
IDictionary vars = GetEnvironmentVariables();
DisctionaryToTable(vars);

SectionTitle("Reading all environment variables for the machine");
IDictionary varsMachine = GetEnvironmentVariables(EnvironmentVariableTarget.Machine);
DisctionaryToTable(varsMachine);

SectionTitle("Reading all environment variables for the user");
IDictionary varsUser = GetEnvironmentVariables(EnvironmentVariableTarget.User);
DisctionaryToTable(varsUser);

#endregion

#region Expanding, setting, and getting environment variables
/*
Scope Level             Command
____________________________________________________
Session/Shell           set MY_ENV_VAR="Alpha"

User                    setx MY_ENV_VAR "Beta"

Machine                 setx MY_ENV_VAR "Gamma" /M
*/

string myComputer = "My username is %USERNAME%. My CPU is %PROCESSOR_IDENTIFIER%.";

WriteLine(ExpandEnvironmentVariables(myComputer));

// Set a process scoped environment variable named MY_PASSWORD, get it, and output it
string password_key = "MY_PASSWORD";

SetEnvironmentVariable(password_key, "Pa$$w0rd");

string? password = GetEnvironmentVariable(password_key);
WriteLine($"{password_key} = {password}");
WriteLine();

// Try to get an environment variable named MY_SECRET at all 3 potential scope levels
string secret_key = "MY_SECRET";

string? secret = GetEnvironmentVariable(secret_key, EnvironmentVariableTarget.Process);
WriteLine($"Process - {secret_key} = {secret}");

secret = GetEnvironmentVariable(secret_key, EnvironmentVariableTarget.Machine);
WriteLine($"Machine - {secret_key} = {secret}");

secret = GetEnvironmentVariable(secret_key, EnvironmentVariableTarget.User);
WriteLine($"User - {secret_key} = {secret}");

#endregion