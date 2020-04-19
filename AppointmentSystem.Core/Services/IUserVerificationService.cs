using System.Threading.Tasks;

namespace AppointmentSystem.Core.Services
{
	public interface IUserVerificationService
	{
		Task<UserVerificationInfo> SendVerificationCodeAsync(InternationalPhone phone);
		bool CheckVerificationCode(UserVerificationInfo verificationInfo, string code);
	}
}
