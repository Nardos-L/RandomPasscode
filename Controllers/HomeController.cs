using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RandomPasscode.Models;
using Microsoft.AspNetCore.Http;

namespace RandomPasscode.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet("")]
        public ViewResult Index()
        {
            
            if (HttpContext.Session.GetInt32("Count") == null)
            {
                
                HttpContext.Session.SetInt32("Count", 1);
            }

            else 
            {
                int count = HttpContext.Session.GetInt32("Count").Value;
                Console.WriteLine($"Session is working {count}");
                HttpContext.Session.SetInt32("Count", count+1);
            }


            ViewBag.Count = HttpContext.Session.GetInt32("Count");

            Random num = new Random();
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string randomPassword = "";
            for (var i=0; i < 14; i++)
            {
                randomPassword += chars[num.Next(chars.Length)];
            }
            ViewBag.Password = randomPassword;
            return View();
        }

        [HttpGet("/generate")]
        public IActionResult Generate()
        {
            return RedirectToAction("Index");
        }

        [HttpGet("/clear")]
        public IActionResult Clear()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
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
