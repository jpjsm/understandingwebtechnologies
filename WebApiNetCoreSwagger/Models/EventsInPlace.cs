using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FcmHipri.Interfaces;

namespace FcmHipri.Models
{
    public class EventsInPlace
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public RegionEvents[] EventsPerRegion { get; set; }
    }
}
