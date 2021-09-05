using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace MyWebApp.Models
{
    public class TimeMessageGenerator
    {
        private IHubContext<MySignalRHub> mySignalRHubContext;

        public IHubContext<MySignalRHub> MySignalRHubContext
        {
            get 
            {
                if (mySignalRHubContext == null)
                {
                    mySignalRHubContext = (IHubContext<MySignalRHub>)SingletonGlobalDictionary.Instance["MySignalRHubContextInstance"];
                }
                return mySignalRHubContext;
            }
        }


        public void run()
        {
            DateTime stopTime = DateTime.Now.AddDays(1);
            while (DateTime.Now < stopTime)
            {
                Task task = MySignalRHubContext.Clients.All.SendAsync("ReceiveMessage", "background thread ", "the server time is: " + DateTime.Now.ToLongTimeString());
                task.Wait();
                Thread.Sleep(5000);
            }
        }
    }
}