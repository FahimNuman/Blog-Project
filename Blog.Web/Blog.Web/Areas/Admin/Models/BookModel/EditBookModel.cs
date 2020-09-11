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
    public class EditBookModel : BookBaseModel
    {
        public int Id { get; set; }
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Title { get; set; }
        public string ImageName { get; set; }
        public IFormFile imageFile { get; set; }
        public string Description { get; set; }
        public string Edition { get; set; }

        public EditBookModel(IBookService bookService) : base(bookService) { }
        public EditBookModel() : base() { }

        public void Edit()
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
                Id = this.Id,
                Title = this.Title,
                ImageName = fileName,
                Description = this.Description,
                Edition = this.Edition,
                PublicationDate = DateTime.Now
            };

            _bookService.EditBook(book);
        }

        internal void Load(int id)
        {
            var book = _bookService.GetBook(id);
            if (book != null)
            {
                Id = book.Id;
                Title = book.Title;
                ImageName =book.ImageName;
                Description = book.Description;
                Edition = book.Edition;
            }
        }
    }
}
