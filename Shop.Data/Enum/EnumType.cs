using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Data.Enum
{
    public class EnumType
    {
       
        public enum Payment
        {
            unpaid, success, fail
        }

        
        public enum Status
        {
            success = 1,
            fail = 0
        }
    }
}
