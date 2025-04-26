using Library.BookInventory.Models;
using Library.BookInventory.Data;

namespace Library.BookInventory.Controllers
{
    public class BookController
    {
        private readonly BookRepository _bookRepository;

        public BookController()
        {
            _bookRepository = new BookRepository();
        }

        public string AddBook(Book book)
        {
            if (_bookRepository.Exists(book.Title, book.Author))
            {
                return "Error: Book already exists.";
            }

            _bookRepository.Add(book);
            return "Book added successfully.";
        }

        public string DeleteBook(int id)
        {
            var book = _bookRepository.GetById(id);
            if (book == null)
            {
                return "Error: Book not found.";
            }

            _bookRepository.Delete(id);
            return "Book deleted successfully.";
        }

        public string UpdateBook(int id, Book updatedBook)
        {
            var existingBook = _bookRepository.GetById(id);
            if (existingBook == null)
            {
                return "Error: Book not found.";
            }

            existingBook.Title = updatedBook.Title;
            existingBook.Author = updatedBook.Author;
            existingBook.Category = updatedBook.Category;

            _bookRepository.Update(existingBook);
            return "Book updated successfully.";
        }

        public string SearchBooks(string searchTerm)
        {
            var result = _bookRepository.SearchBooks(searchTerm).ToList();
            return result.Any() ? string.Join("\n", result.Select(b => $"{b.Title} by {b.Author} ({b.Category})")) : "No books found.";
        }

        public string SortBooksByTitle()
        {
            var result = _bookRepository.SortBooksByTitle().ToList();
            return result.Any() ? string.Join("\n", result.Select(b => $"{b.Title} by {b.Author} ({b.Category})")) : "No books found.";
        }
    }
}
