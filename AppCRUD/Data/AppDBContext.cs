using Microsoft.EntityFrameworkCore;
using AppCRUD.Models;

namespace AppCRUD.Data
{
	public class AppDBContext : DbContext
	{
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }

        public DbSet<Empleado> Empleados { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Empleado>(table =>
			{
				// idEmpleado INT PRIMARY KEY IDENTITY(1,1)
				table.HasKey(col => col.IdEmpleado);
				
				table.Property(col => col.IdEmpleado)
				.UseIdentityColumn()
				.ValueGeneratedOnAdd();

				table.Property(col => col.NombreCompleto).HasMaxLength(50);
				table.Property(col => col.Correo).HasMaxLength(50);
			});

			modelBuilder.Entity<Empleado>().ToTable("Empleado");
		}
	}
}
