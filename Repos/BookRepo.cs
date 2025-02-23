using BookManagmentApi.Models;
using BookManagmentApi.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

public class BookRepo : IBookRepo
{
	private readonly AppDbContext _appDbContext;
	public BookRepo(AppDbContext appDbContext)
	{
		_appDbContext = appDbContext;
	}

	public async Task<Book> GetBook(Guid id)
	{
		return await _appDbContext.Books.FindAsync(id);
	}

	public async Task<IEnumerable<Book>> GetBooks()
	{
		return await _appDbContext.Books.AsNoTracking().ToListAsync();
	}

	public async Task InsertBook(Book entity)
	{
		await _appDbContext.Books.AddAsync(entity);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task InsertMultipleBooks(List<Book> books)
    {
        _appDbContext.Books.AddRange(books);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task UpdateBook(Book entity)
	{
		_appDbContext.Books.Update(entity);
		await _appDbContext.SaveChangesAsync();
	}

	public async Task DeleteBook(Book entity)
	{
		entity.IsDeleted = true;
		await _appDbContext.SaveChangesAsync();
	}
}
