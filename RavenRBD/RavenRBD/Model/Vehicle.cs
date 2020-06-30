using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RavenRBD.Model
{
    public class Vehicle
    {

        public int VehicleId { get; set; }
        public String Brand { get; set; }
        public String Model { get; set; }
        public String Type { get; set; }
        public Transporter transporter { get; set; }

    }
}
