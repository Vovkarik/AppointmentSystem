﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppointmentSystem.Core.Entities;
using AppointmentSystem.Data;

namespace AppointmentSystem.Pages.Admin.Doctors
{
	public class EditModel : PageModel
	{
		private readonly ApplicationDbContext _context;

		public SelectList DoctorCategories { get; set; }

		public EditModel(ApplicationDbContext context)
		{
			_context = context;
		}

		[BindProperty]
		public Doctor Doctor { get; set; }

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if(id == null)
			{
				return NotFound();
			}

			Doctor = await _context.Doctors.FirstOrDefaultAsync(m => m.Id == id);

			if(Doctor == null)
			{
				return NotFound();
			}

			DoctorCategories = new SelectList(_context.DoctorCategories.OrderBy(c => c.Name), nameof(DoctorCategory.Id), nameof(DoctorCategory.Name), Doctor.DoctorCategoryId);

			return Page();
		}

		// To protect from overposting attacks, please enable the specific properties you want to bind to, for
		// more details see https://aka.ms/RazorPagesCRUD.
		public async Task<IActionResult> OnPostAsync()
		{
			if(!ModelState.IsValid)
			{
				return Page();
			}

			_context.Attach(Doctor).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch(DbUpdateConcurrencyException)
			{
				if(!DoctorExists(Doctor.Id))
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

		private bool DoctorExists(int id)
		{
			return _context.Doctors.Any(e => e.Id == id);
		}
	}
}
