using Autofac;
using Blog.Framework.BookBLOG;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web.Areas.Admin.Models.BookModel
{
    public class CreateBookModel : BookBaseModel
    {
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Title { get; set; }
        public string ImageName { get; set; }
        public IFormFile imageFile { get; set; }
        public string Description { get; set; }
        public string Edition { get; set; }

        public CreateBookModel(IBookService bookService) : base(bookService) { }
        public CreateBookModel() : base() { }

        public void Create()
        {
            var hostingEnvironment = Startup.AutofacContainer.Resolve<IWebHostEnvironment>();

            string wwwRootpath = hostingEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(imageFile.FileName);
            string extension = Path.GetExtension(imageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            string path = Path.Combine(wwwRootpath + "/about/", fileName);

            var stream = new FileStream(path, FileMode.Create);
            imageFile.CopyToAsync(stream);
            var book = new Book
            {
                Title = this.Title,
                ImageName= fileName,
                Description = this.Description,
                Edition = this.Edition,
                PublicationDate = DateTime.Now
            };

            _bookService.CreateBook(book);
        }
    }
}

