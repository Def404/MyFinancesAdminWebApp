using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyFinancesAdminWebApp.Context;
using MyFinancesAdminWebApp.Models;

namespace MyFinancesAdminWebApp.Pages.PaymentSystems
{
    public class DetailsModel : PageModel
    {
        private readonly MyFinancesAdminWebApp.Context.MyfinancesContext _context;

        public DetailsModel(MyFinancesAdminWebApp.Context.MyfinancesContext context)
        {
            _context = context;
        }

      public PaymentSystem PaymentSystem { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.PaymentSystems == null)
            {
                return NotFound();
            }

            var paymentsystem = await _context.PaymentSystems.FirstOrDefaultAsync(m => m.PaymentSystemId == id);
            if (paymentsystem == null)
            {
                return NotFound();
            }
            else 
            {
                PaymentSystem = paymentsystem;
            }
            return Page();
        }
    }
}
