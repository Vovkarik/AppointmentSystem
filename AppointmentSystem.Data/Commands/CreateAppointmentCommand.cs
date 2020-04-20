using CqrsSpirit;

namespace AppointmentSystem.Data.Commands
{
	public class CreateAppointmentCommand : ICommand
	{
		public string UserId { get; }
		public int TimeSlotId { get; }
		
		public CreateAppointmentCommand(string userId, int timeSlotId)
		{
			UserId = userId;
			TimeSlotId = timeSlotId;
		}
	}
}
