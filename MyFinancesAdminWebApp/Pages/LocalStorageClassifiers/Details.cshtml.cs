using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyFinancesAdminWebApp.Context;
using MyFinancesAdminWebApp.Models;

namespace MyFinancesAdminWebApp.Pages.LocalStorageClassifiers
{
    public class DetailsModel : PageModel
    {
        private readonly MyFinancesAdminWebApp.Context.MyfinancesContext _context;

        public DetailsModel(MyFinancesAdminWebApp.Context.MyfinancesContext context)
        {
            _context = context;
        }

      public LocalStorageClassifier LocalStorageClassifier { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.LocalStorageClassifiers == null)
            {
                return NotFound();
            }

            var localstorageclassifier = await _context.LocalStorageClassifiers.FirstOrDefaultAsync(m => m.LocalStorageClassifierId == id);
            if (localstorageclassifier == null)
            {
                return NotFound();
            }
            else 
            {
                LocalStorageClassifier = localstorageclassifier;
            }
            return Page();
        }
    }
}
