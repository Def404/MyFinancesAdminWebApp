using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyFinancesAdminWebApp.Context;
using MyFinancesAdminWebApp.Models;

namespace MyFinancesAdminWebApp.Pages.Banks
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

		[BindProperty] public Bank Bank { get; set; } = default!;


		// To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid || _context.Banks == null || Bank == null)
			{
				return Page();
			}

			_context.Banks.Add(Bank);
			await _context.SaveChangesAsync();

			return RedirectToPage("./Index");
		}
	}
}