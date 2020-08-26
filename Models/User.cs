using System;
using System.ComponentModel.DataAnnotations;

namespace yyytours.Models
{
    public class User
    {  
        [Key]
        [Display(Name = "אימייל")]
        [Required(ErrorMessage = "מייל אינו יכול להיות ריק")]
        [EmailAddress(ErrorMessage = "מייל אינו תקין")]
        public string Email { get; set; }

        [Display(Name = "שם מלא")]
        [Required(ErrorMessage = "שם אינו יכול להיות ריק")]
        public string FullName { get; set; }

        [Display(Name = "טלפון")]
        [Required(ErrorMessage = "מספר טלפון אינו יכול להיות ריק")]
        [Phone(ErrorMessage = "מספר טלפון אינו תקין")]
        public string Phone { get; set; }

        [Display(Name = "סיסמה")]
        [Required(ErrorMessage = "סיסמה אינה יכולה להיות ריקה")]
        [RegularExpression(@"^([a-zA-Z0-9@#$%^&+=*]{6,10})$", ErrorMessage = "סיסמה לא תקינה")]
        public string Password { get; set; }

        [Display(Name = "תפקיד")]
        public UserType Type { get; set; }

        public User() { }
    }

    public enum UserType
    {
        Tourist = 0, Guide = 1, Admin = 2
    }
}
