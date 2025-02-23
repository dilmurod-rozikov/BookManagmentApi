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
		var book = await _appDbContext.Books.FindAsync(id);
		if(book is not null)
		{
			book.ViewCount++;
		}

		return book;
	}

	public async Task<IEnumerable<Book>> GetBooks()
	{
		var books = await _appDbContext.Books.ToListAsync();
		foreach (var book in books)
		{
			if (book is not null)
			{
				book.ViewCount++;
			}
		}

        return books;
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
