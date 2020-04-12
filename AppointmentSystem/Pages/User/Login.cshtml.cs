using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppointmentSystem.Pages
{
    public class LoginModel : PageModel
    {
		public IActionResult OnGet()
		{
			return Page();
		}
		public IActionResult OnPost(string phone)
		{
			return RedirectToPage("/User/Verify", new { phone = phone });
		}

	}
}