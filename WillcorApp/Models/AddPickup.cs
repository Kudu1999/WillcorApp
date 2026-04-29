using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WillcorApp.Models
{
    public class AddPickup
    {
        public int ClientId { get; set; }
        public string? Destination { get; set; }
        public string? Notes { get; set; }
    }
}
