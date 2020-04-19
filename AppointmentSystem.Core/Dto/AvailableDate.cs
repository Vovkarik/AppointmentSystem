using System;

namespace AppointmentSystem.Core.Dto
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
