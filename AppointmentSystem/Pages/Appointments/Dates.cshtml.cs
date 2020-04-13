using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppointmentSystem.Pages
{
	[Authorize(AuthenticationSchemes = AuthenticationSchemes.User, Roles = Roles.User)]
	public class DatesModel : PageModel
    {
        public IActionResult OnGet(int? doctorId)
        {
			if(doctorId == null)
			{
				return RedirectToPage("/Appointments/New");
			}
			//var doctor = ...
			//if(doctor == null)
			//{
			//	return RedirectToPage("Index");
			//}

			return Page();
        }

		public JsonResult OnGetDateList()
		{
			var dates = new List<AvailableDate>();
			var now = DateTime.Now.Date;
			for(int i = 0; i < 20; ++i)
			{
				var date = new AvailableDate();
				date.Date = now.AddDays(i);

				if(i % 2 == 0)
				{
					date.Availability = DateAvailability.HalfFull;
				}
				else if(i % 3 == 0)
				{
					date.Availability = DateAvailability.Full;
				}
				else
				{
					date.Availability = DateAvailability.Free;
				}

				dates.Add(date);
			}

			return new JsonResult(dates);
		}
    }
}