using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FcmHipri.Interfaces;

namespace FcmHipri.Models
{
    public class RegionEvents
    {
        public string Region { get; set; }
        public string[] Events { get; set; }
    }
}
