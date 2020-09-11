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

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BooksController : Controller
    {
        private readonly IConfiguration _configuration;
        private FrameworkContext _frameworkContext;

        public BooksController(IConfiguration configuration, FrameworkContext frameworkContext)
        {
            _configuration = configuration;
            _frameworkContext = frameworkContext;
        }

        public IActionResult Index()
        {
            var model = Startup.AutofacContainer.Resolve<BookModel>();
            return View(model);
        }

        

        public IActionResult AddBook()
        {
            var model = new CreateBookModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddBook(
            [Bind(nameof(CreateBookModel.Title),
             nameof(CreateBookModel.imageFile),
             nameof(CreateBookModel.Description),       
             nameof(CreateBookModel.Edition))] CreateBookModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.Create();
                    model.Response = new ResponseModel("Book creation successful.", ResponseType.Success);
                    return RedirectToAction("Index");
                }
                catch (DuplicationException ex)
                {
                    model.Response = new ResponseModel(ex.Message, ResponseType.Failure);
                    // error logger code
                }
                catch (Exception ex)
                {
                    model.Response = new ResponseModel("Book creation failued.", ResponseType.Failure);
                    // error logger code
                }
            }
            return View(model);
        }

        public IActionResult EditBook(int id)
        {
            var model = new EditBookModel();
            model.Load(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditBook(
            [Bind(nameof(EditBookModel.Id),
            nameof(EditBookModel.Title),
            nameof(CreateBookModel.imageFile),
            nameof(EditBookModel.Description),
            nameof(EditBookModel.Edition))] EditBookModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.Edit();
                    model.Response = new ResponseModel("Book creation successful.", ResponseType.Success);
                    return RedirectToAction("Index");
                }
                catch (DuplicationException ex)
                {
                    model.Response = new ResponseModel(ex.Message, ResponseType.Failure);
                    // error logger code
                }
                catch (Exception ex)
                {
                    model.Response = new ResponseModel("Book creation failued.", ResponseType.Failure);
                    // error logger code
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteBook(int id)
        {
            if (ModelState.IsValid)
            {
                var model = new BookModel();
                try
                {
                    var title = model.Delete(id);
                    model.Response = new ResponseModel($"Book {title} successfully deleted.", ResponseType.Success);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    model.Response = new ResponseModel("Book delete failued.", ResponseType.Failure);
                    // error logger code
                }
            }
            return RedirectToAction("index");
        }

        public IActionResult GetBooks()
        {
            var tableModel = new DataTablesAjaxRequestModel(Request);
            var model = Startup.AutofacContainer.Resolve<BookModel>();
            var data = model.GetBooks(tableModel);
            return Json(data);
        }
    }
}