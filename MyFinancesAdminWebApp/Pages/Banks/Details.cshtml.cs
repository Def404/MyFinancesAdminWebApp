using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyFinancesAdminWebApp.Context;
using MyFinancesAdminWebApp.Models;

namespace MyFinancesAdminWebApp.Pages.Banks
{
    public class DetailsModel : PageModel
    {
        private readonly MyfinancesContext _context;

        public DetailsModel(MyfinancesContext context)
        {
            _context = context;
        }

      public Bank Bank { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Banks == null)
            {
                return NotFound();
            }

            var bank = await _context.Banks.FirstOrDefaultAsync(m => m.BankId == id);
            if (bank == null)
            {
                return NotFound();
            }
            else 
            {
                Bank = bank;
            }
            return Page();
        }
    }
}
