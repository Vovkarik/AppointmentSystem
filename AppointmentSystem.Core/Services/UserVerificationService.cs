using OtpNet;
using System;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace AppointmentSystem.Core.Services
{
	public class UserVerificationService : IUserVerificationService
	{
		private const int GenerationDelaySeconds = 60 * 10;
		private readonly ISmsService sms;

		public UserVerificationService(ISmsService sms)
		{
			this.sms = sms;
		}

		public async Task<UserVerificationInfo> SendVerificationCodeAsync(InternationalPhone phone)
		{
			var secretKey = new byte[6];
			using(var rng = new RNGCryptoServiceProvider())
			{
				rng.GetBytes(secretKey);
			}

			var otp = new Totp(secretKey, step: GenerationDelaySeconds);
			string code = otp.ComputeTotp(DateTime.UtcNow);

			await sms.SendAsync(phone, $"Code: {code}");

			return new UserVerificationInfo(phone, Convert.ToBase64String(secretKey));
		}

		public bool CheckVerificationCode(UserVerificationInfo verificationInfo, string code)
		{
			var otp = new Totp(Convert.FromBase64String(verificationInfo.SecretKey), step: GenerationDelaySeconds);
			return otp.VerifyTotp(code, out _, VerificationWindow.RfcSpecifiedNetworkDelay);
		}
	}
}
