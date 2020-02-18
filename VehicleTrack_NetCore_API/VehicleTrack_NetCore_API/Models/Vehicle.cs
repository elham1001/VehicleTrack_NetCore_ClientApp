using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleTrack_NetCore_API.Models
{
    public class Vehicle
    {
        public string VehicleID { get; set; }
        public string RegNo { get; set; }
        public bool Conection { get; set; }
        
        public virtual Customer Customer { get; set; }
        


    }
}
