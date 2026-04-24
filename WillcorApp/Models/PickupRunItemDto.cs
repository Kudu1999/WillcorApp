using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WillcorApp.Models
{
    public class PickupRunItemDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? AreaCode { get; set; }
        public string? ReferenceNumber { get; set; }
        public bool IsExtraPickup { get; set; }
        public bool IsCollected { get; set; }
        public int? BagsCollected { get; set; }
        public int? BagsDropped { get; set; }
        public string? Destination { get; set; }
        public string? Notes { get; set; }
        public DateTime? CollectedAt { get; set; }
    }
}
