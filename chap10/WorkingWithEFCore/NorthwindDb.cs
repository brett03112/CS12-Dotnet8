using Microsoft.EntityFrameworkCore; // To use DbContext
using Microsoft.EntityFrameworkCore.Diagnostics; // To use RelationalEventId

namespace Northwind.EntityModels;


/*
A class named Northwind will be used to represent the database. To use EF Core, the class must inherit
from DbContext. The DbContext class understands how to communicate with databases and dynamically 
generate SQL statements to query and manipulate data.
Your DbContext-derived class should have an overridden method named OnConfiguring, which will
set the database connection string.
*/

    public class NorthwindDb : DbContext
    {
        // These two properties map to tables in the database
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string databaseFile = "Northwind.db";
            string path = Path.Combine(Environment.CurrentDirectory, databaseFile);

            string connectionString = $"Data Source={path}";
            WriteLine($"Connection: {connectionString}");
            optionsBuilder.UseSqlite(connectionString);

            // This is the Console method
            optionsBuilder.LogTo(WriteLine, new[] { RelationalEventId.CommandExecuted })
            #if DEBUG
                .EnableSensitiveDataLogging() // Include SQL parameters in output
                .EnableDetailedErrors()
            #endif
            ;
            /*
            LogTo requires an Action<string> delegate. EF Core will call this delegate, passing a 
            string value for each log message. Passing the Console class WriteLine
            method, therefore, tells the logger to write each method to the console.
            */
        }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Example of using Fluent API instead of attribute annotations
        // to limit the length of a category name to 15 characters

        modelBuilder.Entity<Category>()
            .Property(category => category.CategoryName)
            .IsRequired() // Not null
            .HasMaxLength(15);

            // Some SQLite specific configuration due to not supporting decimal
            if (Database.ProviderName?.Contains("Sqlite") ?? false)
            {
                // To 'fix' the lack of decimal support in SQLite
                modelBuilder.Entity<Product>()
                    .Property(product => product.Cost)
                    .HasConversion<double>();
            }

            // A global filter to exclude discontinued products
            modelBuilder.Entity<Product>()
                .HasQueryFilter(p => !p.Discontinued);
    }
}
