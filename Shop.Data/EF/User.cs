using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Shop.Data.EF
{
    public class User : IdentityUser<Guid>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string? ImageUser { get; set; }
        public string? token { get; set; }
        public bool isAuth { get; set; }
        public DateTime? Dob { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
    }
}
