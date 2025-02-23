using BookManagmentApi.Models;
using BookManagmentApi.Repos.Interfaces;
using BookManagmentApi.Services.Interfaces;

namespace BookManagmentApi.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepo _bookRepo;

        public BookService(IBookRepo bookRepo)
        {
            _bookRepo = bookRepo;
        }

        public Task<Book> GetBook(Guid id)
        {
            return _bookRepo.GetBook(id);
        }

        public Task<IEnumerable<Book>> GetBooks()
        {
            return _bookRepo.GetBooks();
        }

        public Task InsertBook(Book book)
        {
            return _bookRepo.InsertBook(book);
        }

        public Task InsertMultipleBooks(List<Book> books)
        {
            return _bookRepo.InsertMultipleBooks(books);
        }

        public Task UpdateBook(Book book)
        {
            return _bookRepo.UpdateBook(book);
        }

        public Task DeleteBook(Book book)
        {
            return _bookRepo.DeleteBook(book);
        }
    }
}
