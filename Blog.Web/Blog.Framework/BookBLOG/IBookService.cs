using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Framework.BookBLOG
{
    public interface IBookService : IDisposable
    {
        (IList<Book> records, int total, int totalDisplay) GetBooks(int pageIndex,
                                                                    int pageSize,
                                                                    string searchText,
                                                                    string sortText);
        void CreateBook(Book book);
        void EditBook(Book book);
        Book GetBook(int id);
        Book DeleteBook(int id);
    }
}
