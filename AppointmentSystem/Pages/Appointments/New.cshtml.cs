using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppointmentSystem.Pages.Appointments
{
    public class NewModel : PageModel
    {
        public IActionResult OnGet(int? doctorId, DateTime? date, DateTime? time)
        {
			if(doctorId == null)
			{
				return RedirectToPage("/Appointments/Doctors");
			}

			if(date == null)
			{
				return RedirectToPage("/Appointments/Dates", new { doctorId = doctorId });
			}

			return RedirectToPage("/Index");
		}
    }
}