using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Blog.Web.Models;
using NuGet.Frameworks;
using Blog.Framework;

namespace Blog.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
     
        private FrameworkContext _frameworkContext;

        public HomeController(ILogger<HomeController> logger, FrameworkContext frameworkContext)
        {
            _logger = logger;
            _frameworkContext = frameworkContext;
        }

        public IActionResult Index()
        {
            var book = _frameworkContext.Books.ToList();
            ViewBag.Books = book;
            return View();

        }

        public IActionResult Detailspost()
        {
            var setting = _frameworkContext.Settings.ToList();
            ViewBag.Settings = setting;
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
