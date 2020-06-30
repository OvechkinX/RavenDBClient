using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RavenRBD.Model
{
    public class Order
    {

        public int orderID { get; set; }
        public int price { get; set; }
        public DateTime completeTime { get; set; }
        public Vehicle vehicle { get; set; }
        public Load load { get; set; }
        public Forwarder forwarder { get; set; }

    }
}
