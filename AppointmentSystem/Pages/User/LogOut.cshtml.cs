using System;
using System.Threading.Tasks;
using AppointmentSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppointmentSystem.Pages.User
{
    public class LogOutModel : PageModel
    {
		private readonly IUserAuthenticationService userAuthentication;

		public LogOutModel(IUserAuthenticationService userAuthentication)
		{
			this.userAuthentication = userAuthentication;
		}

		public async Task<IActionResult> OnGetAsync()
        {
			await userAuthentication.LogOutAsync();
			return RedirectToPage("/Index");
        }
    }
}