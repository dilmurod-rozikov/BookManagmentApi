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
        public async Task<ActionResult<IEnumerable<string>>>
            GetAllBooksTitle([FromQuery] int paginationNo = 25)
        {
            var books = await _bookService.GetBooks();
            books = books.Skip(paginationNo).OrderByDescending(book => book.ViewCount);
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
                Id = book.Id,
                Title = book.Title,
                AuthorName = book.AuthorName,
                ViewCount = book.ViewCount
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> AddSingleBook([FromBody] CreateBookDto dto)
        {
            if (dto is null || string.IsNullOrWhiteSpace(dto.Title) || string.IsNullOrWhiteSpace(dto.AuthorName))
                return BadRequest("Invalid book data.");

            var books = await _bookService.GetBooks();
            var newBook = new Book()
            {
                AuthorName = dto.AuthorName.Trim(),
                Title = dto.Title.Trim(),
            };

            try
            {
                await _bookService.InsertBook(newBook);
                return CreatedAtAction(nameof(GetBook), new { id = newBook.Id });
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while updating the book.");
            }
        }

        [HttpPost("bulk-insert")]
        public async Task<IActionResult> AddMultipleBooks([FromBody] List<CreateBookDto> booksDto)
        {
            if (booksDto.Count == 0)
                return BadRequest("Book list cannot be empty.");

            var books = await _bookService.GetBooks();
            var newBooks = booksDto.ConvertAll(dto => new Book()
            {
                Title = dto.Title,
                AuthorName = dto.AuthorName,
            });
            try
            {
                await _bookService.InsertMultipleBooks(newBooks);
                return Created();
            }
            catch (Exception)
            {
                return StatusCode(500, "There may be duplicate books!!");
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateSingleBook(Guid id, [FromBody] CreateBookDto dto)
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

            try
            {
                await _bookService.DeleteBook(book);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while updating the book.");
            }
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

            foreach (var book in books)
            {
                try
                {
                    await _bookService.DeleteBook(book);
                }
                catch (Exception)
                {
                    return StatusCode(500, "An error occurred while updating the book.");
                }
            }
          
            return NoContent();
        }
    }
}
