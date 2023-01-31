using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyFinancesAdminWebApp.Context;
using MyFinancesAdminWebApp.Models;

namespace MyFinancesAdminWebApp.Pages.Currencies
{
    [Authorize(Roles = "admin")]
    public class DeleteModel : PageModel
    {
        private readonly MyFinancesAdminWebApp.Context.MyfinancesContext _context;

        public DeleteModel(MyFinancesAdminWebApp.Context.MyfinancesContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Currency Currency { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Currencies == null)
            {
                return NotFound();
            }

            var currency = await _context.Currencies.FirstOrDefaultAsync(m => m.CurrencyId == id);

            if (currency == null)
            {
                return NotFound();
            }
            else 
            {
                Currency = currency;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Currencies == null)
            {
                return NotFound();
            }
            var currency = await _context.Currencies.FindAsync(id);

            if (currency != null)
            {
                Currency = currency;
                _context.Currencies.Remove(Currency);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
