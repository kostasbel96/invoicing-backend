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
    public virtual DbSet<ServiceItem> ServiceItems { get; set; }
    public virtual DbSet<Invoice> Invoices { get; set; }
    public virtual DbSet<InvoiceItem> InvoiceItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Region>().HasData(
            new Region
            {
                Id = 1,
                Uuid = Guid.NewGuid(),
                Name = "ΑΤΤΙΚΗ",
                Code = "AT",
                InsertedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            },
            new Region
            {
                Id = 2,
                Uuid = Guid.NewGuid(),
                Name = "ΘΕΣΣΑΛΙΑ",
                Code = "TH",
                InsertedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            },
            new Region
            {
                Id = 3,
                Uuid = Guid.NewGuid(),
                Name = "ΜΑΚΕΔΟΝΙΑ",
                Code = "MA",
                InsertedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            },
            new Region
            {
                Id = 4,
                Uuid = Guid.NewGuid(),
                Name = "ΠΕΛΟΠΟΝΝΗΣΟΣ",
                Code = "PE",
                InsertedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            },
            new Region
            {
                Id = 5,
                Uuid = Guid.NewGuid(),
                Name = "ΚΡΗΤΗ",
                Code = "CR",
                InsertedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            },
            new Region
            {
                Id = 6,
                Uuid = Guid.NewGuid(),
                Name = "ΙΟΝΙΟ",
                Code = "IO",
                InsertedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            },
            new Region
            {
                Id = 7,
                Uuid = Guid.NewGuid(),
                Name = "ΑΝΑΤΟΛΙΚΟ ΑΙΓΑΙΟ",
                Code = "EA",
                InsertedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            },
            new Region
            {
                Id = 8,
                Uuid = Guid.NewGuid(),
                Name = "ΔΥΤΙΚΗ ΕΛΛΑΔΑ",
                Code = "WG",
                InsertedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            },
            new Region
            {
                Id = 9,
                Uuid = Guid.NewGuid(),
                Name = "ΚΕΝΤΡΙΚΗ ΕΛΛΑΔΑ",
                Code = "CG",
                InsertedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            },
            new Region
            {
                Id = 10,
                Uuid = Guid.NewGuid(),
                Name = "ΒΟΡΕΙΟ ΑΙΓΑΙΟ",
                Code = "NA",
                InsertedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            }
        );
        
        modelBuilder.Entity<TaxOffice>().HasData(
            new TaxOffice { Id = 1, Uuid = Guid.NewGuid(), Name = "ΔΟΥ Α' ΑΘΗΝΩΝ", Code = "ATH1", IsActive = true, InsertedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow },
            new TaxOffice { Id = 2, Uuid = Guid.NewGuid(), Name = "ΔΟΥ Β' ΑΘΗΝΩΝ", Code = "ATH2", IsActive = true, InsertedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow },
            new TaxOffice { Id = 3, Uuid = Guid.NewGuid(), Name = "ΔΟΥ ΓΛΥΦΑΔΑΣ", Code = "GLY", IsActive = true, InsertedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow },
            new TaxOffice { Id = 4, Uuid = Guid.NewGuid(), Name = "ΔΟΥ ΑΜΑΡΟΥΣΙΟΥ", Code = "AMA", IsActive = true, InsertedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow },
            new TaxOffice { Id = 5, Uuid = Guid.NewGuid(), Name = "ΔΟΥ ΑΓΙΩΝ ΑΝΑΡΓΥΡΩΝ", Code = "AAN", IsActive = true, InsertedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow },
            new TaxOffice { Id = 6, Uuid = Guid.NewGuid(), Name = "ΔΟΥ ΧΟΛΑΡΓΟΥ", Code = "HOL", IsActive = true, InsertedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow },
            new TaxOffice { Id = 7, Uuid = Guid.NewGuid(), Name = "ΔΟΥ ΚΗΦΙΣΙΑΣ", Code = "KIF", IsActive = true, InsertedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow },
            new TaxOffice { Id = 8, Uuid = Guid.NewGuid(), Name = "ΔΟΥ ΝΕΑΣ ΙΩΝΙΑΣ", Code = "NIO", IsActive = true, InsertedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow },
            new TaxOffice { Id = 9, Uuid = Guid.NewGuid(), Name = "ΔΟΥ ΠΑΛΛΗΝΗΣ", Code = "PAL", IsActive = true, InsertedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow },
            new TaxOffice { Id = 10, Uuid = Guid.NewGuid(), Name = "ΔΟΥ ΠΕΡΙΣΤΕΡΙΟΥ", Code = "PER", IsActive = true, InsertedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow },

            new TaxOffice { Id = 11, Uuid = Guid.NewGuid(), Name = "ΔΟΥ ΑΙΓΑΛΕΩ", Code = "AIG", IsActive = true, InsertedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow },
            new TaxOffice { Id = 12, Uuid = Guid.NewGuid(), Name = "ΔΟΥ ΚΑΛΛΙΘΕΑΣ", Code = "KAL", IsActive = true, InsertedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow },
            new TaxOffice { Id = 13, Uuid = Guid.NewGuid(), Name = "ΔΟΥ ΠΕΙΡΑΙΑ", Code = "PEI", IsActive = true, InsertedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow },
            new TaxOffice { Id = 14, Uuid = Guid.NewGuid(), Name = "ΔΟΥ ΝΙΚΑΙΑΣ", Code = "NIK", IsActive = true, InsertedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow },

            new TaxOffice { Id = 15, Uuid = Guid.NewGuid(), Name = "ΔΟΥ ΘΕΣΣΑΛΟΝΙΚΗΣ", Code = "THE", IsActive = true, InsertedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow },
            new TaxOffice { Id = 16, Uuid = Guid.NewGuid(), Name = "ΔΟΥ ΚΑΛΑΜΑΡΙΑΣ", Code = "KAM", IsActive = true, InsertedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow },
            new TaxOffice { Id = 17, Uuid = Guid.NewGuid(), Name = "ΔΟΥ ΑΜΠΕΛΟΚΗΠΩΝ", Code = "AMP", IsActive = true, InsertedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow },

            new TaxOffice { Id = 18, Uuid = Guid.NewGuid(), Name = "ΔΟΥ ΑΛΕΞΑΝΔΡΟΥΠΟΛΗΣ", Code = "ALE", IsActive = true, InsertedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow },
            new TaxOffice { Id = 19, Uuid = Guid.NewGuid(), Name = "ΔΟΥ ΔΡΑΜΑΣ", Code = "DRA", IsActive = true, InsertedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow },
            new TaxOffice { Id = 20, Uuid = Guid.NewGuid(), Name = "ΔΟΥ ΚΑΒΑΛΑΣ", Code = "KAV", IsActive = true, InsertedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow },
            new TaxOffice { Id = 21, Uuid = Guid.NewGuid(), Name = "ΔΟΥ ΞΑΝΘΗΣ", Code = "XAN", IsActive = true, InsertedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow },
            new TaxOffice { Id = 22, Uuid = Guid.NewGuid(), Name = "ΔΟΥ ΚΟΜΟΤΗΝΗΣ", Code = "KOM", IsActive = true, InsertedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow },

            new TaxOffice { Id = 23, Uuid = Guid.NewGuid(), Name = "ΔΟΥ ΙΩΑΝΝΙΝΩΝ", Code = "IOA", IsActive = true, InsertedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow },
            new TaxOffice { Id = 24, Uuid = Guid.NewGuid(), Name = "ΔΟΥ ΑΡΤΑΣ", Code = "ART", IsActive = true, InsertedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow },
            new TaxOffice { Id = 25, Uuid = Guid.NewGuid(), Name = "ΔΟΥ ΠΡΕΒΕΖΑΣ", Code = "PRE", IsActive = true, InsertedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow },
            new TaxOffice { Id = 26, Uuid = Guid.NewGuid(), Name = "ΔΟΥ ΗΓΟΥΜΕΝΙΤΣΑΣ", Code = "IGO", IsActive = true, InsertedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow },

            new TaxOffice { Id = 27, Uuid = Guid.NewGuid(), Name = "ΔΟΥ ΛΑΡΙΣΑΣ", Code = "LAR", IsActive = true, InsertedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow },
            new TaxOffice { Id = 28, Uuid = Guid.NewGuid(), Name = "ΔΟΥ ΒΟΛΟΥ", Code = "VOL", IsActive = true, InsertedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow },
            new TaxOffice { Id = 29, Uuid = Guid.NewGuid(), Name = "ΔΟΥ ΤΡΙΚΑΛΩΝ", Code = "TRI", IsActive = true, InsertedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow },
            new TaxOffice { Id = 30, Uuid = Guid.NewGuid(), Name = "ΔΟΥ ΚΑΡΔΙΤΣΑΣ", Code = "KAR", IsActive = true, InsertedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow },

            new TaxOffice { Id = 31, Uuid = Guid.NewGuid(), Name = "ΔΟΥ ΠΑΤΡΩΝ", Code = "PAT", IsActive = true, InsertedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow },
            new TaxOffice { Id = 32, Uuid = Guid.NewGuid(), Name = "ΔΟΥ ΑΓΡΙΝΙΟΥ", Code = "AGR", IsActive = true, InsertedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow },
            new TaxOffice { Id = 33, Uuid = Guid.NewGuid(), Name = "ΔΟΥ ΠΥΡΓΟΥ", Code = "PYR", IsActive = true, InsertedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow },
            new TaxOffice { Id = 34, Uuid = Guid.NewGuid(), Name = "ΔΟΥ ΜΕΣΟΛΟΓΓΙΟΥ", Code = "MES", IsActive = true, InsertedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow },

            new TaxOffice { Id = 35, Uuid = Guid.NewGuid(), Name = "ΔΟΥ ΚΟΡΙΝΘΟΥ", Code = "KOR", IsActive = true, InsertedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow },
            new TaxOffice { Id = 36, Uuid = Guid.NewGuid(), Name = "ΔΟΥ ΑΡΓΟΥΣ", Code = "ARG", IsActive = true, InsertedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow },
            new TaxOffice { Id = 37, Uuid = Guid.NewGuid(), Name = "ΔΟΥ ΝΑΥΠΛΙΟΥ", Code = "NAF", IsActive = true, InsertedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow },
            new TaxOffice { Id = 38, Uuid = Guid.NewGuid(), Name = "ΔΟΥ ΣΠΑΡΤΗΣ", Code = "SPA", IsActive = true, InsertedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow },
            new TaxOffice { Id = 39, Uuid = Guid.NewGuid(), Name = "ΔΟΥ ΤΡΙΠΟΛΗΣ", Code = "TRP", IsActive = true, InsertedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow },

            new TaxOffice { Id = 40, Uuid = Guid.NewGuid(), Name = "ΔΟΥ ΗΡΑΚΛΕΙΟΥ", Code = "HER", IsActive = true, InsertedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow },
            new TaxOffice { Id = 41, Uuid = Guid.NewGuid(), Name = "ΔΟΥ ΧΑΝΙΩΝ", Code = "CHA", IsActive = true, InsertedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow },
            new TaxOffice { Id = 42, Uuid = Guid.NewGuid(), Name = "ΔΟΥ ΡΕΘΥΜΝΟΥ", Code = "RET", IsActive = true, InsertedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow },
            new TaxOffice { Id = 43, Uuid = Guid.NewGuid(), Name = "ΔΟΥ ΑΓΙΟΥ ΝΙΚΟΛΑΟΥ", Code = "AGN", IsActive = true, InsertedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow },

            new TaxOffice { Id = 44, Uuid = Guid.NewGuid(), Name = "ΔΟΥ ΡΟΔΟΥ", Code = "ROD", IsActive = true, InsertedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow },
            new TaxOffice { Id = 45, Uuid = Guid.NewGuid(), Name = "ΔΟΥ ΚΩ", Code = "KOS", IsActive = true, InsertedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow },
            new TaxOffice { Id = 46, Uuid = Guid.NewGuid(), Name = "ΔΟΥ ΜΥΤΙΛΗΝΗΣ", Code = "MYT", IsActive = true, InsertedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow },
            new TaxOffice { Id = 47, Uuid = Guid.NewGuid(), Name = "ΔΟΥ ΧΙΟΥ", Code = "CHI", IsActive = true, InsertedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow },
            new TaxOffice { Id = 48, Uuid = Guid.NewGuid(), Name = "ΔΟΥ ΣΑΜΟΥ", Code = "SAM", IsActive = true, InsertedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow }
        );
        
        modelBuilder.Entity<VatRate>().HasData(
            new VatRate
            {
                Id = 1,
                Uuid = Guid.NewGuid(),
                Name = "24%",
                Rate = 24m,
                IsActive = true,
                InsertedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            },
            new VatRate
            {
                Id = 2,
                Uuid = Guid.NewGuid(),
                Name = "17%",
                Rate = 17m,
                IsActive = true,
                InsertedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            },
            new VatRate
            {
                Id = 3,
                Uuid = Guid.NewGuid(),
                Name = "13%",
                Rate = 13m,
                IsActive = true,
                InsertedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            },
            new VatRate
            {
                Id = 4,
                Uuid = Guid.NewGuid(),
                Name = "6%",
                Rate = 6m,
                IsActive = true,
                InsertedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            },
            new VatRate
            {
                Id = 5,
                Uuid = Guid.NewGuid(),
                Name = "0%",
                Rate = 0m,
                IsActive = true,
                InsertedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            }
        );

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasOne(c => c.Region)
                .WithMany(r => r.Customers)
                .HasForeignKey(c => c.RegionId);
            
            entity.HasOne(c => c.TaxOffice)
                .WithMany()
                .HasForeignKey(c => c.TaxOfficeId);

            entity.HasIndex(c => c.Email).IsUnique().HasFilter("\"IsActive\" = true");;
            entity.HasIndex(c => c.Phone).IsUnique().HasFilter("\"IsActive\" = true");;
            entity.HasIndex(c => c.Vat).IsUnique().HasFilter("\"IsActive\" = true");;
            entity.HasIndex(c => new { c.Firstname, c.Lastname });
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasOne(i => i.Customer)
                .WithMany()
                .HasForeignKey(i => i.CustomerId);

            entity.HasIndex(i => i.InvoiceNumber).IsUnique().HasFilter("\"IsActive\" = true");;
        });

        modelBuilder.Entity<InvoiceItem>(entity =>
        {
            entity.HasOne(ii => ii.Invoice)
                .WithMany(i => i.Items)
                .HasForeignKey(ii => ii.InvoiceId);

            entity.HasOne(ii => ii.Product)
                .WithMany(p => p.InvoiceItems)
                .HasForeignKey(ii => ii.ProductId);

            entity.HasOne(ii => ii.ServiceItem)
                .WithMany(s => s.InvoiceItems)
                .HasForeignKey(ii => ii.ServiceItemId);
        });
        
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasIndex(i => i.Name).IsUnique().HasFilter("\"IsActive\" = true");;
        });
        
        modelBuilder.Entity<ServiceItem>(entity =>
        {
            entity.HasIndex(i => i.Name).IsUnique().HasFilter("\"IsActive\" = true");;
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