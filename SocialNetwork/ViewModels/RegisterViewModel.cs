using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.ViewModels
{
    public class RegisterViewModel
    {

        [Required]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string EmailReg { get; set; }

        [Required]
        [Display(Name = "Год")]
        public int Year { get; set; }

        [Required]
        [Display(Name = "День")]
        public int Date { get; set; }

        [Required]
        [Display(Name = "Месяц")]
        public int Month { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        [StringLength(100, ErrorMessage = "Поле {0} должно иметь минимум {2} и максимум {1} символов.", MinimumLength = 5)]
        public string PasswordReg { get; set; }

        [Required]
        [Compare("PasswordReg", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }

        [Required]
        [Display(Name = "Никнейм")]
        public string Login { get; set; }
        
        // [Required(ErrorMessage = "Поле Имя обязательно для заполнения")]
        // [DataType(DataType.Text)]
        // [Display(Name = "Имя", Prompt = "Введите имя")]
        // public string FirstName { get; set; }
        //
        // [Required(ErrorMessage ="Поле Фамилия обязательно для заполнения")]
        // [DataType(DataType.Text)]
        // [Display(Name = "Фамилия", Prompt = "Введите фамилию")]
        // public string LastName { get; set; }
        //
        // [Required(ErrorMessage = "Поле Email обязательно для заполнения")]
        // [EmailAddress]
        // [Display(Name = "Email", Prompt = "example.com")]
        // public string EmailReg { get; set; }
        //
        // [Required(ErrorMessage = "Поле Год обязательно для заполнения")]
        // [Display(Name = "Год", Prompt = "Год")]
        // public int? Year { get; set; }
        //
        // [Required(ErrorMessage = "Поле День обязательно для заполнения")]
        // [Display(Name = "День", Prompt = "День")]
        // public int? Date { get; set; }
        //
        // [Required(ErrorMessage = "Поле Месяц обязательно для заполнения")]
        // [Display(Name = "Месяц", Prompt = "Месяц")]
        // public int? Month { get; set; }
        //
        // [Required(ErrorMessage = "Поле Пароль обязательно для заполнения")]
        // [DataType(DataType.Password)]
        // [Display(Name = "Пароль", Prompt = "Введите пароль")]
        // [StringLength(100, ErrorMessage = "Поле {0} должно иметь минимум {2} и максимум {1} символов.", MinimumLength = 5)]
        // public string PasswordReg { get; set; }
        //
        // [Required(ErrorMessage = "Обязательно подтвердите пароль")]
        // [Compare("PasswordReg", ErrorMessage = "Пароли не совпадают")]
        // [DataType(DataType.Password)]
        // [Display(Name = "Подтвердить пароль", Prompt = "Введите пароль повторно")]
        // public string PasswordConfirm { get; set; }
        //
        // public string Login => EmailReg;
    }
}
