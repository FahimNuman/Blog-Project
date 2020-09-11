using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Framework;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public class SettingsController : Controller
    {
        private FrameworkContext _frameworkContext;
        public SettingsController( FrameworkContext frameworkContext)
        {
            
            _frameworkContext = frameworkContext;
        }
        public IActionResult Index()
        {
            var setting = _frameworkContext.Settings.ToList();
            ViewBag.Settings = setting;
            return View();
        }

        
    }
}