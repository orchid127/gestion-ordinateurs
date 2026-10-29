using GestionOrdinateurs.Shared;
using Microsoft.EntityFrameworkCore;

namespace GestionOrdinateurs.API;

public class ParcInformatiqueContext : DbContext
{
    public DbSet<Ordinateur> Ordinateurs { get; set; }

    public DbSet<Reparation> Reparations { get; set; }

    public DbSet<User> Users { get; set; }

    public ParcInformatiqueContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=parcinfo.db");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Ordinateur>()
            .OwnsOne(o => o.Caracteristiques);

        modelBuilder.Entity<Ordinateur>()
            .HasMany(o => o.Reparations)
            .WithOne(r => r.UnOrdinateur);
    }
}