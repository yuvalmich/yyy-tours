using System;
namespace yyy_tours.Models
{

    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class User
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required, Phone]
        public string Phone { get; set; }

        [RegularExpression(@"^([a-zA-Z0-9@#$%^&+=*]{6,10})$")]
        public string Password { get; set; }

        public UserType Type { get; set; }

        
        public User() {}
    }

    public enum UserType
    {
        Tourist, Guide, Admin
    }
}
