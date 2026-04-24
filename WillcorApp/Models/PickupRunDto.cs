using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WillcorApp.Models
{
    public class PickupRunDto
    {
        public int Id { get; set; }
        public DateTime RunDate { get; set; }
        public string DayName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public List<PickupRunItemDto> Items { get; set; } = new();
    }
}
