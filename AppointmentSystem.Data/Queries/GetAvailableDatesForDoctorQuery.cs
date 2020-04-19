using AppointmentSystem.Core.Dto;
using AppointmentSystem.Core.Entities;
using CqrsSpirit;
using CqrsSpirit.Objects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentSystem.Data.Queries
{
	public interface IGetAvailableDatesForDoctorQuery : IQuery
	{
		Task<IEnumerable<AvailableDate>> ExecuteAsync(int doctorId);
	}

	public class GetAvailableDatesForDoctorQuery : ObjectQueryBase<ApplicationDbContext>, IGetAvailableDatesForDoctorQuery
	{
		private class DateGroup
		{
			public DateTime Date { get; set; }
			public List<AppointmentSlot> Slots { get; set; }
		}

		public GetAvailableDatesForDoctorQuery(ApplicationDbContext dbContext) : base(dbContext)
		{
		}

		public async Task<IEnumerable<AvailableDate>> ExecuteAsync(int doctorId)
		{
			DateTime now = DateTime.UtcNow;

			var groups = (await DbContext.AppointmentSlots
				.Where(slot => slot.DoctorId == doctorId && slot.StartTime > now)
				.OrderBy(slot => slot.StartTime)
				.ToListAsync())
				.GroupBy(slot => slot.StartTime.Date)
				.Select(group => new DateGroup { Date = group.Key, Slots = group.ToList() })
				.ToList();

			var dates = new List<AvailableDate>();
			foreach(DateGroup group in groups)
			{
				DateTime date = group.Date;
				int totalCount = group.Slots.Count;
				int takenCount = group.Slots.Count(slot => slot.Appointment != null);
				double t = (double)takenCount / totalCount;

				DateAvailability availability;
				if(t < 0.5)
				{
					availability = DateAvailability.Free;
				}
				else if(t < 1)
				{
					availability = DateAvailability.HalfFull;
				}
				else
				{
					availability = DateAvailability.Full;
				}

				dates.Add(new AvailableDate
				{
					Date = date,
					Availability = availability
				});
			}

			return dates;
		}
	}
}
