using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Northwind.EntityModels;

/*
    Class Definition: The NorthwindContext class is a partial class that inherits from DbContext, which is a base class 
        for Entity Framework Core. It represents a database context for the Northwind database.

    Methods:

    Constructors:

    *   NorthwindContext(): An empty constructor that does nothing.

    *   NorthwindContext(DbContextOptions<NorthwindContext> options): Creates a new instance of the context with the 
        specified options.

    DbSet Properties:

    *   Categories, Customers, Employees, etc.: These properties represent the tables in the Northwind database and provide 
        access to the data in those tables.

    OnConfiguring:

    *   Configures the DbContextOptionsBuilder to use the Northwind.db database file in the root directory of the project. 
        If the file does not exist, it throws a FileNotFoundException.

    OnModelCreating:

    *   Configures the model that Code First uses to map the database. It sets default values for certain properties and 
        sets up relationships between tables.

    OnModelCreatingPartial:

    *   A partial method that allows for additional configuration of the model. It is called at the end of the 
        OnModelCreating method. Overall, this class provides a way to interact with the Northwind database 
        using Entity Framework Core.
        
*/
public partial class NorthwindContext : DbContext
{

    /// <summary>
    /// Initializes a new instance of the <see cref="NorthwindContext"/> class.
    /// </summary>
    public NorthwindContext()
    {
    }

    /// <summary>
    /// Creates a new instance of a NorthwindContext.
    /// </summary>
    /// <param name="options">The options for this context.</param>
    public NorthwindContext(DbContextOptions<NorthwindContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeTerritory> EmployeeTerritories { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Shipper> Shippers { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<Territory> Territories { get; set; }


        /// <summary>
        /// Configures the DbContextOptionsBuilder used to create a DbContext
        /// </summary>
        /// <param name="optionsBuilder">The DbContextOptionsBuilder used to create a DbContext</param>
        /// <remarks>
        /// Configures the DbContextOptionsBuilder to use the Northwind.db database file
        /// found in the root directory of the project.
        /// If the file does not exist, a FileNotFoundException is thrown.
        /// The path to the database file is logged to the console when the DbContext
        /// is created.
        /// </remarks>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            string database = "Northwind.db";
            string dir = Environment.CurrentDirectory;
            string path = string.Empty;

            if (dir.EndsWith("net8.0"))
            {
                // In the <project>/bin/Debug | Release/net8.0 directory
                path = Path.Combine(dir, "..", "..", "..", "..", "..", database);
            }
            else
            {
                // In the <project> directory
                path = Path.Combine("..", database);
            }

            path = Path.GetFullPath(path); // Convert to absolute path
            NorthwindContextLogger.WriteLine($"Database path: {path}");

            if (!File.Exists(path))
            {
                throw new FileNotFoundException(message: $"{path} not found.", fileName: path);
            }

            optionsBuilder.UseSqlite($"Filename={path}");

            optionsBuilder.LogTo(NorthwindContextLogger.WriteLine, new[]
                { Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.CommandExecuting });
        }
    }

        /// <summary>
        /// Configures the model that Code First uses to map the database
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the database.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.Property(e => e.Freight).HasDefaultValueSql("0");
        });
        
        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.Property(e => e.Quantity).HasDefaultValueSql("1");

            entity.Property(e => e.UnitPrice).HasDefaultValueSql("0");

            entity.HasOne(d => d.Order).WithMany(p =>
                p.OrderDetails).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Product).WithMany(p =>
                p.OrderDetails).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.Discontinued).HasDefaultValueSql("0");

            entity.Property(e => e.ReorderLevel).HasDefaultValueSql("0");

            entity.Property(e => e.UnitPrice).HasDefaultValueSql("0");
            
            entity.Property(e => e.UnitsInStock).HasDefaultValueSql("0");
            
            entity.Property(e => e.UnitsOnOrder).HasDefaultValueSql("0");
            
            entity.Property(product => product.UnitPrice).HasConversion<double>();
        });
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
