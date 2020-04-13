using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using AppointmentSystem.Services;

namespace AppointmentSystem.Pages
{
	public class VerifyModel : PageModel
	{
		private readonly IUserAuthenticationService userAuthentication;

		public string ReturnUrl { get; set; }

		public VerifyModel(IUserAuthenticationService userAuthentication)
		{
			this.userAuthentication = userAuthentication;
		}

		public IActionResult OnGet(string returnUrl, string phone)
		{
			// Return to index if we don't have an URL to go back to.
			ReturnUrl = string.IsNullOrEmpty(returnUrl) ? "/Index" : returnUrl;

			return Page();
		}

		public IActionResult OnPost(string returnUrl, string code)
		{
			if(!TempData.TryGetValue("VerificationSecret", out object secretKey))
			{
				return RedirectToPage("/User/Login", new { returnUrl = returnUrl });
			}

			if(!userAuthentication.VerifyVerificationCode((string)secretKey, code))
			{
				// Prevent clearing the session if the code is invalid.
				TempData.Keep();
				return RedirectToPage(new { returnUrl });
			}

			return RedirectToPage(returnUrl);
		}
	}
}