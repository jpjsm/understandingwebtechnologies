using System;
using System.Collections.Generic;
using System.Text;

namespace FcmHipri.Interfaces
{
    public interface IEventsInPlace
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IRegionEvents[] EventsPerRegion { get; set; }
    }
}
