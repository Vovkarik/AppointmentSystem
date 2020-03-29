using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppointmentSystem.Pages
{
    public class DoctorsModel : PageModel
    {
		public List<AvailableDoctor> AvailableDoctors { get; } = new List<AvailableDoctor>();

        public void OnGet()
        {
			for(int i = 0; i < 15; ++i)
			{
				AvailableDoctors.Add(new AvailableDoctor
				{
					Id = i + 1,
					Name = "Тест",
					Surname = "Тестов",
					MiddleName = "Тестович",
					Category = new Database.DoctorCategory { Name = "кардиолог" }
				});
			}
        }
    }
}
