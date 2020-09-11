using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Autofac;
using Microsoft.AspNetCore.Http;
using Blog.Framework;
using Blog.Web.Areas.Admin.Models.BookModel;
using Blog.Web.Areas.Admin.Models.SettingModel;

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SettingsController : Controller
    {
        private readonly IConfiguration _configuration;
        private FrameworkContext _frameworkContext;

        public SettingsController(IConfiguration configuration, FrameworkContext frameworkContext)
        {
            _configuration = configuration;
            _frameworkContext = frameworkContext;
        }

        public IActionResult Index()
        {
            var model = Startup.AutofacContainer.Resolve<SettingModel>();
            return View(model);
        }

       

        public IActionResult AddSetting()
        {
            var model = new CreateSettingModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddSetting(
            [Bind(nameof(CreateSettingModel.Title),
             nameof(CreateSettingModel.imageFile),
             nameof(CreateSettingModel.Description))] CreateSettingModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.Create();
                    model.Response = new ResponseModel("Setting creation successful.", ResponseType.Success);
                    return RedirectToAction("Index");
                }
                catch (DuplicationException ex)
                {
                    model.Response = new ResponseModel(ex.Message, ResponseType.Failure);
                    // error logger code
                }
                catch (Exception ex)
                {
                    model.Response = new ResponseModel("Setting creation failued.", ResponseType.Failure);
                    // error logger code
                }
            }
            return View(model);
        }

        public IActionResult EditSetting(int id)
        {
            var model = new EditSettingModel();
            model.Load(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditSetting(
            [Bind(nameof(EditSettingModel.Id),
            nameof(EditSettingModel.Title),
            nameof(EditSettingModel.imageFile),
            nameof(EditSettingModel.Description))] EditSettingModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.Edit();
                    model.Response = new ResponseModel("Setting creation successful.", ResponseType.Success);
                    return RedirectToAction("Index");
                }
                catch (DuplicationException ex)
                {
                    model.Response = new ResponseModel(ex.Message, ResponseType.Failure);
                    // error logger code
                }
                catch (Exception ex)
                {
                    model.Response = new ResponseModel("Setting creation failued.", ResponseType.Failure);
                    // error logger code
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteSetting(int id)
        {
            if (ModelState.IsValid)
            {
                var model = new SettingModel();
                try
                {
                    var title = model.Delete(id);
                    model.Response = new ResponseModel($"Setting {title} successfully deleted.", ResponseType.Success);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    model.Response = new ResponseModel("Setting delete failued.", ResponseType.Failure);
                    // error logger code
                }
            }
            return RedirectToAction("index");
        }

        public IActionResult GetSettings()
        {
            var tableModel = new DataTablesAjaxRequestModel(Request);
            var model = Startup.AutofacContainer.Resolve<SettingModel>();
            var data = model.GetSettings(tableModel);
            return Json(data);
        }
    }
}