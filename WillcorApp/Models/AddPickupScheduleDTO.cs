using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WillcorApp.Models
{
    public class AddPickupScheduleDTO
    {
        public int ClientId { get; set; }
        public string? Frequency { get; set; } = string.Empty;
        public string? Destination { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public int WeekInterval { get; set; } = 1;
        public DateTime StartDate { get; set; }
        public List<DayOfWeek> CollectionDays { get; set; } = new();
    }
}
