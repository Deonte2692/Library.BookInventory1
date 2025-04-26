using Microsoft.EntityFrameworkCore;
using Moq;
using System.Threading.Tasks;
using Library.BookInventory.Models;
using Library.BookInventory.Data;
using Library.BookInventory.Controllers;
using Xunit;
using Microsoft.AspNetCore.Mvc;

namespace Library.BookInventory.Tests
{
    public class BooksApiControllerTests
    {
        private readonly BooksApiController _controller;
        private readonly Mock<LibraryContext> _mockContext;
        private readonly Mock<DbSet<Book>> _mockDbSet;
        private readonly Mock<BookController> _mockBookController;

        public BooksApiControllerTests()
        {
            // Mocking the DbSet<Book>
            _mockDbSet = new Mock<DbSet<Book>>();
            _mockContext = new Mock<LibraryContext>();

            // Setting up the DbSet mock to return our mocked DbSet
            _mockContext.Setup(m => m.Books).Returns(_mockDbSet.Object);

            // Creating a mock for BookController which now depends on LibraryContext
            _mockBookController = new Mock<BookController>(_mockContext.Object);

            // Passing the mock BookController to BooksApiController
            _controller = new BooksApiController(_mockBookController.Object);
        }

        [Fact]
        public async Task AddBook_ShouldReturnCreatedAtAction_WhenBookIsValid()
        {
            // Arrange
            var newBook = new Book { Id = 1, Title = "Test Book", Author = "Test Author", Category = "Fiction" };

            // Mocking AddAsync to return a ValueTask with a successful EntityEntry
            _mockDbSet.Setup(m => m.AddAsync(It.IsAny<Book>(), default))
                .ReturnsAsync(new Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Book>(null));

            // Mocking SaveChangesAsync to return 1, indicating a successful save
            _mockContext.Setup(m => m.SaveChangesAsync(default)).ReturnsAsync(1);

            // Act
            var result = await _controller.AddBook(newBook);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(201, createdAtActionResult.StatusCode);
        }
    }
}
