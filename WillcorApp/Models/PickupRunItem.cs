using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WillcorApp.Models
{
    public class PickupRunItem
    {
        public int Id { get; set; }

        public int PickupRunId { get; set; }
        public PickupRun? PickupRun { get; set; }

        public int ClientId { get; set; }
        public Client? Client { get; set; }

        public bool IsExtraPickup { get; set; } = false;
        public bool IsCollected { get; set; } = false;

        public int? BagsCollected { get; set; }
        public int? BigBagsCollected { get; set; }
        public int? SmallBagsCollected { get; set; }
        public double? TrailerLoadsCollected { get; set; }
        public int? BagsDropped { get; set; }

        public string? Destination { get; set; }
        public string? Notes { get; set; }
        public DateTime? CollectedAt { get; set; }
    }
}
