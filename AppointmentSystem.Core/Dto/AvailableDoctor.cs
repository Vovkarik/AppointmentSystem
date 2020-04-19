using AppointmentSystem.Core.Entities;

namespace AppointmentSystem.Core.Dto
{
	public class AvailableDoctor
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string MiddleName { get; set; }
		public DoctorCategory Category { get; set; }

		public string FullName => $"{Name} {Surname} {MiddleName}";
		public string CategoryName => Category?.Name;
	}
}
