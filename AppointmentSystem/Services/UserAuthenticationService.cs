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
		bool VerifyVerificationCode(string secretKey, string verificationCode);
	}

	public class UserAuthenticationService : IUserAuthenticationService
	{
		private readonly int GenerationDelaySeconds = 60 * 10;
		private readonly ISmsService smsService;

		public UserAuthenticationService(ISmsService smsService)
		{
			this.smsService = smsService;
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

		public bool VerifyVerificationCode(string secretKey, string verificationCode)
		{
			var otp = new Totp(Convert.FromBase64String(secretKey), step: GenerationDelaySeconds);
			return otp.VerifyTotp(verificationCode, out _, VerificationWindow.RfcSpecifiedNetworkDelay);
		}
	}
}
