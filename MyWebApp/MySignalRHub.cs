
using Microsoft.AspNetCore.SignalR;
using MyWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MyWebApp
{
   // [HubName("mySignalRHub")]
    public class MySignalRHub : Hub
    {

        [HubMethodName("sendMessage")]  //matches default
        public async Task SendMessage(string user, string message)
        {
            if (Clients != null)
            {
                await Clients.All.SendAsync("ReceiveMessage", user, message);
            }

            
        }
    }
}