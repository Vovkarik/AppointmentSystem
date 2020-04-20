﻿namespace AppointmentSystem.Core.Entities
{
	public class Appointment
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public int AppointmentSlotId { get; set; }

		public virtual AppointmentSlot AppointmentSlot { get; set; }
	}
}