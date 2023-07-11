using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Data;

namespace Nip.Models.Interfaces;

public class NipContext : DbContext
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  public NipContext(DbContextOptions<NipContext> options) : base(options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  {
  }
  public DbSet<TaxPayer> TaxPayers { get; set; }
  public DbSet<AccountNumber> AccountNumbers { get; set; }
  public DbSet<AuthorizedClerk> AuthorizedClerks { get; set; }
  public DbSet<Partner> Partners { get; set; }
  public DbSet<Representative> Representatives { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    //modelBuilder.Entity<TaxPayer>(eb =>
    //{
    //  eb.HasIndex(t => t.Name);
    //  eb.HasIndex(t => t.Nip);
    //  eb.HasIndex(t => t.Regon);
    //});

  }
  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
  }
  public virtual int SaveChanges()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is CreatedModel &&
                    e.State == EntityState.Added);

        foreach (var entityEntry in entries)
        {
          if (entityEntry.State == EntityState.Added)
            {
                ((CreatedModel)entityEntry.Entity).Created = DateTime.UtcNow;
            }
        }

        return base.SaveChanges();
    }

    public virtual async Task<int> SaveChangesAsync()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is CreatedModel &&
                    e.State == EntityState.Added);

        foreach (var entityEntry in entries)
        {
            if (entityEntry.State == EntityState.Added)
            {
                ((CreatedModel)entityEntry.Entity).Created = DateTime.UtcNow;
            }
        }
        return await base.SaveChangesAsync();
    }
}
