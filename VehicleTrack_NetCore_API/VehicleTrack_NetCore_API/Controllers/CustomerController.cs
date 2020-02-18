using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VehicleTrack_NetCore_API.Models;

namespace VehicleTrack_NetCore_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]


    public class CustomerController : Controller
    {

        private readonly AppDbContext _appDbContext;
        public CustomerController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        [Route("all")]
        public List<Customer> Get()
        {
            var customer = _appDbContext.Cutomers.ToList();

            return customer;
        }
    }
}