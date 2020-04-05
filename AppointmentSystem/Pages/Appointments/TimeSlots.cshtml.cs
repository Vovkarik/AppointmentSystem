using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppointmentSystem.Pages.Appointments
{
    public class TimeSlotsModel : PageModel
    {
        public IActionResult OnGet(int? doctorId, DateTime? date)
        {
			if(doctorId == null)
			{
				return RedirectToPage("/Appointments/New");
			}

			if(date == null)
			{
				return RedirectToPage("/Appointments/Dates");
			}

			return Page();
        }
    }
}