using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using VehicleTrack_NetCore_API.Models;

namespace VehicleTrack_NetCore_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]


    public class VehicleController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public VehicleController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        [Route ("all")]
        public List<Vehicle> Get()
        {
            var vehicle = _appDbContext.Vehicles.ToList();

            return vehicle;
        }

        [HttpGet]
        [Route("filter")]
        public List<Vehicle> Get(int? CustomerID, int? statusID)
        {
            bool Connected=false;
            if (statusID != -1)
                Connected = statusID == 1 ? true : false;

            var vehicle = _appDbContext.Vehicles.Where(c => (c.Customer.CID == CustomerID || CustomerID==-1) && (c.Conection== Connected || statusID==-1)).ToList();
             

            return vehicle;
        }


    }
}