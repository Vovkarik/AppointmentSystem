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
			services.AddDistributedMemoryCache();

			services.AddSession(options =>
			{
				options.IdleTimeout = TimeSpan.FromMinutes(10);
				options.Cookie.HttpOnly = true;
				options.Cookie.IsEssential = true;
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

			services.AddDbContext<AppointmentContext>();
			services.AddIdentity<ApplicationUser, IdentityRole>()
				.AddEntityFrameworkStores<AppointmentContext>()
				.AddDefaultTokenProviders();
			services.AddAuthentication(o =>
			{
				o.DefaultScheme = AuthenticationSchemes.User;
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
		public void Configure(IApplicationBuilder app)
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

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseSession();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapRazorPages();
			});
		}
	}
}
