using System;

namespace AppointmentSystem.Database
{
	public class AppointmentSlot
	{
		public int Id { get; set; }
		public int ServiceId { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
	}
}
