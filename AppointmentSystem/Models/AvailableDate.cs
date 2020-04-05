using System;

namespace AppointmentSystem.Models
{
	public enum DateAvailability
	{
		Free, HalfFull, Full
	}

	public class AvailableDate
	{
		public DateTime Date { get; set; }
		public DateAvailability Availability { get; set; }
	}
}
