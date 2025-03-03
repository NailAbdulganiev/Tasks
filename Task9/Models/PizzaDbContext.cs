using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Task9.Models;

public partial class PizzaDbContext : DbContext
{
    public PizzaDbContext()
    {
    }

    public PizzaDbContext(DbContextOptions<PizzaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Pizza> Pizzas { get; set; }

    public virtual DbSet<PizzaSize> PizzaSizes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=PizzaDb;Username=postgres;Password=Nail");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pizza>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pizzas__3214EC07910AB8F5");

            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.ImageUrl).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<PizzaSize>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PizzaSiz__3214EC073474BCAF");

            entity.HasOne(d => d.Pizza).WithMany(p => p.PizzaSizes)
                .HasForeignKey(d => d.PizzaId)
                .HasConstraintName("FK__PizzaSize__Pizza__398D8EEE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
