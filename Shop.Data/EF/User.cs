using System;
using System.Collections.Generic;

namespace Shop.Data.EF
{
    public partial class User
    {
        public string Uid { get; set; } = null!;
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? ImageUser { get; set; }
        public string? Account { get; set; }
        public string? Password { get; set; }
        public DateTime? Dob { get; set; }
    }
}
