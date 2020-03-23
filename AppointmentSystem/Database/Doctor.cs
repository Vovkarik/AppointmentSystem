namespace AppointmentSystem.Database
{
	public class Doctor
	{
		public int Id { get; set; }
		public int DoctorCategoryId { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string MiddleName { get; set; }
	}
}
