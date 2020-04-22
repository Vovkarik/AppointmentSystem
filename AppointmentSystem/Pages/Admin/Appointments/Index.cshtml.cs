using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentSystem.Core.Dto;
using AppointmentSystem.Data.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppointmentSystem.Pages.Admin.Appointments
{
    public class IndexModel : PageModel
    {
		private readonly IGetAllPendingAppointmentsQuery getAppointmentsQuery;

		public List<AssignedAppointment> Appointments { get; } = new List<AssignedAppointment>();

		public IndexModel(IGetAllPendingAppointmentsQuery getAppointmentsQuery)
		{
			this.getAppointmentsQuery = getAppointmentsQuery;
		}

		public async Task<IActionResult> OnGet()
        {
			Appointments.AddRange(await getAppointmentsQuery.ExecuteAsync());
			return Page();
        }
    }
}