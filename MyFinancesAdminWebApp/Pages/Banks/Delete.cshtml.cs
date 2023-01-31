using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyFinancesAdminWebApp.Models;

namespace MyFinancesAdminWebApp.Pages.Banks
{
    public class DeleteModel : PageModel
    {
        private readonly Context.MyfinancesContext _context;

        public DeleteModel(Context.MyfinancesContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Banks == null)
            {
                return NotFound();
            }
            var bank = await _context.Banks.FindAsync(id);

            if (bank != null)
            {
                Bank = bank;
                _context.Banks.Remove(Bank);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
