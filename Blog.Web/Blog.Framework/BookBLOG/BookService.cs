using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Blog.Framework.BookBLOG
{
    public class BookService : IBookService, IDisposable
    {
        private IBlogUnitOfWork _blogUnitOfWork;

        public BookService(IBlogUnitOfWork blogUnitOfWork)
        {
            _blogUnitOfWork = blogUnitOfWork;
        }

        public void CreateBook(Book book)
        {
            var count = _blogUnitOfWork.BookRepository.GetCount(x => x.Title == book.Title);
            if (count > 0)
                throw new DuplicationException("Book title already exists", nameof(book.Title));

            _blogUnitOfWork.BookRepository.Add(book);
            _blogUnitOfWork.Save();
        }

        public Book DeleteBook(int id)
        {
            var book = _blogUnitOfWork.BookRepository.GetById(id);
            _blogUnitOfWork.BookRepository.Remove(book);
            _blogUnitOfWork.Save();
            return book;
        }

        public void Dispose()
        {
            _blogUnitOfWork?.Dispose();
        }

        public void EditBook(Book book)
        {
            var count = _blogUnitOfWork.BookRepository.GetCount(x => x.Title == book.Title
                    && x.Id != book.Id);

            if (count > 0)
                throw new DuplicationException("Book title already exists", nameof(book.Title));

            var existingBook = _blogUnitOfWork.BookRepository.GetById(book.Id);
            existingBook.ImageName = book.ImageName;
            existingBook.Description = book.Description;
            existingBook.Edition = book.Edition;
            existingBook.PublicationDate = book.PublicationDate;
            existingBook.Title = book.Title;

            _blogUnitOfWork.Save();
        }

        public Book GetBook(int id)
        {
            return _blogUnitOfWork.BookRepository.GetById(id);
        }

        public (IList<Book> records, int total, int totalDisplay) GetBooks(int pageIndex, int pageSize, string searchText, string sortText)
        {
            var result = _blogUnitOfWork.BookRepository.GetAll().ToList();
            return (result, 0, 0);
        }
    }
}
