using System.ComponentModel.DataAnnotations;

namespace MyFinancesAdminWebApp.Models;

/// <summary>
/// В данном классификаторе содержится название банка и его цвет (для визуализации)
/// </summary>
public partial class Bank
{
    public int BankId { get; set; }
    [Required(ErrorMessage = "Обязательное поле")]
    [MaxLength(20)]
    [Display(Name = "Название")]
    public string Name { get; set; } = null!;
    [Required(ErrorMessage = "Обязательное поле")]
    [MaxLength(6)]
    [MinLength(6)]
    [Display(Name = "Цвет")]
    public string Colour { get; set; } = null!;
}
