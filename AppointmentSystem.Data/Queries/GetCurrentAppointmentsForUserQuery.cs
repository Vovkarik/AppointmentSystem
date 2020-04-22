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
	public interface IGetCurrentAppointmentsForUserQuery : IQuery
	{
		Task<IEnumerable<AvailableTimeSlot>> ExecuteAsync(string userId);
	}

	public class GetCurrentAppointmentsForUserQuery : ObjectQueryBase<ApplicationDbContext>, IGetCurrentAppointmentsForUserQuery
	{
		public GetCurrentAppointmentsForUserQuery(ApplicationDbContext dbContext) : base(dbContext)
		{

		}

		public async Task<IEnumerable<AvailableTimeSlot>> ExecuteAsync(string userId)
		{
			DateTime now = DateTime.UtcNow.Date;
			return await DbContext.AppointmentSlots
				.Include(slot => slot.Appointment)
				.Where(slot => slot.Appointment != null && slot.Appointment.UserId == userId && slot.StartTime.Date >= now  && slot.StartTime.Hour >= now.Hour)
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
