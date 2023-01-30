using System.Diagnostics;
using System.Net;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyFinancesAdminWebApp.Controllers;
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
    
    public ActionResult OnPost()
    {

        if (!ModelState.IsValid)
            return Page();
        
        var response = LoginController.GetUserToken(UserLogin.Login, UserLogin.Password);
        
        Debug.WriteLine(response.Content);
        if (!response.IsSuccessStatusCode)
        {
            return Page();
        }

        var content = JsonNode.Parse(response.Content);
        var token = content["token"].ToString();
        
        
        
        return RedirectToPage("/Privacy");
    }
}
