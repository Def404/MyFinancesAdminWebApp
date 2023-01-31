using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyFinancesAdminWebApp.Context;
using MyFinancesAdminWebApp.Models;

namespace MyFinancesAdminWebApp.Pages.TransactionTypes
{
    public class DeleteModel : PageModel
    {
        private readonly MyFinancesAdminWebApp.Context.MyfinancesContext _context;

        public DeleteModel(MyFinancesAdminWebApp.Context.MyfinancesContext context)
        {
            _context = context;
        }

        [BindProperty]
      public TransactionType TransactionType { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.TransactionTypes == null)
            {
                return NotFound();
            }

            var transactiontype = await _context.TransactionTypes.FirstOrDefaultAsync(m => m.TransactionTypeId == id);

            if (transactiontype == null)
            {
                return NotFound();
            }
            else 
            {
                TransactionType = transactiontype;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.TransactionTypes == null)
            {
                return NotFound();
            }
            var transactiontype = await _context.TransactionTypes.FindAsync(id);

            if (transactiontype != null)
            {
                TransactionType = transactiontype;
                _context.TransactionTypes.Remove(TransactionType);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
