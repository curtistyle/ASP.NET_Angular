using Microsoft.EntityFrameworkCore;


namespace DB
{
	public class BarContext : DbContext
	{
		protected BarContext(DbContextOptions<BarContext> options)
			: base(options)
		{
			
		}

		public DbSet<Beer> Beers { get; set; }

		public DbSet<Brand> Brands { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Brand>().ToTable("Brand");
			modelBuilder.Entity<Beer>().ToTable("Beer");
		}

	}
}
