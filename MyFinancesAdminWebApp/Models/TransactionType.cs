using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyFinancesAdminWebApp.Models;

/// <summary>
/// Классификатор для определения вида транзакции (перевод, платеж, пополнение и т.п.)
/// </summary>
public partial class TransactionType
{
    public int TransactionTypeId { get; set; }
    [Display(Name = "Название")]
    [MaxLength(20)]
    [Required]
    public string Name { get; set; } = null!;
    [Display(Name = "Класс транзакции", Description = "+ пополнение / - списание")]
    [Required]
    public char Class { get; set; }
}
