using System;
using System.Collections.Generic;
using BoilerPlate.Entities.Model;
using BoilerPlate.Utils;

namespace BoilerPlate.Service.BookService
{
    public interface IBookService
    {
        List<Book> GetAllBooks();
        Book GetBook(Guid id);
    }

    public class BookService : IBookService, IScopedService<IBookService>
    {
        public List<Book> GetAllBooks()
        {
            throw new NotImplementedException();
        }

        public Book GetBook(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}