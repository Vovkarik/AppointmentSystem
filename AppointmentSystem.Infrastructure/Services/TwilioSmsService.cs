using AppointmentSystem.Core.Services;
using System;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace AppointmentSystem.Infrastructure.Services
{
	public class TwilioSmsService : ISmsService
	{
		public TwilioSmsService()
		{
			string accountSid = Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID");
			string authToken = Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN");
			TwilioClient.Init(accountSid, authToken);
		}

		public Task SendAsync(InternationalPhone phone, string message)
		{
			return MessageResource.CreateAsync(to: new Twilio.Types.PhoneNumber(phone.Formatted), from: new Twilio.Types.PhoneNumber("+16106157515"), body: message);
		}
	}
}
