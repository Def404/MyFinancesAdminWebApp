using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyFinancesAdminWebApp.Context;
using MyFinancesAdminWebApp.Models;

namespace MyFinancesAdminWebApp.Pages.TransactionTypes
{
    [Authorize(Roles = "admin")]
    public class IndexModel : PageModel
    {
        private readonly MyFinancesAdminWebApp.Context.MyfinancesContext _context;

        public IndexModel(MyFinancesAdminWebApp.Context.MyfinancesContext context)
        {
            _context = context;
        }

        public IList<TransactionType> TransactionType { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.TransactionTypes != null)
            {
                TransactionType = await _context.TransactionTypes.ToListAsync();
            }
        }
    }
}
