using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace AppointmentSystem.Pages
{
	public class VerifyModel : PageModel
	{
		private string returnUrl = string.Empty;
		private string phone = string.Empty;

		public IActionResult OnGet(string returnUrl, string phone)
		{
			this.returnUrl = returnUrl ?? "/";
			this.phone = phone;

			return Page();
		}

		public IActionResult OnPost(string code)
		{
			return Page();
		}
	}
}