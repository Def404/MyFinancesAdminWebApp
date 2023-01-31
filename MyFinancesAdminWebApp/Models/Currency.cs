using System.ComponentModel.DataAnnotations;

namespace MyFinancesAdminWebApp.Models;

/// <summary>
/// Сущность хранит наименования валют, их сокращенные названия и символы
/// </summary>
public partial class Currency
{
    public int CurrencyId { get; set; }
    [Display(Name = "Название")]
    [Required]
    [MaxLength(10)]
    public string Name { get; set; } = null!;
    [Display(Name = "Код")]
    [Required]
    [MaxLength(3)]
    [MinLength(3)]
    public string ShortName { get; set; } = null!;
    [Display(Name = "Символ")]
    [Required]
    public char Sign { get; set; }
}
