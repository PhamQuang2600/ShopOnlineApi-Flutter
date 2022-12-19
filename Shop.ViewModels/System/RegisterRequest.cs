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
        public string user { get; set; }
        
        public string name { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        
        public string phone { get; set; }

        [DataType(DataType.Password)]
        public string password { get; set; }
            
        [DataType(DataType.Password)]
        public string confirmpassword { get; set; }
    }
}
