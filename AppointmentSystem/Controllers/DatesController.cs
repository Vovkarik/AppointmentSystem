using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentSystem.Core.Dto;
using AppointmentSystem.Data.Queries;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentSystem.Controllers
{
	[Route("api/[controller]")]
	public class DatesController : Controller
	{
		private readonly IGetAvailableDatesForDoctorQuery getDatesQuery;

		public DatesController(IGetAvailableDatesForDoctorQuery getDatesQuery)
		{
			this.getDatesQuery = getDatesQuery;
		}

		[HttpGet]
		public async Task<IEnumerable<AvailableDate>> Get([FromQuery] int doctorId)
		{
			return await getDatesQuery.ExecuteAsync(doctorId);
		}
	}
}
