using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AppointmentSystem.Database
{
	public class AppointmentContext : IdentityDbContext<ApplicationUser>
	{
		public DbSet<DoctorCategory> DoctorCategories { get; set; }
		public DbSet<Doctor> Doctors { get; set; }
		public DbSet<Service> Services { get; set; }
		public DbSet<AppointmentSlot> AppointmentSlots { get; set; }
		public DbSet<Appointment> Appointments { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite("Filename=appointment.db");
		}
	}
}
