using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyFinancesAdminWebApp.Models;

namespace MyFinancesAdminWebApp.Pages.Banks
{
    [Authorize(Roles = "admin")]
    public class EditModel : PageModel
    {
        private readonly Context.MyfinancesContext _context;

        public EditModel(Context.MyfinancesContext context)
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

            var bank =  await _context.Banks.FirstOrDefaultAsync(m => m.BankId == id);
            if (bank == null)
            {
                return NotFound();
            }
            Bank = bank;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Bank).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BankExists(Bank.BankId))
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

        private bool BankExists(int id)
        {
          return (_context.Banks?.Any(e => e.BankId == id)).GetValueOrDefault();
        }
    }
}
