using AppointmentSystem.Core.Dto;
using CqrsSpirit;
using CqrsSpirit.Objects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentSystem.Data.Queries
{
	public interface IGetAllPendingAppointmentsQuery : IQuery
	{
		Task<IEnumerable<AssignedAppointment>> ExecuteAsync();
	}

	public class GetAllPendingAppointmentsQuery : ObjectQueryBase<ApplicationDbContext>, IGetAllPendingAppointmentsQuery
	{
		public GetAllPendingAppointmentsQuery(ApplicationDbContext dbContext) : base(dbContext)
		{
		}

		public async Task<IEnumerable<AssignedAppointment>> ExecuteAsync()
		{
			DateTime now = DateTime.UtcNow;
			return await DbContext.AppointmentSlots
				.Include(slot => slot.Appointment)
					.ThenInclude(appointment => appointment.User)
				.Where(slot => slot.Appointment != null && slot.StartTime > now)
				.OrderBy(slot => slot.StartTime)
				.Select(slot => new AssignedAppointment
				{
					Id = slot.Appointment.Id,
					Slot = new AvailableTimeSlot
					{
						Id = slot.Id,
						StartTime = slot.StartTime,
						Doctor = new AvailableDoctor
						{
							Id = slot.Doctor.Id,
							Name = slot.Doctor.Name,
							Surname = slot.Doctor.Surname,
							MiddleName = slot.Doctor.MiddleName,
							Category = slot.Doctor.DoctorCategory
						}
					},
					User = new AssignedUser { Phone = new Core.Services.InternationalPhone(slot.Appointment.User.PhoneNumber) }
				}).ToListAsync();
		}
	}
}
