using BookManagmentApi.Models;

namespace BookManagmentApi.Repos.Interfaces
{
    public interface IBookRepo
    {
        Task<Book> GetBook(Guid id);
        Task<IEnumerable<Book>> GetBooks();
        Task InsertBook(Book book);
        Task InsertMultipleBooks(List<Book> books);
        Task UpdateBook(Book book);
        Task DeleteBook(Book book);
    }
}
