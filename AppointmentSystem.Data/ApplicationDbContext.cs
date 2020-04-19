using AppointmentSystem.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AppointmentSystem.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}

		public DbSet<DoctorCategory> DoctorCategories { get; set; }
		public DbSet<Doctor> Doctors { get; set; }
		public DbSet<AppointmentSlot> AppointmentSlots { get; set; }
		public DbSet<Appointment> Appointments { get; set; }
	}
}
