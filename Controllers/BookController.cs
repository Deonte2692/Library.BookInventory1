using Library.BookInventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.BookInventory.Controllers
{
    public class BookController
    {
        private static List<Book> _books = new List<Book>();
        private static int _nextId = 1;

        // Add Book: Validate inputs & add to inventory
        public string AddBook(Book book)
        {
            if (string.IsNullOrWhiteSpace(book.Title) || string.IsNullOrWhiteSpace(book.Author))
            {
                return "Error: Title and Author are required.";
            }

            // Check for duplicate (same title + author)
            if (_books.Any(b => b.Title == book.Title && b.Author == book.Author))
            {
                return "Error: Book already exists.";
            }

            book.Id = _nextId++;
            _books.Add(book);
            return "Book added successfully.";
        }

        // Edit Book Details: Update title, author, or category
        public string EditBook(int id, string newTitle, string newAuthor, string newCategory)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return "Error: Book not found.";
            }

            if (!string.IsNullOrWhiteSpace(newTitle))
                book.Title = newTitle;

            if (!string.IsNullOrWhiteSpace(newAuthor))
                book.Author = newAuthor;

            if (!string.IsNullOrWhiteSpace(newCategory))
                book.Category = newCategory;

            return "Book updated successfully.";
        }

        // Delete Book: Remove outdated or lost books
        public string DeleteBook(int id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return "Error: Book not found.";
            }

            _books.Remove(book);
            return "Book deleted successfully.";
        }

        // Search Books: Filter by title, author, or category
        public List<Book> SearchBooks(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return new List<Book>();

            searchTerm = searchTerm.ToLower();

            return _books
                .Where(b => b.Title.ToLower().Contains(searchTerm) ||
                            b.Author.ToLower().Contains(searchTerm) ||
                            b.Category.ToLower().Contains(searchTerm))
                .ToList();
        }

        // Sort Books: Sort books alphabetically by title
        public List<Book> SortBooksByTitle()
        {
            return _books.OrderBy(b => b.Title).ToList();
        }
    }
}
