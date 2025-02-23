using BookManagmentApi.DTO;
using BookManagmentApi.Models;
using BookManagmentApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookManagmentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController(IBookService bookService) : ControllerBase
    {
        private readonly IBookService _bookService = bookService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetAllBooksTitle()
        {
            var books = await _bookService.GetBooks();
            return Ok(books.Select(book => book.Title));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBook(Guid id)
        {
            var book = await _bookService.GetBook(id);
            if (book == null)
                return NotFound();

            var dto = new BookDto()
            {
                Title = book.Title,
                AuthorName = book.AuthorName,
                ViewCount = book.ViewCount
            };
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> AddSingleBook([FromBody] BookDto dto)
        {
            if (dto is null || string.IsNullOrWhiteSpace(dto.Title) || string.IsNullOrWhiteSpace(dto.AuthorName))
                return BadRequest("Invalid book data.");

            var books = await _bookService.GetBooks();
            foreach(var book in books)
            {
                if(book.Title.Equals(dto.Title.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    return BadRequest($"{dto.Title} is already exist!");
                }
            }
            var newBook = new Book()
            {
                AuthorName = dto.AuthorName.Trim(),
                Title = dto.Title.Trim(),
            };

            await _bookService.InsertBook(newBook);
            return CreatedAtAction(nameof(GetBook), new { id = newBook.Id }, newBook);
        }

        [HttpPost("bulk-insert")]
        public async Task<IActionResult> AddMultipleBooks([FromBody] List<BookDto> booksDto)
        {
            if (booksDto.Count == 0)
                return BadRequest("Book list cannot be empty.");

            var books = await _bookService.GetBooks();
            foreach (var bookDto in booksDto)
            {
                foreach (var book in books)
                {
                    if (book.Title.Equals(bookDto.Title.Trim(), StringComparison.OrdinalIgnoreCase))
                    {
                        return BadRequest($"{bookDto.Title} is already exist!");
                    }
                }
            }
            var newBooks = booksDto.ConvertAll(dto => new Book()
            {
                Title = dto.Title,
                AuthorName = dto.AuthorName,
            });

            await _bookService.InsertMultipleBooks(newBooks);
            return Created();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSingleBook(Guid id, [FromBody] BookDto dto)
        {
            var book = await _bookService.GetBook(id);
            if(book == null)
            {
                return NotFound();
            }

            if(!string.IsNullOrEmpty(dto.AuthorName.Trim())) {
                book.Title = dto.Title;
            }
            if (!string.IsNullOrEmpty(dto.Title.Trim())) {
                book.Title = dto.Title;
            }

            try
            {
                await _bookService.UpdateBook(book);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while updating the book.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSingleBook(Guid id)
        {
            var book = await _bookService.GetBook(id);
            if(book is null)
            {
                return NotFound("Id is invalid!");
            }
            await _bookService.DeleteBook(book);
            return NoContent();
        }

        [HttpDelete("bulk-delete")]
        public async Task<IActionResult> DeleteMultipleBooks(List<Guid> bookIds)
        {
            if (bookIds.Count != 0)
                return BadRequest("Invalid book IDs.");

            var books = new List<Book>();
            foreach (var id in bookIds)
            {
                var book = await _bookService.GetBook(id);
                if (book is not null)
                    books.Add(book);
            }

            if (books.Count == 0)
            {
                return NotFound("No valid books found for deletion.");
            }
            else
            {
                foreach (var book in books)
                    await _bookService.DeleteBook(book);
            }

            return NoContent();
        }
    }
}
