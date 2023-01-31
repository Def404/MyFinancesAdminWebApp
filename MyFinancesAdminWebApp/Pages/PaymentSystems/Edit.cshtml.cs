using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyFinancesAdminWebApp.Context;
using MyFinancesAdminWebApp.Models;

namespace MyFinancesAdminWebApp.Pages.PaymentSystems
{
    [Authorize(Roles = "admin")]
    public class EditModel : PageModel
    {
        private readonly MyFinancesAdminWebApp.Context.MyfinancesContext _context;

        public EditModel(MyFinancesAdminWebApp.Context.MyfinancesContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PaymentSystem PaymentSystem { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.PaymentSystems == null)
            {
                return NotFound();
            }

            var paymentsystem =  await _context.PaymentSystems.FirstOrDefaultAsync(m => m.PaymentSystemId == id);
            if (paymentsystem == null)
            {
                return NotFound();
            }
            PaymentSystem = paymentsystem;
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

            _context.Attach(PaymentSystem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentSystemExists(PaymentSystem.PaymentSystemId))
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

        private bool PaymentSystemExists(int id)
        {
          return (_context.PaymentSystems?.Any(e => e.PaymentSystemId == id)).GetValueOrDefault();
        }
    }
}
