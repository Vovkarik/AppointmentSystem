using AppointmentSystem.Core;
using AppointmentSystem.Core.Entities;
using AppointmentSystem.Core.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthenticationSchemes = AppointmentSystem.Core.AuthenticationSchemes;

namespace AppointmentSystem.Infrastructure.Services
{
	public class UserIdentityService : IUserIdentityService
	{
		private readonly IHttpContextAccessor httpContextAccessor;
		private readonly UserManager<ApplicationUser> userManager;

		public UserIdentityService(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
		{
			this.httpContextAccessor = httpContextAccessor;
			this.userManager = userManager;
		}

		public async Task SignInOrRegisterWithPhoneAsync(InternationalPhone phone)
		{
			ApplicationUser user = await userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phone.Formatted);
			if(user == null)
			{
				user = new ApplicationUser
				{
					UserName = StripSpecialCharacters(phone.Formatted),
					PhoneNumber = phone.Formatted
				};
				var result = await userManager.CreateAsync(user);
				result = await userManager.AddToRoleAsync(user, Roles.User);
			}

			var identity = new ClaimsIdentity(AuthenticationSchemes.User);
			identity.AddClaim(new Claim(ClaimTypes.Role, Roles.User));
			identity.AddClaim(new Claim(ClaimTypes.Name, user.Id));
			var principal = new ClaimsPrincipal(identity);
			await httpContextAccessor.HttpContext.SignInAsync(AuthenticationSchemes.User, principal);
		}

		public async Task SignOutAsync()
		{
			await httpContextAccessor.HttpContext.SignOutAsync(AuthenticationSchemes.User);
		}

		public async Task<ApplicationUser> GetCurrentUserAsync()
		{
			string id = httpContextAccessor.HttpContext.User.Identity.Name;
			if(id == null)
			{
				return null;
			}

			return await userManager.FindByIdAsync(id);
		}

		private string StripSpecialCharacters(string phone)
		{
			return new string(phone.Where(ch => char.IsDigit(ch)).ToArray());
		}
	}
}
