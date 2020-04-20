using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppointmentSystem.Core;
using AppointmentSystem.Core.Dto;
using AppointmentSystem.Data.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppointmentSystem.Pages.Appointments
{
	[Authorize(AuthenticationSchemes = AuthenticationSchemes.User)]
	public class TimeSlotsModel : PageModel
    {
		private readonly IGetAvailableTimeSlotsForDayQuery getSlotsQuery;

		[BindProperty(SupportsGet = true)]
		public int? DoctorId { get; set; }

		[BindProperty(SupportsGet = true)]
		public DateTime? Date { get; set; }

		public List<AvailableTimeSlot> AvailableSlots { get; } = new List<AvailableTimeSlot>();

		public TimeSlotsModel(IGetAvailableTimeSlotsForDayQuery getSlotsQuery)
		{
			this.getSlotsQuery = getSlotsQuery;
		}

		public async Task<IActionResult> OnGetAsync()
        {
			if(DoctorId == null)
			{
				return RedirectToPage("/Appointments/Doctors");
			}

			if(Date == null)
			{
				return RedirectToPage("/Appointments/Dates");
			}

			AvailableSlots.AddRange(await getSlotsQuery.ExecuteAsync(DoctorId.Value, Date.Value.Date));

			return Page();
        }
    }
}