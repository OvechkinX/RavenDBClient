using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RavenRBD.Model
{
    public class Load
    {

        public int LoadId { get; set; }
        public int Weight { get; set; }
        public string Type { get; set; }
        public Loader loader { get; set; }

    }
}
