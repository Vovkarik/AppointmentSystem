using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AppointmentSystem.Core.Entities;
using AppointmentSystem.Data;

namespace AppointmentSystem.Pages.Admin.Slots
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<AppointmentSlot> AppointmentSlot { get;set; }

        public async Task OnGetAsync()
        {
            AppointmentSlot = await _context.AppointmentSlots.ToListAsync();
        }
    }
}
