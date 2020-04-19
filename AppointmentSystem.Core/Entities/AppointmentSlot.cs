using System;
using System.ComponentModel;

namespace AppointmentSystem.Core.Entities
{
	public class AppointmentSlot
	{
		public int Id { get; set; }
		public int DoctorId { get; set; }
		[DisplayName("Время начала")]
		public DateTime StartTime { get; set; }
		[DisplayName("Время окончания")]
		public DateTime EndTime { get; set; }

		[DisplayName("Врач")]
		public virtual Doctor Doctor { get; set; }
		public virtual Appointment Appointment { get; set; }
	}
}
