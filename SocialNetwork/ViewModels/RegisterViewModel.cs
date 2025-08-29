using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Имя обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Имя", Prompt = "Введите имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Фамилия обязательна для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Фамилия", Prompt = "Введите фамилию")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email обязательно для заполнения")]
        [EmailAddress]
        [Display(Name = "Email", Prompt = "example.com")]
        public string EmailReg { get; set; }

        [Required(ErrorMessage = "Год обязательно для заполнения")]
        [Display(Name = "Год", Prompt = "Год")]
        public int? Year { get; set; }

        [Required(ErrorMessage = "День обязательно для заполнения")]
        [Display(Name = "День", Prompt = "День")]
        public int? Date { get; set; }

        [Required(ErrorMessage = "Месяц обязательно для заполнения")]
        [Display(Name = "Месяц", Prompt = "Месяц")]
        public int? Month { get; set; }

        [Required(ErrorMessage = "Пароль обязательно для заполнения")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль", Prompt = "Введите пароль")]
        [StringLength(100, ErrorMessage = "Поле {0} должно иметь минимум {2} и максимум {1} символов.", MinimumLength = 5)]
        public string PasswordReg { get; set; }

        [Required(ErrorMessage = "Обязательно подтвердите пароль")]
        [Compare("PasswordReg", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль", Prompt = "Введите пароль повторно")]
        public string PasswordConfirm { get; set; }

        public string Login => EmailReg;
    }
}