using Library.BookInventory.Models;
using Library.BookInventory.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Library.BookInventory.Data
{
    public class BookRepository
    {
        private readonly LibraryContext _context;

        public BookRepository(LibraryContext context)
        {
            _context = context;
        }

        public bool Exists(string title, string author)
        {
            return _context.Books.Any(b => b.Title == title && b.Author == author);
        }

        public Book GetById(int id)
        {
            return _context.Books.FirstOrDefault(b => b.Id == id);
        }

        public void Add(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var book = GetById(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
        }

        public void Update(Book book)
        {
            var existingBook = _context.Books.FirstOrDefault(b => b.Id == book.Id);
            if (existingBook != null)
            {
                _context.Entry(existingBook).CurrentValues.SetValues(book);
                _context.SaveChanges();
            }
        }

        public IQueryable<Book> SearchBooks(string searchTerm)
        {
            return _context.Books
                .Where(b => b.Title.Contains(searchTerm) || b.Author.Contains(searchTerm) || b.Category.Contains(searchTerm));
        }

        public IQueryable<Book> SortBooksByTitle()
        {
            return _context.Books.OrderBy(b => b.Title);
        }
    }
}
