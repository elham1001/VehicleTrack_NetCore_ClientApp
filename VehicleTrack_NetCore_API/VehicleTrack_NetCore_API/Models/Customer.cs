using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleTrack_NetCore_API.Models
{
    public class Customer
    {
         [Key]
        public int CID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

    }
}
