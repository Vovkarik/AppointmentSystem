namespace AppointmentSystem.Database
{
	public class Appointment
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public int AppointmentSlotId { get; set; }
	}
}
