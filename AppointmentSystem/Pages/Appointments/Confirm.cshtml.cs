using System;
using System.Threading.Tasks;
using AppointmentSystem.Core;
using AppointmentSystem.Core.Dto;
using AppointmentSystem.Core.Services;
using AppointmentSystem.Data.Commands;
using AppointmentSystem.Data.Queries;
using CqrsSpirit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppointmentSystem.Pages.Appointments
{
	[Authorize(AuthenticationSchemes = AuthenticationSchemes.User)]
    public class ConfirmModel : PageModel
    {
		private readonly IGetTimeSlotQuery getSlotQuery;
		private readonly ICommandsDispatcher commandsDispatcher;
		private readonly IUserIdentityService identityService;

		[BindProperty(SupportsGet = true)]
		public int? TimeSlotId { get; set; }

		public AvailableTimeSlot Slot { get; set; }

		public ConfirmModel(IGetTimeSlotQuery getSlotQuery, ICommandsDispatcher commandsDispatcher, IUserIdentityService identityService)
		{
			this.getSlotQuery = getSlotQuery;
			this.commandsDispatcher = commandsDispatcher;
			this.identityService = identityService;
		}

		public async Task<IActionResult> OnGetAsync()
        {
			if(TimeSlotId == null)
			{
				return RedirectToPage("/Appointments/Doctors");
			}

			AvailableTimeSlot slot = await getSlotQuery.ExecuteAsync(TimeSlotId.Value);
			if(slot == null)
			{
				return RedirectToPage("/Appointments/Doctors");
			}

			Slot = slot;

			return Page();
        }

		public async Task<IActionResult> OnPostAsync()
		{
			try
			{
				var user = await identityService.GetCurrentUserAsync();
				await commandsDispatcher.ExecuteAsync(new CreateAppointmentCommand(userId: user.Id, timeSlotId: TimeSlotId.GetValueOrDefault()));
				return RedirectToPage("/User/Profile");
			}
			catch(Exception)
			{
				return RedirectToPage("/Appointments/Failed");
			}
		}
    }
}
