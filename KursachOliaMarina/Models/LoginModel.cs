using System;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace KursachOliaMarina.Models
{
    public class LoginModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
    public class RegisterModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

        [Required]
        public int Age { get; set; }
        [Required]
        public string LName { get; set; }
        [Required]
        public string FName { get; set; }
        [Required]
        public string Sex { get; set; }
        [Required]
        public int Visit { get; set; }
    }
}