using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppointmentSystem.Core;
using AppointmentSystem.Core.Dto;
using AppointmentSystem.Data.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppointmentSystem.Pages
{
	[Authorize(AuthenticationSchemes = AuthenticationSchemes.User)]
	public class DoctorsModel : PageModel
    {
		private readonly IGetAvailableDoctorsQuery getDoctorsQuery;

		public List<AvailableDoctor> AvailableDoctors { get; } = new List<AvailableDoctor>();

		public DoctorsModel(IGetAvailableDoctorsQuery getDoctorsQuery)
		{
			this.getDoctorsQuery = getDoctorsQuery;
		}

		public async Task OnGetAsync()
        {
			foreach(AvailableDoctor doctor in await getDoctorsQuery.ExecuteAsync())
			{
				AvailableDoctors.Add(doctor);
			}
        }
    }
}
