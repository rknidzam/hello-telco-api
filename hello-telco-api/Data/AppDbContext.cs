using hello_telco_api.Models;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<PhoneNumber> PhoneNumbers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Add seed data for the Customer entity
        modelBuilder.Entity<Customer>().HasData(
            new Customer { Id = 1, Name = "John Doe" },
            new Customer { Id = 2, Name = "Jane Doe" }
        );

        // Add seed data for the PhoneNumber entity
        modelBuilder.Entity<PhoneNumber>().HasData(
            new PhoneNumber { Id = 1, Number = "123-456-7890", Status = true, CustomerId = 1 },
            new PhoneNumber { Id = 2, Number = "987-654-3210", Status = false, CustomerId = 2 }
        );
    }
}