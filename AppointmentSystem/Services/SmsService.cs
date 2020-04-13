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
		private const string accountSid = "ACd18347b836c1c66ab08918590bb7adac";
		private const string authToken = "35462b0d17c71da0bdd8aa22576ab478";

		public SmsService()
		{
			TwilioClient.Init(accountSid, authToken);
		}

		public Task SendAsync(string phone, string message)
		{
			return MessageResource.CreateAsync(to: new Twilio.Types.PhoneNumber(phone), from: new Twilio.Types.PhoneNumber("+16106157515"), body: message);
		}
	}
}
