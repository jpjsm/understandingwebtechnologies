using System;
using System.Collections.Generic;
using System.Text;

namespace FcmHipri.Interfaces
{
    public interface IEventsQuery
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string[] Regions { get; set; }
    }
}
