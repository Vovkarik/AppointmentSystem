using System.ComponentModel;

namespace AppointmentSystem.Core.Entities
{
	public class DoctorCategory
	{
		public int Id { get; set; }
		[DisplayName("Название")]
		public string Name { get; set; }
	}
}
