using Microsoft.EntityFrameworkCore;

namespace Invoicing_Backend.Data;

public class InvoicingAppDbContext : DbContext
{
    public InvoicingAppDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public virtual DbSet<Region> Regions { get; set; }
    public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Invoice> Invoices { get; set; }
    public virtual DbSet<InvoiceItem> InvoiceItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Region>().HasData(
            new Region
            {
                Id = 1,
                Uuid = Guid.NewGuid(),
                Name = "Αττική",
                Code = "AT",
                InsertedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            },
            new Region
            {
                Id = 2,
                Uuid = Guid.NewGuid(),
                Name = "Θεσσαλία",
                Code = "TH",
                InsertedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            },
            new Region
            {
                Id = 3,
                Uuid = Guid.NewGuid(),
                Name = "Μακεδονία",
                Code = "MA",
                InsertedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            },
            new Region
            {
                Id = 4,
                Uuid = Guid.NewGuid(),
                Name = "Πελοπόννησος",
                Code = "PE",
                InsertedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            },
            new Region
            {
                Id = 5,
                Uuid = Guid.NewGuid(),
                Name = "Κρήτη",
                Code = "CR",
                InsertedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            },
            new Region
            {
                Id = 6,
                Uuid = Guid.NewGuid(),
                Name = "Ιόνιο",
                Code = "IO",
                InsertedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            },
            new Region
            {
                Id = 7,
                Uuid = Guid.NewGuid(),
                Name = "Ανατολικό Αιγαίο",
                Code = "EA",
                InsertedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            },
            new Region
            {
                Id = 8,
                Uuid = Guid.NewGuid(),
                Name = "Δυτική Ελλάδα",
                Code = "WG",
                InsertedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            },
            new Region
            {
                Id = 9,
                Uuid = Guid.NewGuid(),
                Name = "Κεντρική Ελλάδα",
                Code = "CG",
                InsertedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            },
            new Region
            {
                Id = 10,
                Uuid = Guid.NewGuid(),
                Name = "Βόρειο Αιγαίο",
                Code = "NA",
                InsertedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            }
        );

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasOne(c => c.Region)
                .WithMany(r => r.Customers)
                .HasForeignKey(c => c.RegionId);

            entity.HasIndex(c => c.Email).IsUnique();
            entity.HasIndex(c => c.Phone).IsUnique();
            entity.HasIndex(c => c.Vat).IsUnique();
            entity.HasIndex(c => new { c.Firstname, c.Lastname });
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasOne(i => i.Customer)
                .WithMany()
                .HasForeignKey(i => i.CustomerId);

            entity.HasIndex(i => i.InvoiceNumber).IsUnique();
        });

        modelBuilder.Entity<InvoiceItem>(entity =>
        {
            entity.HasOne(ii => ii.Invoice)
                .WithMany(i => i.Items)
                .HasForeignKey(ii => ii.InvoiceId);

            entity.HasOne(ii => ii.Product)
                .WithMany(p => p.InvoiceItems)
                .HasForeignKey(ii => ii.ProductId);
        });
        
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasIndex(p => p.Name).IsUnique();
        });
    }
    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<BaseEntity>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.Uuid = Guid.NewGuid();
                entry.Entity.InsertedAt = DateTime.UtcNow;
                entry.Entity.ModifiedAt = DateTime.UtcNow;
                entry.Entity.IsActive = true;
            }

            if (entry.State == EntityState.Modified)
                entry.Entity.ModifiedAt = DateTime.UtcNow;
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}