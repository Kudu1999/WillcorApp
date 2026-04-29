using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WillcorApp.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? AreaCode { get; set; }          // like IS, CBD, RP, R
        public string? ReferenceNumber { get; set; }   // like RPC 238
        public string? Notes { get; set; }

        public ICollection<PickupSchedule> PickupSchedules { get; set; } = new List<PickupSchedule>();
        public ICollection<PickupRunItem> PickupRunItems { get; set; } = new List<PickupRunItem>();
    }
}
