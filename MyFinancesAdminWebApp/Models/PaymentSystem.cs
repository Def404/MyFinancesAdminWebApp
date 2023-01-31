using System.ComponentModel.DataAnnotations;

namespace MyFinancesAdminWebApp.Models;

/// <summary>
/// Классификатор платежных систем, содержащий в себе название и изображение
/// </summary>
public partial class PaymentSystem
{
    public int PaymentSystemId { get; set; }
    [Display(Name = "Название")]
    [Required]
    [MaxLength(15)]
    public string Name { get; set; } = null!;
    [Display(Name = "Путь")]
    [Required]
    public string ImagePath { get; set; } = null!;
}
