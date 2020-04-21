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
	public interface IGetAvailableTimeSlotsForDayQuery : IQuery
	{
		Task<IEnumerable<AvailableTimeSlot>> ExecuteAsync(int doctorId, DateTime date);
	}

	public class GetAvailableTimeSlotsForDayQuery : ObjectQueryBase<ApplicationDbContext>, IGetAvailableTimeSlotsForDayQuery
	{
		public GetAvailableTimeSlotsForDayQuery(ApplicationDbContext dbContext) : base(dbContext)
		{
		}

		public async Task<IEnumerable<AvailableTimeSlot>> ExecuteAsync(int doctorId, DateTime date)
		{
			DateTime now = DateTime.UtcNow;
			if(date.Date < now.Date)
			{
				return Enumerable.Empty<AvailableTimeSlot>();
			}

			return await DbContext.AppointmentSlots
				.Include(slot => slot.Appointment)
				.Where(slot => slot.DoctorId == doctorId && slot.StartTime.Date == date.Date && slot.Appointment == null)
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
