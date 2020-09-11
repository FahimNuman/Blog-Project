using Autofac;
using Blog.Framework;
using Blog.Framework.BookBLOG;
using Blog.Web.Areas.Admin.Models.BookModel;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web.Areas.Admin.Models.BookModel
{
    public class BookModel : BookBaseModel
    {
        public BookModel(IBookService bookService) : base(bookService) { }
        public BookModel() : base() { }

        internal object GetBooks(DataTablesAjaxRequestModel tableModel)
        {
            var data = _bookService.GetBooks(
                tableModel.PageIndex,
                tableModel.PageSize,
                tableModel.SearchText,
                tableModel.GetSortText(new string[] { "Title","ImageName" ,"Description", "Edition", "PublicationDate","Id" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                record.Title,
                                record.ImageName,
                                //record.imageFile.ToString(),
                                record.Description,
                                record.Edition,
                                record.PublicationDate.ToString(),
                                record.Id.ToString()
                        }
                    ).ToArray()

            };
        }

        internal string Delete(int id)
        {
            var deletedBook = _bookService.DeleteBook(id);
            return deletedBook.Title;
        }
    }
}

