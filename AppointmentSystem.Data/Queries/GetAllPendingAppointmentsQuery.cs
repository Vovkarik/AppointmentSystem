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
		Task<IEnumerable<AvailableTimeSlot>> ExecuteAsync();
	}

	public class GetAllPendingAppointmentsQuery : ObjectQueryBase<ApplicationDbContext>, IGetAllPendingAppointmentsQuery
	{
		public GetAllPendingAppointmentsQuery(ApplicationDbContext dbContext) : base(dbContext)
		{
		}

		public async Task<IEnumerable<AvailableTimeSlot>> ExecuteAsync()
		{
			DateTime now = DateTime.UtcNow;
			return await DbContext.AppointmentSlots
				.Include(slot => slot.Appointment)
				.Where(slot => slot.Appointment != null && slot.StartTime > now)
				.OrderBy(slot => slot.StartTime)
				.Select(slot => new AvailableTimeSlot
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
				}).ToListAsync();
		}
	}
}
