using System.ComponentModel.DataAnnotations;

namespace MyFinancesAdminWebApp.Models;

public class UserLogin
{
    [Required(ErrorMessage = "Поле должно быть заполненным")]
    [MaxLength(20, ErrorMessage = "Логин должен быть не больше 20 символов")]
    [MinLength(5, ErrorMessage = "Логин должен быть не меньше 5 символов")]
    [RegularExpression(@"[a-zA-Z0-9-_\.]", ErrorMessage = "В логине могут быть буквы, цифры и символы(-, _)")]
    public string Login { get; set; }
    [Required(ErrorMessage = "Поле должно быть заполненным")]
    [MaxLength(20, ErrorMessage = "Пароль должен быть не больше 20 символов")]
    [MinLength(8, ErrorMessage = "Пароль должен быть не меньше 8 символов")]
    [RegularExpression(@"(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$", 
        ErrorMessage = "В пароле могуть быть строчные и прописные латинские буквы, цифры, спецсимволы. " +
                       "Минимум 8 символов. Обязатльно хотя бы одна строчная и прописаня буква и одна цифра")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
