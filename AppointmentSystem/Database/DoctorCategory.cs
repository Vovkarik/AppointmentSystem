using System.ComponentModel;

namespace AppointmentSystem.Database
{
	public class DoctorCategory
	{
		public int Id { get; set; }
		[DisplayName("Название")]
		public string Name { get; set; }
	}
}
