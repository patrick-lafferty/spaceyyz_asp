using SpaceYYZ.Models;
using Microsoft.EntityFrameworkCore;

namespace SpaceYYZ.Data
{
	public class DesignContext : DbContext
	{
		public DesignContext(DbContextOptions<DesignContext> options)
			: base(options)
		{
			
		}

		public DbSet<Engine> Engines {get; set;}
	}
}
