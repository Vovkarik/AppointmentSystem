using System;
using System.Threading.Tasks;
using AppointmentSystem.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppointmentSystem.Pages
{
	public class VerifyModel : PageModel
	{
		private readonly IUserVerificationService verificationService;
		private readonly IUserIdentityService identityService;

		[BindProperty(SupportsGet = true)]
		public string ReturnUrl { get; set; }

		public VerifyModel(IUserVerificationService verificationService, IUserIdentityService identityService)
		{
			this.verificationService = verificationService;
			this.identityService = identityService;
		}

		public IActionResult OnGet()
		{
			return Page();
		}

		public async Task<IActionResult> OnPostAsync(string code)
		{
			ReturnUrl ??= "/Index";
			if(!TempData.TryGetValue("VerificationSecret", out object secretKey))
			{
				return RedirectToPage("/User/Login", new { ReturnUrl });
			}

			if(!TempData.TryGetValue("Phone", out object phone))
			{
				return RedirectToPage("/User/Login", new { ReturnUrl });
			}

			var internationalPhone = new InternationalPhone((string)phone);
			var info = new UserVerificationInfo(internationalPhone, (string)secretKey);
			if(!verificationService.CheckVerificationCode(info, code))
			{
				// Prevent deletion of secret if code was entered incorrectly.
				TempData.Keep();
				return Page();
			}

			await identityService.SignInOrRegisterWithPhoneAsync(internationalPhone);
			return LocalRedirect(ReturnUrl);
		}
	}
}