using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppointmentSystem.Database;

namespace AppointmentSystem.Pages.AppointmentSlots
{
    public class EditModel : PageModel
    {
        private readonly AppointmentSystem.Database.AppointmentContext _context;

        public EditModel(AppointmentSystem.Database.AppointmentContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AppointmentSlot AppointmentSlot { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AppointmentSlot = await _context.AppointmentSlots.FirstOrDefaultAsync(m => m.Id == id);

            if (AppointmentSlot == null)
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

            _context.Attach(AppointmentSlot).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentSlotExists(AppointmentSlot.Id))
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

        private bool AppointmentSlotExists(int id)
        {
            return _context.AppointmentSlots.Any(e => e.Id == id);
        }
    }
}
