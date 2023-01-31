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
    public class DetailsModel : PageModel
    {
        private readonly MyFinancesAdminWebApp.Context.MyfinancesContext _context;

        public DetailsModel(MyFinancesAdminWebApp.Context.MyfinancesContext context)
        {
            _context = context;
        }

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
    }
}
