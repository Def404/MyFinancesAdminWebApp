using System.ComponentModel.DataAnnotations;

namespace MyFinancesAdminWebApp.Models;

public class UserLogin
{
    [Required(ErrorMessage = "Ээээ введи меня слыш")]
    public string Login { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
