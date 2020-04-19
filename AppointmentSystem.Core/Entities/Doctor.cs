using System.ComponentModel;

namespace AppointmentSystem.Core.Entities
{
	public class Doctor
	{
		public int Id { get; set; }
		public int DoctorCategoryId { get; set; }
		[DisplayName("Имя")]
		public string Name { get; set; }
		[DisplayName("Фамилия")]
		public string Surname { get; set; }
		[DisplayName("Отчество")]
		public string MiddleName { get; set; }

		public string FullName => $"{Name} {Surname} {MiddleName}";

		[DisplayName("Категория")]
		public virtual DoctorCategory DoctorCategory { get; set; }
	}
}
