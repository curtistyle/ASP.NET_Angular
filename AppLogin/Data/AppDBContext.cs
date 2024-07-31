using Microsoft.EntityFrameworkCore;
using AppLogin.Models;

namespace AppLogin.Data
{
	public class AppDBContext : DbContext
	{
        

        public AppDBContext(DbContextOptions<AppDBContext> options) :base(options)
        {
            
        }

        // definir las tablas:
        public DbSet<Usuario> Usuarios { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Usuario>(table =>
			{
				// Columna IdUsiario
				table.HasKey(col => col.IdUsuario);   // llave primaria 
				table.Property(col => col.IdUsuario)  //  
					.UseIdentityColumn()              // el id aumenta de 1 en 1
					.ValueGeneratedOnAdd();           // el id se autoincrementa cada vez que insertamos un usuario
				// Columna NombreCompleto
				table.Property(col => col.NombreCompleto)
					.HasMaxLength(50);                // maximo de caracteres que acepta NombreCompleto
				// Columna Correo
				table.Property(col => col.Correo);
				// Columna Clave
				table.Property(col => col.Clave);
			});

			modelBuilder.Entity<Usuario>().ToTable("Usuario"); // crea la tabla en la base de con el nombre "Usuario" y no con el nombre "Usuarios" del DBset


		}

	}
}
