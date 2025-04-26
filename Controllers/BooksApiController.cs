using Library.BookInventory.Data;
using Library.BookInventory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.BookInventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksApiController : ControllerBase
    {
        private readonly LibraryContext _context;

        public BooksApiController(LibraryContext context)
        {
            _context = context;
        }

        public BooksApiController(BookController @object)
        {
        }

        // POST api/books
        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] Book book)
        {
            if (book == null)
            {
                return BadRequest("Book is null");
            }

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        // DELETE api/books/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound("Book not found");
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent(); // 204 status code - successful delete with no content
        }

        // PUT api/books/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] Book book)
        {
            if (book == null || id != book.Id)
            {
                return BadRequest("Book data is invalid");
            }

            var existingBook = await _context.Books.FindAsync(id);
            if (existingBook == null)
            {
                return NotFound("Book not found");
            }

            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.Category = book.Category;

            _context.Books.Update(existingBook);
            await _context.SaveChangesAsync();

            return NoContent(); // 204 status code - successful update with no content
        }

        // GET api/books
        [HttpGet]
        public async Task<IActionResult> GetBooks([FromQuery] string searchTerm)
        {
            var booksQuery = _context.Books.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                booksQuery = booksQuery.Where(b => b.Title.Contains(searchTerm) || b.Author.Contains(searchTerm) || b.Category.Contains(searchTerm));
            }

            var books = await booksQuery.ToListAsync();
            return Ok(books);
        }

        // GET api/books/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound("Book not found");
            }
            return Ok(book);
        }

        // GET api/books/sort
        [HttpGet("sort")]
        public async Task<IActionResult> GetSortedBooks()
        {
            var books = await _context.Books.OrderBy(b => b.Title).ToListAsync();
            return Ok(books);
        }
    }
}
