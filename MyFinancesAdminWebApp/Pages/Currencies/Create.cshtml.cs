using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyFinancesAdminWebApp.Context;
using MyFinancesAdminWebApp.Models;

namespace MyFinancesAdminWebApp.Pages.Currencies
{
	[Authorize(Roles = "admin")]
	public class CreateModel : PageModel
	{
		private readonly MyfinancesContext _context;

		public CreateModel(MyfinancesContext context)
		{
			_context = context;
		}

		public IActionResult OnGet()
		{
			return Page();
		}

		[BindProperty] public Currency Currency { get; set; } = default!;


		// To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid || _context.Currencies == null || Currency == null)
			{
				return Page();
			}

			_context.Currencies.Add(Currency);
			await _context.SaveChangesAsync();

			return RedirectToPage("./Index");
		}
	}
}