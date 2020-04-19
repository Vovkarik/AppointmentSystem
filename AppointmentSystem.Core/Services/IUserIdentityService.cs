using AppointmentSystem.Core.Entities;
using System.Threading.Tasks;

namespace AppointmentSystem.Core.Services
{
	public interface IUserIdentityService
	{
		Task SignInOrRegisterWithPhoneAsync(InternationalPhone phone);
		Task SignOutAsync();
		Task<ApplicationUser> GetCurrentUserAsync();
	}
}
