using System;
using System.Collections.Generic;

namespace EquipmentsBO.Models
{
    public partial class Equipment
    {
        public int EqId { get; set; }
        public string EqCode { get; set; } = null!;
        public string EqName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string SupplierName { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int Quantity { get; set; }
        public int Status { get; set; }
        public int RoomId { get; set; }

        public virtual Room Room { get; set; } = null!;
    }
}
