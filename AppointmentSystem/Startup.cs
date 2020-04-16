using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AppointmentSystem.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AppointmentSystem.Middleware;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using AppointmentSystem.Services;
using Microsoft.EntityFrameworkCore;

namespace AppointmentSystem
{
	public class Startup
	{
		public Startup(IConfiguration configuration, IWebHostEnvironment environment)
		{
			Configuration = configuration;
			Environment = environment;
		}

		public IConfiguration Configuration { get; }
		public IWebHostEnvironment Environment { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddHttpContextAccessor();

			services.AddSession(options =>
			{
				options.IdleTimeout = TimeSpan.FromMinutes(10);
				options.Cookie.Name = ".AppointmentSystem";
				options.Cookie.Path = "/";
				options.Cookie.HttpOnly = true;
				options.Cookie.IsEssential = true;
				options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Lax;
				options.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.SameAsRequest;
			});

			var builder = services.AddRazorPages()
				.AddJsonOptions(o =>
				{
					o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
				}).AddSessionStateTempDataProvider();

#if DEBUG
			if(Environment.IsDevelopment())
			{
				builder.AddRazorRuntimeCompilation();
			}
#endif

			services.AddDbContext<AppointmentContext>(o => o.UseNpgsql(Configuration.GetConnectionString("Database")));
			services.AddIdentity<ApplicationUser, IdentityRole>(o =>
			{
				o.User.AllowedUserNameCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+-()";
			})
				.AddEntityFrameworkStores<AppointmentContext>()
				.AddDefaultTokenProviders();

			services.AddAuthentication(o =>
			{
				o.DefaultScheme = AuthenticationSchemes.User;
				o.DefaultAuthenticateScheme = AuthenticationSchemes.User;
			})
				.AddCookie(AuthenticationSchemes.User, o =>
				{
					o.LoginPath = "/User/Login";
				});

			// TODO: uncomment this once admin panel is up.
			//.AddCookie(AuthenticationSchemes.Administrator, o =>
			//{
			//	o.LoginPath = "";
			//});

			services.AddSingleton<ISmsService, SmsService>();
			services.AddTransient<IUserAuthenticationService, UserAuthenticationService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IServiceProvider serviceProvider)
		{
			if(Environment.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseBrowserLink();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseHttpContextItemsMiddleware();
			app.UseRouting();

			app.UseCookiePolicy();
			app.UseAuthentication();
			app.UseAuthorization();

			app.UseSession();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapRazorPages();
			});

			CreateRoles(serviceProvider).Wait();
		}

		private async Task CreateRoles(IServiceProvider serviceProvider)
		{
			var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
			var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
			string[] roles = { Roles.User, Roles.Administrator };

			foreach(string role in roles)
			{
				if(!await roleManager.RoleExistsAsync(role))
				{
					await roleManager.CreateAsync(new IdentityRole(role));
				}
			}
		}
	}
}
