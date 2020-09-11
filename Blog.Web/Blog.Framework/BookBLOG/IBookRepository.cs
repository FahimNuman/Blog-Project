using Blog.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Framework.BookBLOG
{
    public interface IBookRepository : IRepository<Book, int, FrameworkContext>
    {
        IList<Book> GetLatestBooks();
    }
}