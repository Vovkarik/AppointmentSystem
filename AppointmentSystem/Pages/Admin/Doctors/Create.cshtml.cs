using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AppointmentSystem.Database;

namespace AppointmentSystem.Pages.Admin.Doctors
{
    public class CreateModel : PageModel
    {
        private readonly AppointmentSystem.Database.AppointmentContext _context;

		public SelectList DoctorCategories { get; set; }

		public CreateModel(AppointmentSystem.Database.AppointmentContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
			DoctorCategories = new SelectList(_context.DoctorCategories.OrderBy(c => c.Name), nameof(DoctorCategory.Id), nameof(DoctorCategory.Name));
			return Page();
        }

        [BindProperty]
        public Doctor Doctor { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Doctors.Add(Doctor);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
