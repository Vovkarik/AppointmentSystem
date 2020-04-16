using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppointmentSystem.Database;

namespace AppointmentSystem.Pages.Admin.Categories
{
    public class EditModel : PageModel
    {
        private readonly AppointmentSystem.Database.AppointmentContext _context;

        public EditModel(AppointmentSystem.Database.AppointmentContext context)
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

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(DoctorCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorCategoryExists(DoctorCategory.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool DoctorCategoryExists(int id)
        {
            return _context.DoctorCategories.Any(e => e.Id == id);
        }
    }
}
