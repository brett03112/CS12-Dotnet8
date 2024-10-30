using Northwind.Blazor.Components;
using Northwind.Blazor.Services; // To use NorthwindServiceServerSide

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddNorthwindContext();

// Add services to the container.
builder.Services.AddRazorComponents();

// A transient service is one that creates a new instance for each request. 
builder.Services.AddTransient<INorthwindService, NorthwindServiceServerSide>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>();

app.Run();
