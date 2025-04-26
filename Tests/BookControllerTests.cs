using Library.BookInventory.Controllers;
using Library.BookInventory.Models;
using Xunit;

namespace Library.BookInventory.Tests
{
    public class BookControllerTests
    {
        [Fact]
        public void AddBook_ValidData_ReturnsSuccessMessage()
        {
            var controller = new BookController();
            var newBook = new Book
            {
                Title = "1984",
                Author = "George Orwell",
                Category = "Fiction"
            };

            var result = controller.AddBook(newBook);

            Assert.Equal("Book added successfully.", result);
        }

        [Fact]
        public void AddBook_MissingTitle_ReturnsErrorMessage()
        {
            var controller = new BookController();
            var invalidBook = new Book
            {
                Title = "",
                Author = "Unknown Author",
                Category = "Unknown"
            };

            var result = controller.AddBook(invalidBook);

            Assert.Equal("Error: Title and Author are required.", result);
        }

        [Fact]
        public void DeleteBook_BookDoesNotExist_ReturnsErrorMessage()
        {
            var controller = new BookController();

            var result = controller.DeleteBook(999);

            Assert.Equal("Error: Book not found.", result);
        }

        [Fact]
        public void DeleteBook_BookExists_ReturnsSuccessMessage()
        {
            var controller = new BookController();
            var newBook = new Book { Title = "Test Book", Author = "Test Author", Category = "Test" };
            controller.AddBook(newBook);

            var result = controller.DeleteBook(newBook.Id);

            Assert.Equal("Book deleted successfully.", result);
        }
    }
}
