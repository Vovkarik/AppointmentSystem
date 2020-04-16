using System;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace AppointmentSystem.Services
{
	public interface ISmsService
	{
		Task SendAsync(string phone, string message);
	}

	public class SmsService : ISmsService
	{
		public SmsService()
		{
			string accountSid = Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID");
			string authToken = Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN");
			TwilioClient.Init(accountSid, authToken);
		}

		public Task SendAsync(string phone, string message)
		{
			return MessageResource.CreateAsync(to: new Twilio.Types.PhoneNumber(phone), from: new Twilio.Types.PhoneNumber("+16106157515"), body: message);
		}
	}
}
