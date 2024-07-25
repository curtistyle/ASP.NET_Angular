using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplicationGlosario.Models;

public partial class GlosarioContext : DbContext
{
    public GlosarioContext()
    {
    }

    public GlosarioContext(DbContextOptions<GlosarioContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Espanol> Espanols { get; set; }

    public virtual DbSet<Ingles> Ingles { get; set; }

/*   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
   {
    //optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB; Database=Glosario; Trusted_Connection=True;");

   }
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.*/

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Espanol>(entity =>
        {
            entity.HasKey(e => e.IdEspanol).HasName("PK__Espanol__8ACB23967ACF1073");

            entity.ToTable("Espanol");

            entity.Property(e => e.IdEspanol).HasColumnName("idEspanol");
            entity.Property(e => e.IdIngles).HasColumnName("idIngles");
            entity.Property(e => e.Palabra)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("palabra");

            entity.HasOne(d => d.IdInglesNavigation).WithMany(p => p.Espanols)
                .HasForeignKey(d => d.IdIngles)
                .HasConstraintName("FK__Espanol__idIngle__38996AB5");
        });

        modelBuilder.Entity<Ingles>(entity =>
        {
            entity.HasKey(e => e.IdIngles).HasName("PK__Ingles__B712B5674CC039B0");

            entity.Property(e => e.IdIngles).HasColumnName("idIngles");
            entity.Property(e => e.CategoriaGramatical)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("categoriaGramatical");
            entity.Property(e => e.Palabra)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("palabra");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
