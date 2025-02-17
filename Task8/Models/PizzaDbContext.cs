using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Task8.Models;

public partial class PizzaDbContext : DbContext
{
    public PizzaDbContext()
    {
    }

    public PizzaDbContext(DbContextOptions<PizzaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<PizzaSizes> PizzaSizes { get; set; }

    public virtual DbSet<Pizzas> Pizzas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=desktop-mv7qrea;Database=PizzaDb;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PizzaSizes>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PizzaSiz__3214EC07106C2E70");

            entity.HasOne(d => d.Pizza).WithMany(p => p.PizzaSizes)
                .HasForeignKey(d => d.PizzaId)
                .HasConstraintName("FK__PizzaSize__Pizza__412EB0B6");
        });

        modelBuilder.Entity<Pizzas>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pizzas__3214EC0759C86B02");

            entity.Property(e => e.ImageUrl).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
        
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
