using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyFinancesAdminWebApp.Context;
using MyFinancesAdminWebApp.Models;

namespace MyFinancesAdminWebApp.Pages;
[AllowAnonymous]
public class Login : PageModel
{
    [BindProperty]
    public UserLogin UserLogin { get; set; }
    
    public void OnGet()
    {
        
    }
    
    public async Task<ActionResult> OnPost()
    {
        User? user;

        await using (MyfinancesContext db = new MyfinancesContext())
        {
            user = db.Users
                .FromSqlRaw("SELECT * FROM users WHERE login = {0} AND password = crypt({1}, password)",
                    UserLogin.Login,
                    UserLogin.Password)
                .FirstOrDefault();
        }

        if (user == null)
            return Page();

        if (user.Role != "admin")
            return Page();
                
        var claims = new[]{
            new Claim(ClaimTypes.NameIdentifier, user.Login),
            new Claim(ClaimTypes.Role, user.Role)
        };

        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        return RedirectToPage("/Index");
       
    }
}
