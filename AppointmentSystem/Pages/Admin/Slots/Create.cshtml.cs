using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AppointmentSystem.Core.Entities;
using AppointmentSystem.Data;

namespace AppointmentSystem.Pages.Admin.Slots
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

		public SelectList Doctors { get; set; }

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
			Doctors = new SelectList(_context.Doctors.OrderBy(d => d.DoctorCategoryId), nameof(Doctor.Id), nameof(Doctor.FullName));
            return Page();
        }

        [BindProperty]
        public AppointmentSlot AppointmentSlot { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.AppointmentSlots.Add(AppointmentSlot);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
