namespace AppointmentSystem.Core.Services
{
	public class UserVerificationInfo
	{
		public InternationalPhone Phone { get; }
		public string SecretKey { get; }

		public UserVerificationInfo(InternationalPhone phone, string secretKey)
		{
			Phone = phone;
			SecretKey = secretKey;
		}
	}
}
