using System.Threading.Tasks;
using AppointmentSystem.Core.Entities;
using CqrsSpirit;
using CqrsSpirit.Objects;

namespace AppointmentSystem.Data.Commands
{
	public class CreateAppointmentCommandHandler : ObjectCommandHandlerBase<CreateAppointmentCommand, ApplicationDbContext>, ICommandHandler<CreateAppointmentCommand>
	{
		public CreateAppointmentCommandHandler(ApplicationDbContext dbContext) : base(dbContext)
		{
		}

		public override async Task ExecuteAsync(CreateAppointmentCommand command)
		{
			var appointment = new Appointment
			{
				UserId = command.UserId,
				AppointmentSlotId = command.TimeSlotId
			};

			await DbContext.Appointments.AddAsync(appointment);
		}
	}
}
