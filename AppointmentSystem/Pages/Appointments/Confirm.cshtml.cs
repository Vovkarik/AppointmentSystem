using System;
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
    public class ConfirmModel : PageModel
    {
		private readonly IGetTimeSlotQuery getSlotQuery;

		[BindProperty(SupportsGet = true)]
		public int? TimeSlotId { get; set; }

		public AvailableTimeSlot Slot { get; set; }

		public ConfirmModel(IGetTimeSlotQuery getSlotQuery)
		{
			this.getSlotQuery = getSlotQuery;
		}

		public async Task<IActionResult> OnGetAsync()
        {
			if(TimeSlotId == null)
			{
				return RedirectToPage("/Appointments/Doctors");
			}

			AvailableTimeSlot slot = await getSlotQuery.ExecuteAsync(TimeSlotId.Value);
			if(slot == null)
			{
				return RedirectToPage("/Appointments/Doctors");
			}

			Slot = slot;

			return Page();
        }
    }
}
