using System.Threading.Tasks;

namespace AppointmentSystem.Core.Services
{
	public interface ISmsService
	{
		Task SendAsync(InternationalPhone phone, string message);
	}
}
