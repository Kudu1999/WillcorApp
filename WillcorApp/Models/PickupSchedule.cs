using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WillcorApp.Models
{
    public class PickupSchedule
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public Client? Client { get; set; } = null!;
        public string Frequency { get; set; } = string.Empty;   // Weekly, Free, etc.
        public string CollectionDay { get; set; } = string.Empty; // Wednesday, Tuesday, etc.
        public string? Destination { get; set; } = string.Empty;  // Collect, Garden, Bus Refuse
        public bool IsActive { get; set; } = true;
    }
}
