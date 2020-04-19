using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AppointmentSystem.Core.Entities;
using AppointmentSystem.Data;

namespace AppointmentSystem.Pages.Admin.Slots
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
