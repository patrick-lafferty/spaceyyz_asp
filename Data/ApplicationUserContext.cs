using SpaceYYZ.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SpaceYYZ.Data
{
	public class ApplicationUserContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
	{
		public ApplicationUserContext(DbContextOptions<ApplicationUserContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
		}
	}
}
