using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WillcorApp.Models
{
    public class UpdatePickupRunItemDto
    {
        public bool IsCollected { get; set; }
        public int? BagsCollected { get; set; }
        public int? BigBagsCollected { get; set; }
        public int? SmallBagsCollected { get; set; }
        public double? TrailerLoadsCollected { get; set; }
        public int? BagsDropped { get; set; }
        public string? Notes { get; set; }
    }
}
