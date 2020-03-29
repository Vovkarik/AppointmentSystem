using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppointmentSystem.Pages
{
    public class DatesModel : PageModel
    {
        public IActionResult OnGet(int doctorId)
        {
			//var doctor = ...
			//if(doctor == null)
			//{
			//	return RedirectToPage("Index");
			//}

			return Page();
        }
    }
}