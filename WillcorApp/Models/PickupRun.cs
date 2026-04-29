using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WillcorApp.Models
{
    public class PickupRun
    {
        public int Id { get; set; }
        public DateTime RunDate { get; set; }
        public string DayName { get; set; } = string.Empty;
        public string Status { get; set; } = "Open";

        public ICollection<PickupRunItem> PickupRunItems { get; set; } = new List<PickupRunItem>();
    }
}
