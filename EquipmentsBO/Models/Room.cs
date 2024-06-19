using System;
using System.Collections.Generic;

namespace EquipmentsBO.Models
{
    public partial class Room
    {
        public Room()
        {
            Equipment = new HashSet<Equipment>();
        }

        public int RoomId { get; set; }
        public string RoomName { get; set; } = null!;
        public string Location { get; set; } = null!;
        public string Status { get; set; } = null!;

        public virtual ICollection<Equipment> Equipment { get; set; }
    }
}
