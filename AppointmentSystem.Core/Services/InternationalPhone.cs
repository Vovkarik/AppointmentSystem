using PhoneNumbers;

namespace AppointmentSystem.Core.Services
{
	public class InternationalPhone
	{
		public string Formatted { get; }

		public InternationalPhone(string phone)
		{
			Formatted = phone;
		}

		public static bool TryParse(string phone, out InternationalPhone parsed)
		{
			PhoneNumberUtil util = PhoneNumberUtil.GetInstance();
			try
			{
				PhoneNumber phoneNumber = util.Parse(phone, null);
				parsed = new InternationalPhone(util.Format(phoneNumber, PhoneNumberFormat.INTERNATIONAL));
				return true;
			}
			catch(NumberParseException)
			{
				parsed = null;
				return false;
			}
		}
	}
}
