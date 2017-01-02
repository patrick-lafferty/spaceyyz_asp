using System;
using SpaceYYZ.Data;
using SpaceYYZ.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SpaceYYZ
{
	public class Startup
	{
		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				.AddEnvironmentVariables();

			Configuration = builder.Build();
		}

		public IConfigurationRoot Configuration {get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddEntityFrameworkSqlServer()
				.AddDbContext<ApplicationUserContext>(options =>
						options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

			services.AddIdentity<ApplicationUser, IdentityRole>()
				.AddEntityFrameworkStores<ApplicationUserContext>()
				.AddDefaultTokenProviders();

			services.AddMvc();
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole(Configuration.GetSection("Logging"));

			app.UseStaticFiles();
			app.UseIdentity();
			
			app.UseMvc(routes =>
			{
				routes.MapRoute(
						name: "default",
						template: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
