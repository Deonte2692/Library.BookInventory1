using Library.BookInventory.Models;
using Library.BookInventory.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Library.BookInventory.Controllers
{
    public class BookController : Controller
    {
        private readonly LibraryContext _context;

        public BookController(LibraryContext context)
        {
            _context = context;
        }

        // Display list of all books
        public IActionResult Index()
        {
            var books = _context.Books.ToList();
            return View(books);  // Pass books to Index view
        }

        // Show form to add a new book
        public IActionResult Add()
        {
            // Initialize a new Book object and set default values for required properties
            var newBook = new Book
            {
                Title = string.Empty,  // Set Title to an empty string or some default value
                Author = string.Empty, // Same for Author and Category if needed
                Category = string.Empty
            };

            return View(newBook);  // Pass the newBook to the view
        }


        // Handle form submission for adding a new book
        [HttpPost]
        public IActionResult Add(Book newBook)
        {
            if (ModelState.IsValid)
            {
                _context.Books.Add(newBook);
                _context.SaveChanges();
                return RedirectToAction("Index");  // Redirect back to the Index page after adding
            }
            return View(newBook);  // If model is not valid, show form again
        }


        // Show form to edit an existing book
        public IActionResult Edit(int id)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();  // Return a 404 if the book doesn't exist
            }
            return View(book);  // Pass book data to Edit view
        }

        // Handle form submission for updating a book
        [HttpPost]
        public IActionResult Edit(Book updatedBook)
        {
            if (ModelState.IsValid)
            {
                _context.Books.Update(updatedBook);
                _context.SaveChanges();
                return RedirectToAction("Index");  // Redirect to Index page after updating
            }
            return View(updatedBook);  // If model is not valid, show form again
        }

        // Show confirmation page for deleting a book
        public IActionResult Delete(int id)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();  // Return a 404 if the book doesn't exist
            }
            return View(book);  // Pass book data to Delete view
        }

        // Handle deletion of a book
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");  // Redirect to Index page after deletion
        }

        public IActionResult Search(string searchTerm)
        {
            Console.WriteLine($"Search triggered with term: {searchTerm}");  // Debug log

            if (!string.IsNullOrEmpty(searchTerm))
            {
                var books = _context.Books
                    .Where(b => b.Title.Contains(searchTerm) || b.Author.Contains(searchTerm) || b.Category.Contains(searchTerm))
                    .ToList();

                Console.WriteLine($"Books found: {books.Count}");  // Debug log

                ViewData["SearchTerm"] = searchTerm;  // Set the search term to retain it in the view

                return View(books);
            }
            else
            {
                ViewData["SearchTerm"] = "";  // Clear search term if no search was made
                return View(new List<Book>());
            }
        }

    }
}
