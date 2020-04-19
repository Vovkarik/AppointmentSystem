using System;
using AppointmentSystem.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppointmentSystem.Pages
{
	[Authorize(AuthenticationSchemes = AuthenticationSchemes.User)]
	public class DatesModel : PageModel
    {
		public IActionResult OnGet(int? doctorId)
        {
			if(doctorId == null)
			{
				return RedirectToPage("/Appointments/New");
			}

			return Page();
        }
    }
}