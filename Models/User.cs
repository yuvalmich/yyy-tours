﻿using System;
namespace yyy_tours.Models
{

    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class User
    {
        [Required(ErrorMessage = "מייל אינו יכול להיות ריק")]
        [EmailAddress(ErrorMessage = "מייל אינו תקין")]
        public string Email { get; set; }

        [Required(ErrorMessage = "שם אינו יכול להיות ריק")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "מספר טלפון אינו יכול להיות ריק")]
        [Phone(ErrorMessage = "מספר טלפון אינו תקין")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "סיסמה אינה יכולה להיות ריקה")]
        [RegularExpression(@"^([a-zA-Z0-9@#$%^&+=*]{6,10})$", ErrorMessage ="סיסמה לא תקינה")]
        public string Password { get; set; }

        public UserType Type { get; set; }

        
        public User() {}
    }

    public enum UserType
    {
        Tourist, Guide, Admin
    }
}