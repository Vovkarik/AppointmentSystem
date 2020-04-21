using System.Linq;
using System.Threading.Tasks;
using AppointmentSystem.Core.Entities;
using CqrsSpirit;
using CqrsSpirit.Objects;
using Microsoft.EntityFrameworkCore;

namespace AppointmentSystem.Data.Commands
{
	public class CreateAppointmentCommandHandler : ObjectCommandHandlerBase<CreateAppointmentCommand, ApplicationDbContext>, ICommandHandler<CreateAppointmentCommand>
	{
		public CreateAppointmentCommandHandler(ApplicationDbContext dbContext) : base(dbContext)
		{
		}

		public override async Task ExecuteAsync(CreateAppointmentCommand command)
		{
			if(await DbContext.Appointments.AnyAsync(appointment => appointment.AppointmentSlotId == command.TimeSlotId))
			{
				throw new System.Exception("AppointmentSlot already booked");
			}

			var appointment = new Appointment
			{
				UserId = command.UserId,
				AppointmentSlotId = command.TimeSlotId
			};

			await DbContext.Appointments.AddAsync(appointment);
			await DbContext.SaveChangesAsync();
		}
	}
}
