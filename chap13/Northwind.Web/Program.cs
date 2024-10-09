var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

#region COnfigure the HTTP pipeline and routes

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.MapGet("/", () => $"Environment is {app.Environment.EnvironmentName}");

#endregion

app.Run();


WriteLine("\n\n\n\nThis executes after the web server has stopped!");   
