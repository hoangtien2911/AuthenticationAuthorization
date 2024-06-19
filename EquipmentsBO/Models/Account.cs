using System;
using System.Collections.Generic;

namespace EquipmentsBO.Models
{
    public partial class Account
    {
        public string Email { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public DateTime? Birthday { get; set; }
        public string? Status { get; set; }
        public int RoleId { get; set; }
    }
}
