using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using VehicleTrack_NetCore_API.Controllers;
 
 
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.SignalR;

//using Microsoft.AspNet.SignalR;

namespace VehicleTrack_NetCore_API.Models
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private int executionCount = 0;
        private readonly ILogger<TimedHostedService> _logger;
        private Timer _timer;
        private readonly IServiceScopeFactory _scopeFactory;

        private readonly IHubContext<ChatHub>  _graphHubContext;

        private readonly HttpClient _client;
        private HubConnection _connection;
        private  string HubURL = "https://localhost:44317/chathub";


        public TimedHostedService(ILogger<TimedHostedService> logger, IServiceScopeFactory scopeFactory, IHubContext<ChatHub> graphHubContext)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;

            _graphHubContext = graphHubContext;




        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
          
            _timer = new Timer(DoWork, null, TimeSpan.Zero,
            TimeSpan.FromSeconds(60));

            return Task.CompletedTask;
        }
       

        private void DoWork(object state)
        {

            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                var allVehicles = dbContext.Vehicles.ToList();
                
                foreach (Vehicle vehicle in allVehicles)
                {
                    Random randomNr = new Random();
                    bool connection = (Convert.ToInt32(randomNr.Next(0, 2)) == 1 ? true : false);
                    vehicle.Conection = connection;
                }
                dbContext.SaveChanges();

                _graphHubContext.Clients.All.SendAsync("broadcastMessage", allVehicles);

               // _connection = new HubConnectionBuilder().WithUrl(HubURL).Build();
               // _connection.SendAsync("broadcastMessage", allVehicles);

                //HubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();

                //var context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
                //context.Clients.All.methodInJavascript("hello world");

                //ChatHub sendStatus = new ChatHub();
                //sendStatus.Send(allVehicles);
            }

            // _client.GetAsync("https://localhost:44317/api/Vehicle/all");

        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

       
         
    }
}
