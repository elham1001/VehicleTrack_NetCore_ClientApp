using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleTrack_NetCore_API.Models;

namespace VehicleTrack_NetCore_API
{
    public class ChatHub : Hub
    {
       
        public override Task OnConnectedAsync()
        {
          
            return base.OnConnectedAsync();
        }
        public void Send(List<Vehicle> vehicles)
        {
            if(Clients!=null)
            Clients.All.SendAsync("broadcastMessage",vehicles);
        }

        public override Task OnDisconnectedAsync(System.Exception exception)
        {
            Clients.All.SendAsync("broadcastMessage", "system", $"{Context.ConnectionId} left the conversation");
            return base.OnDisconnectedAsync(exception);
        }


    }
}
