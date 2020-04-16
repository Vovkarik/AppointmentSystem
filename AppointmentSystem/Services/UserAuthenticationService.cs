using AppointmentSystem.Database;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OtpNet;
using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace AppointmentSystem.Services
{
	public interface IUserAuthenticationService
	{
		Task<string> SendVerificationCodeAndGetSecretKey(string phone);
		Task<bool> LoginWithVerificationCodeAsync(string secretKey, string phone, string verificationCode);
	}

	public class UserAuthenticationService : IUserAuthenticationService
	{
		private readonly int GenerationDelaySeconds = 60 * 10;
		private readonly ISmsService smsService;
		private readonly IHttpContextAccessor httpContextAccessor;
		private readonly UserManager<ApplicationUser> userManager;
		private readonly SignInManager<ApplicationUser> signInManager;

		public UserAuthenticationService(ISmsService smsService, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			this.smsService = smsService;
			this.httpContextAccessor = httpContextAccessor;
			this.userManager = userManager;
			this.signInManager = signInManager;
		}

		public async Task<string> SendVerificationCodeAndGetSecretKey(string phone)
		{
			var secretKey = new byte[6];
			using(var rng = new RNGCryptoServiceProvider())
			{
				rng.GetBytes(secretKey);
			}

			var otp = new Totp(secretKey, step: GenerationDelaySeconds);
			string code = otp.ComputeTotp(DateTime.UtcNow);

			await smsService.SendAsync(phone, $"Code: {code}");

			return Convert.ToBase64String(secretKey);
		}

		public async Task<bool> LoginWithVerificationCodeAsync(string secretKey, string phone, string verificationCode)
		{
			var otp = new Totp(Convert.FromBase64String(secretKey), step: GenerationDelaySeconds);
			bool succeeded = otp.VerifyTotp(verificationCode, out _, VerificationWindow.RfcSpecifiedNetworkDelay);

			if(succeeded)
			{
				ApplicationUser user = await userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phone);
				if(user == null)
				{
					user = new ApplicationUser
					{
						UserName = StripSpecialCharacters(phone),
						PhoneNumber = phone
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

			return succeeded;
		}

		public async Task LogOutAsync()
		{
			await httpContextAccessor.HttpContext.SignOutAsync(AuthenticationSchemes.User);
		}

		public Task<ApplicationUser> GetCurrentUserAsync()
		{
			return userManager.FindByIdAsync(httpContextAccessor.HttpContext.User.Identity.Name);
		}

		private string StripSpecialCharacters(string phone)
		{
			return new string(phone.Where(ch => char.IsDigit(ch)).ToArray());
		}
	}
}
