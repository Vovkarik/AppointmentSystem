using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhoneNumbers;

namespace AppointmentSystem.Pages
{
    public class LoginModel : PageModel
    {
		private readonly IUserAuthenticationService userAuthentication;
		private readonly PhoneNumberUtil phoneUtil;

		[BindProperty(SupportsGet = true)]
		public string ReturnUrl { get; set; }

		public LoginModel(IUserAuthenticationService userAuthentication)
		{
			this.userAuthentication = userAuthentication;
			phoneUtil = PhoneNumberUtil.GetInstance();
		}

		public IActionResult OnGet()
		{
			return Page();
		}

		public async Task<IActionResult> OnPost(string phone)
		{
			string internationalPhone = "";

			try
			{
				PhoneNumber phoneNumber = phoneUtil.Parse(phone, "RU");
				internationalPhone = phoneUtil.Format(phoneNumber, PhoneNumberFormat.INTERNATIONAL);
			}
			catch(NumberParseException e)
			{
				ModelState.AddModelError(e.ErrorType.ToString(), e.Message);
			}

			if(string.IsNullOrEmpty(internationalPhone))
			{
				return Page();
			}

			string secretKey = await userAuthentication.SendVerificationCodeAndGetSecretKey(phone);
			TempData["VerificationSecret"] = secretKey;
			TempData["Phone"] = internationalPhone;
			return RedirectToPage("/User/Verify", new { returnUrl = ReturnUrl });
		}

	}
}