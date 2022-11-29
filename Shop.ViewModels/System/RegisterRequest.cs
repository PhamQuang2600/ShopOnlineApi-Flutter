using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Shop.ViewModels.System
{
    public class RegisterRequest
    {
        public string Account { get; set; }
        
        public string Name { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime Dob { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        
        public string Phone { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
            
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
