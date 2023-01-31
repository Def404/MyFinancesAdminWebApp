using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyFinancesAdminWebApp.Context;
using MyFinancesAdminWebApp.Models;

namespace MyFinancesAdminWebApp.Pages.LocalStorageClassifiers
{
    public class EditModel : PageModel
    {
        private readonly MyFinancesAdminWebApp.Context.MyfinancesContext _context;

        public EditModel(MyFinancesAdminWebApp.Context.MyfinancesContext context)
        {
            _context = context;
        }

        [BindProperty]
        public LocalStorageClassifier LocalStorageClassifier { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.LocalStorageClassifiers == null)
            {
                return NotFound();
            }

            var localstorageclassifier =  await _context.LocalStorageClassifiers.FirstOrDefaultAsync(m => m.LocalStorageClassifierId == id);
            if (localstorageclassifier == null)
            {
                return NotFound();
            }
            LocalStorageClassifier = localstorageclassifier;
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

            _context.Attach(LocalStorageClassifier).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocalStorageClassifierExists(LocalStorageClassifier.LocalStorageClassifierId))
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

        private bool LocalStorageClassifierExists(int id)
        {
          return (_context.LocalStorageClassifiers?.Any(e => e.LocalStorageClassifierId == id)).GetValueOrDefault();
        }
    }
}
