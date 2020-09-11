using Blog.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.Framework.BookBLOG
{
    public class BookRepository : Repository<Book, int, FrameworkContext>, IBookRepository
    {
        public BookRepository(FrameworkContext dbContext)
            : base(dbContext)
        {

        }

        public IList<Book> GetLatestBooks()
        {
            return Get(x => x.PublicationDate < DateTime.Now.AddDays(-7)).ToList();
        }
    }
}