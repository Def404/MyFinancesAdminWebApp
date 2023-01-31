using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyFinancesAdminWebApp.Models;

/// <summary>
/// В данном классификаторе, содержится информация для визуализации приложения, такая как, название и номер иконки выбранного хранилища
/// </summary>
public partial class LocalStorageClassifier
{
    public int LocalStorageClassifierId { get; set; }
    [Display(Name = "Название")]
    [Required]
    [MaxLength(15)]
    public string Name { get; set; } = null!;
    [Display(Name = "Номер картинки")]
    [Description("номер в Material Desifn")]
    public int PictureNumber { get; set; }
}
