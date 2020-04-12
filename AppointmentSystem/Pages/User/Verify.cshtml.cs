using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace AppointmentSystem.Pages
{
	public class VerifyModel : PageModel
	{
		public const string generatedSessionCode = "_Code";
		public IActionResult OnGet(string number)
		{
			if (string.IsNullOrEmpty(HttpContext.Session.GetString(generatedSessionCode)))
			{
				HttpContext.Session.SetString(generatedSessionCode, generateCode());
			}
			return Page();
		}
		
		public IActionResult OnPost(string enteredCode)
		{
			if(enteredCode == HttpContext.Session.GetString(generatedSessionCode))
			{
				return RedirectToPage("/Appointments/Doctors");
			}
			else
			{
				return Page();
			}
		}

		public static string generateCode()
		{
			string num = "0123456789";
			int len = num.Length;
			string code = string.Empty;
			int codeDigits = 6;
			string finalDigit;
			int getIndex;

			for(int i=0; i<codeDigits; i++)
			{
				do
				{
					getIndex = new Random().Next(0, len);
					finalDigit = num.ToCharArray()[getIndex].ToString();
				}
				while (code.IndexOf(finalDigit)!=-1);
				code += finalDigit;
			}

			return code;
		}
	}
}