using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WillcorApp.Models
{
    public class PickupDay
    {
        public int Id { get; set; }

        public DayOfWeek Day { get; set; }

        public int PickupScheduleId { get; set; }
    }
}
