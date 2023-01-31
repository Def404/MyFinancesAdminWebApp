using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyFinancesAdminWebApp.Context;
using MyFinancesAdminWebApp.Models;

namespace MyFinancesAdminWebApp.Pages.LocalStorageClassifiers
{
    public class CreateModel : PageModel
    {
        private readonly MyFinancesAdminWebApp.Context.MyfinancesContext _context;

        public CreateModel(MyFinancesAdminWebApp.Context.MyfinancesContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public LocalStorageClassifier LocalStorageClassifier { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.LocalStorageClassifiers == null || LocalStorageClassifier == null)
            {
                return Page();
            }

            _context.LocalStorageClassifiers.Add(LocalStorageClassifier);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
