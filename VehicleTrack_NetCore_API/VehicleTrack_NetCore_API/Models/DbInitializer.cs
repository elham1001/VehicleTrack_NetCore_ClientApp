using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleTrack_NetCore_API.Models
{
    public class DbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                // Seed the database.

                if (!context.Vehicles.Any())
                {
                    context.AddRange
                    (
                        new Vehicle { VehicleID = "YS2R4X20005399401", RegNo = "ABC123" },
                        new Vehicle { VehicleID = "VLUR4X20009093588", RegNo = "DEF456" },
                        new Vehicle { VehicleID = "VLUR4X20009048067", RegNo = "GHI789" },
                        new Vehicle { VehicleID = "YS2R4X20005388011", RegNo = "JKL012" },
                        new Vehicle { VehicleID = "YS2R4X20005387949", RegNo = "MNO345" },
                        new Vehicle { VehicleID = "VLUR4X20009048066", RegNo = "PQR678" },
                        new Vehicle { VehicleID = "YS2R4X20005387055", RegNo = "STU901" }

                        );
                }

                if (!context.Cutomers.Any())
                {
                    context.AddRange
                    (
                        new Customer { Name = "Kalles Grustransporter AB ", Address = "Cementvägen 8, 111 11 Södertälje" },
                         new Customer { Name = "Johans Bulk AB ", Address = "Balkvägen 12, 222 22 Stockholm" },
                          new Customer { Name = "Haralds Värdetransporter AB  ", Address = "Budgetvägen 1, 333 33 Uppsala" }
                       

                        );
                }


                context.SaveChanges();

            }
        }
    }
}
