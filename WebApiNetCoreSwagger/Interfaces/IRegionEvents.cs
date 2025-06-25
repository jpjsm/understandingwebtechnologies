using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FcmHipri.Interfaces
{
    public interface IRegionEvents
    {
        public string Region { get; set; }
        public string[] Events { get; set; }
    }
}
