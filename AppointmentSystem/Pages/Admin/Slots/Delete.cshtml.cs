using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AppointmentSystem.Database;

namespace AppointmentSystem.Pages.Admin.Slots
{
    public class DeleteModel : PageModel
    {
        private readonly AppointmentSystem.Database.AppointmentContext _context;

        public DeleteModel(AppointmentSystem.Database.AppointmentContext context)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AppointmentSlot = await _context.AppointmentSlots.FindAsync(id);

            if (AppointmentSlot != null)
            {
                _context.AppointmentSlots.Remove(AppointmentSlot);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
