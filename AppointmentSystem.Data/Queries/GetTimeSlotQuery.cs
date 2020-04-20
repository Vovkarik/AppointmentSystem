using AppointmentSystem.Core.Dto;
using AppointmentSystem.Core.Entities;
using CqrsSpirit;
using CqrsSpirit.Objects;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentSystem.Data.Queries
{
	public interface IGetTimeSlotQuery : IQuery
	{
		Task<AvailableTimeSlot> ExecuteAsync(int timeSlotId);
	}

	public class GetTimeSlotQuery : ObjectQueryBase<ApplicationDbContext>, IGetTimeSlotQuery
	{
		public GetTimeSlotQuery(ApplicationDbContext dbContext) : base(dbContext)
		{
		}

		public async Task<AvailableTimeSlot> ExecuteAsync(int timeSlotId)
		{
			AppointmentSlot slot = await DbContext.AppointmentSlots.Where(slot => slot.Id == timeSlotId).FirstOrDefaultAsync();
			if(slot == null)
			{
				return null;
			}

			return new AvailableTimeSlot
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
			};
		}
	}

}
