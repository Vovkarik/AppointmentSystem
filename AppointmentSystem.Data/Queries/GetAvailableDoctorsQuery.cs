using AppointmentSystem.Core.Entities;
using CqrsSpirit;
using CqrsSpirit.Objects;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AppointmentSystem.Core.Dto;

namespace AppointmentSystem.Data.Queries
{
	public interface IGetAvailableDoctorsQuery : IQuery
	{
		Task<IEnumerable<AvailableDoctor>> ExecuteAsync();
	}

	public class GetAvailableDoctorsQuery : ObjectQueryBase<ApplicationDbContext>, IGetAvailableDoctorsQuery
	{
		public GetAvailableDoctorsQuery(ApplicationDbContext dbContext) : base(dbContext)
		{
		}

		public async Task<IEnumerable<AvailableDoctor>> ExecuteAsync()
		{
			return await DbContext.Doctors
				.OrderBy(doctor => doctor.DoctorCategory.Name)
				.Select(doctor => new AvailableDoctor
				{
					Id = doctor.Id,
					Name = doctor.Name,
					Surname = doctor.Surname,
					MiddleName = doctor.MiddleName,
					Category = doctor.DoctorCategory
				})
				.ToListAsync();
		}
	}
}
