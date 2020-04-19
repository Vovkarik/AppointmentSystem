using System;
using System.Threading.Tasks;
using AppointmentSystem.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppointmentSystem.Pages.User
{
    public class LogOutModel : PageModel
    {
		private readonly IUserIdentityService userIdentityService;

		public LogOutModel(IUserIdentityService userIdentityService)
		{
			this.userIdentityService = userIdentityService;
		}

		public async Task<IActionResult> OnGetAsync()
        {
			await userIdentityService.SignOutAsync();
			return RedirectToPage("/Index");
        }
    }
}