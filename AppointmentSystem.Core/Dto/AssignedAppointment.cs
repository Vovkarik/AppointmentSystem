namespace AppointmentSystem.Core.Dto
{
	public class AssignedAppointment
	{
		public int Id { get; set; }
		public AssignedUser User { get; set; }
		public AvailableTimeSlot Slot { get; set; }
	}
}
