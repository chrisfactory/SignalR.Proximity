using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Samples.NotifyR.Notifier.AspNetCore.Models;
using NotifyR.Notifier;
using NotifyR.Common;

namespace Samples.NotifyR.Notifier.AspNetCore.Controllers
{


    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INotifyRNotifierFactory _notifier;
        public HomeController(ILogger<HomeController> logger, INotifyRNotifierFactory notifier)
        {
            _logger = logger;
            _notifier = notifier;
        }

        public IActionResult Index()
        {
            //var notifier = _notifier.New<IToaster>();
            
             
            //using (var context = notifier.UseScopeAll().GetProxy())
            //{
            //    for (int i = 0; i < 1; i++)
            //    {
            //        context.Proxy.Notify();
            //    }
            //}
         
            //var n2 = notifier.WithUser("cohl");
            //using (var context = n2.GetProxy())
            //{
            //    for (int i = 0; i < 1; i++)
            //    {
            //        context.Proxy.Notify();
            //    }
            //}


            //var n3 = _notifier.New<IToaster>().WithUser("cohl").UseScopeUser("cohl");
            ////.AddLogging(logging =>
            ////{
            ////    logging.ClearProviders();
            ////      //logging.AddConsole();
            ////      //logging.AddEventLog();
            ////  });
            //using (var context = n3.GetProxy())
            //{
            //    for (int i = 0; i < 1; i++)
            //    {
            //        context.Proxy.Notify();
            //    }
            //}

            //using (var context = notifier.GetProxy())
            //{
            //    for (int i = 0; i < 1; i++)
            //    {
            //        context.Proxy.Notify();
            //    }
            //}

            return View();
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
    }
}
