using System;

namespace MvcCore.ViewModels.ViewModels
{
    public class UserViewModel
    {
        public Guid ID { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
    }
}