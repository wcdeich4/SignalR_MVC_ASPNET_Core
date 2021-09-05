using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using MyWebApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IHubContext<MySignalRHub> _hubContext;

        public HomeController(ILogger<HomeController> logger, IHubContext<MySignalRHub> hubContext)
        {
            _logger = logger;
            _hubContext = hubContext;
        }

        public async Task<IActionResult> Index()
        {
            ViewResult viewResult = View();

            Thread backgroundThread1 = new Thread(new ThreadStart(this.introductionMessage)) ;
            backgroundThread1.Start();

            TimeMessageGenerator timeMessageGenerator = new TimeMessageGenerator();
            Thread backgroundThread2 = new Thread(new ThreadStart(timeMessageGenerator.run));
            backgroundThread2.Start();

            return viewResult;
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        private void introductionMessage()
        {
            Thread.Sleep(5000);
            _hubContext.Clients.All.SendAsync("ReceiveMessage", "HomeController ", " This is a demo of MVC ASP.NET Core with SignalR that can be used in Linux");
        }
    }
}
