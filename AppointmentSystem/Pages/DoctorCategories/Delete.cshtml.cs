using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AppointmentSystem.Database;

namespace AppointmentSystem.Pages.DoctorCategories
{
    public class DeleteModel : PageModel
    {
        private readonly AppointmentSystem.Database.AppointmentContext _context;

        public DeleteModel(AppointmentSystem.Database.AppointmentContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DoctorCategory DoctorCategory { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DoctorCategory = await _context.DoctorCategories.FirstOrDefaultAsync(m => m.Id == id);

            if (DoctorCategory == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DoctorCategory = await _context.DoctorCategories.FindAsync(id);

            if (DoctorCategory != null)
            {
                _context.DoctorCategories.Remove(DoctorCategory);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
