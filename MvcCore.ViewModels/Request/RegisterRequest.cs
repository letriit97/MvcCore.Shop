using System;
using System.ComponentModel.DataAnnotations;

namespace MvcCore.ViewModels.Request
{
    public class RegisterRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        public string Email { get; set; }
        public string Mobile { get; set; }
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        public string Repassword { get; set; }
    }
}