using System;

namespace AppointmentSystem.Models
{
	public class AvailableTimeSlot
	{
		public int Id { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
	}
}
