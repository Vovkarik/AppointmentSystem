using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppointmentSystem.Pages.Appointments
{
    public class TimeSlotsModel : PageModel
    {
		public List<AvailableTimeSlot> AvailableSlots { get; } = new List<AvailableTimeSlot>();

        public IActionResult OnGet(int? doctorId, DateTime? date)
        {
			if(doctorId == null || date == null)
			{
				return RedirectToPage("/Appointments/New");
			}

			for(int i = 0; i < 5; ++i)
			{
				var slot = new AvailableTimeSlot
				{
					Id = i,
					StartTime = date.Value.Date.AddHours(8 + i)
				};
				AvailableSlots.Add(slot);
			}

			return Page();
        }
    }
}