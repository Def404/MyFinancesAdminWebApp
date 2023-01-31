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
    public class IndexModel : PageModel
    {
        private readonly MyFinancesAdminWebApp.Context.MyfinancesContext _context;

        public IndexModel(MyFinancesAdminWebApp.Context.MyfinancesContext context)
        {
            _context = context;
        }

        public IList<LocalStorageClassifier> LocalStorageClassifier { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.LocalStorageClassifiers != null)
            {
                LocalStorageClassifier = await _context.LocalStorageClassifiers.ToListAsync();
            }
        }
    }
}
