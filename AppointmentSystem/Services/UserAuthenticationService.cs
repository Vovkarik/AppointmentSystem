using AppointmentSystem.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.EntityFrameworkCore;
using OtpNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace AppointmentSystem.Services
{
	public interface IUserAuthenticationService
	{
		Task<string> SendVerificationCodeAndGetSecretKey(string phone);
		Task<bool> VerifyVerificationCode(string secretKey, string phone, string verificationCode);
	}

	public class UserAuthenticationService : IUserAuthenticationService
	{
		private readonly int GenerationDelaySeconds = 60 * 10;
		private readonly ISmsService smsService;
		private readonly UserManager<ApplicationUser> userManager;
		private readonly SignInManager<ApplicationUser> signInManager;

		public UserAuthenticationService(ISmsService smsService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			this.smsService = smsService;
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

		public async Task<bool> VerifyVerificationCode(string secretKey, string phone, string verificationCode)
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
						PhoneNumber = phone
					};
					await userManager.CreateAsync(user);
				}
				
				await signInManager.SignInAsync(user, false);
			}

			return succeeded;
		}
	}
}
