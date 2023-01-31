using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyFinancesAdminWebApp.Context;
using MyFinancesAdminWebApp.Models;

namespace MyFinancesAdminWebApp.Pages.Banks
{
    [Authorize(Roles = "admin")]
    public class IndexModel : PageModel
    {
        private readonly MyfinancesContext _context;

        public IndexModel(MyfinancesContext context)
        {
            _context = context;
        }

        public IList<Bank> Bank { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Banks != null)
            {
                Bank = await _context.Banks.ToListAsync();
            }
        }
    }
}
