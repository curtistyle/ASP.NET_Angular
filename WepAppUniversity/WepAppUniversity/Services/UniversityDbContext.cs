using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebAppUniversity.Services
{
	public class UniversityDbContext : IdentityDbContext
	{
		public UniversityDbContext(DbContextOptions options) : base(options)
		{

		}


	}
}
