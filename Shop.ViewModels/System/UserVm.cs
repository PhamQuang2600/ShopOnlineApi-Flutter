using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Shop.ViewModels.System
{
    public class UserVm
    {
        public Guid Uid { get; set; }
        
        public string Name { get; set; }
        
        public string UserName { get; set; }
        
        public string Password { get; set; }
        public string ImageUser { get; set; }
        
        public DateTime Dob { get; set; }

        [Phone]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$")]
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}