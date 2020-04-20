using AppointmentSystem.Core.Dto;
using AppointmentSystem.Core.Entities;
using CqrsSpirit;
using CqrsSpirit.Objects;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentSystem.Data.Queries
{
	public interface IGetDoctorQuery : IQuery
	{
		Task<AvailableDoctor> ExecuteAsync(int doctorId);
	}

	public class GetDoctorQuery : ObjectQueryBase<ApplicationDbContext>, IGetDoctorQuery
	{
		public GetDoctorQuery(ApplicationDbContext dbContext) : base(dbContext)
		{
		}

		public async Task<AvailableDoctor> ExecuteAsync(int doctorId)
		{
			Doctor doctor = await DbContext.Doctors.Where(doctor => doctor.Id == doctorId).FirstOrDefaultAsync();
			return new AvailableDoctor
			{
				Id = doctor.Id,
				Name = doctor.Name,
				Surname = doctor.Surname,
				MiddleName = doctor.MiddleName,
				Category = doctor.DoctorCategory
			};
		}
	}

}
