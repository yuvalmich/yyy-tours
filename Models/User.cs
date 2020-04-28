using System;
namespace yyy_tours.Models
{
    public class User
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public UserType Type { get; set; }

        public User()
        {
        }
    }

    public enum UserType
    {
        Tourist, Guide, Admin
    }
}
