using System;
using Library.BookInventory.Controllers;
using Library.BookInventory.Data;
using Library.BookInventory.Models;

namespace Library.BookInventory
{
    class Program
    {
        static void Main(string[] args)
        {
            var controller = new BookController();
            bool running = true;

            using (LibraryContext context = new LibraryContext())
            {
                context.Database.EnsureCreated();  // Ensure the database is created (for testing purposes)
            }

            Console.WriteLine("=== Welcome to the Book Inventory System ===");

            while (running)
            {
                Console.WriteLine("\nChoose an option:");
                Console.WriteLine("1. Add Book");
                Console.WriteLine("2. Delete Book");
                Console.WriteLine("3. Update Book");
                Console.WriteLine("4. Search Books");
                Console.WriteLine("5. Sort Books");
                Console.WriteLine("6. Exit");

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        AddBookUI(controller);
                        break;
                    case "2":
                        DeleteBookUI(controller);
                        break;
                    case "3":
                        UpdateBookUI(controller);
                        break;
                    case "4":
                        SearchBooksUI(controller);
                        break;
                    case "5":
                        SortBooksUI(controller);
                        break;
                    case "6":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }

            Console.WriteLine("Exiting the system. Goodbye!");
        }

        static void AddBookUI(BookController controller)
        {
            Console.Write("Enter Title: ");
            var title = Console.ReadLine().Trim();

            Console.Write("Enter Author: ");
            var author = Console.ReadLine().Trim();

            Console.Write("Enter Category: ");
            var category = Console.ReadLine().Trim();

            var newBook = new Book { Title = title, Author = author, Category = category };

            var result = controller.AddBook(newBook);
            Console.WriteLine(result);
        }

        static void DeleteBookUI(BookController controller)
        {
            Console.Write("Enter Book ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var result = controller.DeleteBook(id);
                Console.WriteLine(result);
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }

        static void UpdateBookUI(BookController controller)
        {
            Console.Write("Enter Book ID to update: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Console.Write("Enter new Title: ");
                var title = Console.ReadLine().Trim();

                Console.Write("Enter new Author: ");
                var author = Console.ReadLine().Trim();

                Console.Write("Enter new Category: ");
                var category = Console.ReadLine().Trim();

                var updatedBook = new Book
                {
                    Id = id,
                    Title = title,
                    Author = author,
                    Category = category
                };

                var result = controller.UpdateBook(id, updatedBook);
                Console.WriteLine(result);
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }

        static void SearchBooksUI(BookController controller)
        {
            Console.Write("Enter search term (Title, Author, or Category): ");
            var searchTerm = Console.ReadLine().Trim();
            var result = controller.SearchBooks(searchTerm);
            Console.WriteLine(result);
        }

        static void SortBooksUI(BookController controller)
        {
            var result = controller.SortBooksByTitle();
            Console.WriteLine(result);
        }
    }
}
