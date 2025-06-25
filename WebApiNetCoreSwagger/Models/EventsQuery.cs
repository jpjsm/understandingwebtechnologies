using FcmHipri.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FcmHipri.Models
{
    public class EventsQuery
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string[] Regions { get; set; }
    }
}
