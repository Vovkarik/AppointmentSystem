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
    public class IndexModel : PageModel
    {
        private readonly AppointmentSystem.Database.AppointmentContext _context;

        public IndexModel(AppointmentSystem.Database.AppointmentContext context)
        {
            _context = context;
        }

        public IList<DoctorCategory> DoctorCategory { get;set; }

        public async Task OnGetAsync()
        {
            DoctorCategory = await _context.DoctorCategories.ToListAsync();
        }
    }
}
