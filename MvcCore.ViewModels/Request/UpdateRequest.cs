using System;
using System.ComponentModel.DataAnnotations;

namespace MvcCore.ViewModels.Request
{
    public class UpdateRequest
    {
        public Guid ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        public string Email { get; set; }
        public string Mobile { get; set; }
    }
}