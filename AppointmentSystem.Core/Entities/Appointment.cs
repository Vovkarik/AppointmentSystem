namespace AppointmentSystem.Core.Entities
{
	public class Appointment
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		public int AppointmentSlotId { get; set; }

		public virtual ApplicationUser User { get; set; }
		public virtual AppointmentSlot AppointmentSlot { get; set; }
	}
}
