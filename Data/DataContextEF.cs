
// Installations Required:

// 1) Microsoft.EntityFrameworkCore
// This is the main EF Core package. It provides the fundamental functionality of Entity Framework Core.

// It includes:
// DbContext – manages database interactions.
// DbSet<T> – represents database tables.
// ModelBuilder – configures your entity models.
// DbContextOptionsBuilder – configures your database context.
// LINQ query support.
// Change tracking.

// Command:
// dotnet add package Microsoft.EntityFrameworkCore --version 10.0.14

// 2) Microsoft.EntityFrameworkCore.Relational
// This package provides functionality that allows EF Core to work with relational databases.

// It provides functionality such as:
// Database table mapping.
// Schema configuration.
// Primary key and foreign key configuration.
// Database transactions.
// Relational query support.
// Migrations-related relational functionality.

// Command:
// dotnet add package Microsoft.EntityFrameworkCore.Relational --version 10.0.14

// 3) Microsoft.EntityFrameworkCore.SqlServer
// This package allows Entity Framework Core to communicate with Microsoft SQL Server.

// It provides:
// SQL Server database connectivity.
// SQL Server-specific SQL generation.
// SQL Server data type mappings.
// SQL Server-specific database configuration.
// Support for SQL Server features.

// Command:
// dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 10.0.14

using DotnetApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetApi.Data
{
    // DbContext is the main class provided by Entity Framework Core.
    // It acts as a bridge between our application and the database.
    public class DataContextEF : DbContext
    {
        // IConfiguration allows us to access application configuration,
        // including connection strings from appsettings.json.
        private readonly IConfiguration _config;

        // Constructor: Receives IConfiguration through Dependency Injection.
        public DataContextEF(IConfiguration config)
        {
            _config = config;
        }

        // DbSet represents a database table.
        // Users represents the Users table in SQL Server.
        public virtual DbSet<User> Users { get; set; }

        // Represents the UserSalary table.
        public virtual DbSet<UserSalary> UserSalary { get; set; }

        // Represents the UserJobInfo table.
        public virtual DbSet<UserJobInfo> UserJobInfo { get; set; }

        // OnConfiguring is used to configure the database connection.
        // EF Core calls this method when configuring the DbContext.
        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            // Check whether the DbContext has already been configured.
            if (!optionBuilder.IsConfigured)
            {
                // Configure SQL Server as our database provider.
                optionBuilder.UseSqlServer(
                    _config.GetConnectionString("DefaultConnection"),

                    // Enable automatic retries when certain transient
                    // database connection failures occur.
                    sqlOptions => sqlOptions.EnableRetryOnFailure()
                );
            }
        }

        // OnModelCreating is used to configure how our C# models
        // map to database tables, columns, keys, and relationships.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Set Project1AppSchema as the default database schema.
            modelBuilder.HasDefaultSchema("Project1AppSchema");

            // Configure the User model.
            modelBuilder.Entity<User>()

                // Map User model to the Users table.
                .ToTable("Users", "Project1AppSchema")

                // Set UserId as the primary key.
                .HasKey(u => u.UserId);

            // Configure the UserSalary model.
            modelBuilder.Entity<UserSalary>()

                // Map UserSalary model to the UserSalary table.
                .ToTable("UserSalary", "Project1AppSchema")

                // Set UserId as the primary key.
                .HasKey(u => u.UserId);

            // Configure the UserJobInfo model.
            modelBuilder.Entity<UserJobInfo>()

                // Map UserJobInfo model to the UserJobInfo table.
                .ToTable("UserJobInfo", "Project1AppSchema")

                // Set UserId as the primary key.
                .HasKey(u => u.UserId);
        }
    }
}
