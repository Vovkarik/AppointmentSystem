using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentSystem.Core;
using AppointmentSystem.Core.Dto;
using AppointmentSystem.Core.Entities;
using AppointmentSystem.Core.Services;
using AppointmentSystem.Data.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppointmentSystem.Pages.User
{
	[Authorize(AuthenticationSchemes = AuthenticationSchemes.User)]
    public class ProfileModel : PageModel
    {
		private readonly IUserIdentityService identityService;
		private readonly IGetCurrentAppointmentsForUserQuery getAppointmentsQuery;

		public List<AvailableTimeSlot> Slots { get; } = new List<AvailableTimeSlot>();

		public ProfileModel(IUserIdentityService identityService, IGetCurrentAppointmentsForUserQuery getAppointmentsQuery)
		{
			this.identityService = identityService;
			this.getAppointmentsQuery = getAppointmentsQuery;
		}

		public async Task<IActionResult> OnGet()
        {
			ApplicationUser user = await identityService.GetCurrentUserAsync();
			Slots.AddRange(await getAppointmentsQuery.ExecuteAsync(user.Id));
			return Page();
        }
    }
}