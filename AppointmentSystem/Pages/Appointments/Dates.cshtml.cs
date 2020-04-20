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
		[BindProperty(SupportsGet = true)]
		public int? DoctorId { get; set; }

		public IActionResult OnGet()
        {
			if(DoctorId == null)
			{
				return RedirectToPage("/Appointments/Doctors");
			}

			return Page();
        }
    }
}