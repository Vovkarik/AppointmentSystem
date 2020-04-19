using System;
using System.Threading.Tasks;
using AppointmentSystem.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppointmentSystem.Pages
{
    public class LoginModel : PageModel
    {
		private readonly IUserVerificationService verificationService;

		[BindProperty(SupportsGet = true)]
		public string ReturnUrl { get; set; }

		public LoginModel(IUserVerificationService verificationService)
		{
			this.verificationService = verificationService;
		}

		public IActionResult OnGet()
		{
			return Page();
		}

		public async Task<IActionResult> OnPostAsync(string phone)
		{
			if(InternationalPhone.TryParse(phone, out InternationalPhone parsed))
			{
				UserVerificationInfo info = await verificationService.SendVerificationCodeAsync(parsed);
				TempData["VerificationSecret"] = info.SecretKey;
				TempData["Phone"] = info.Phone.Formatted;
				return RedirectToPage("/User/Verify", new { ReturnUrl });
			}

			return Page();
		}

	}
}