using Library.BookInventory.Models;
using Library.BookInventory.Controllers;
using System;

namespace Library.BookInventory
{
    class Program
    {
        static void Main(string[] args)
        {
            var controller = new BookController();
            bool running = true;

            Console.WriteLine("=== Welcome to the Book Inventory System ===");

            while (running)
            {
                Console.WriteLine("\nChoose an option:");
                Console.WriteLine("1. Add Book");
                Console.WriteLine("2. Delete Book");
                Console.WriteLine("3. View All Books");
                Console.WriteLine("4. Edit Book");
                Console.WriteLine("5. Search Books");
                Console.WriteLine("6. Sort Books");
                Console.WriteLine("7. Exit");

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.Write("Enter Title: ");
                        var title = (Console.ReadLine() ?? "").Trim();
                        Console.Write("Enter Author: ");
                        var author = (Console.ReadLine() ?? "").Trim();
                        Console.Write("Enter Category: ");
                        var category = (Console.ReadLine() ?? "").Trim();

                        var newBook = new Book { Title = title, Author = author, Category = category };

                        var addResult = controller.AddBook(newBook);
                        Console.WriteLine(addResult);
                        break;

                    case "2":
                        Console.Write("Enter Book ID to delete: ");
                        if (int.TryParse(Console.ReadLine(), out int deleteId))
                        {
                            var deleteResult = controller.DeleteBook(deleteId);
                            Console.WriteLine(deleteResult);
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter a valid number.");
                        }
                        break;

                    case "3":
                        var books = controller.SortBooksByTitle();
                        Console.WriteLine("\n--- Book List ---");
                        foreach (var book in books)
                        {
                            Console.WriteLine($"ID: {book.Id}, Title: {book.Title}, Author: {book.Author}, Category: {book.Category}");
                        }
                        break;

                    case "4":
                        Console.Write("Enter Book ID to edit: ");
                        if (int.TryParse(Console.ReadLine(), out int editId))
                        {
                            Console.Write("Enter new Title (or leave blank to keep current): ");
                            var newTitle = Console.ReadLine();
                            Console.Write("Enter new Author (or leave blank to keep current): ");
                            var newAuthor = Console.ReadLine();
                            Console.Write("Enter new Category (or leave blank to keep current): ");
                            var newCategory = Console.ReadLine();

                            var editResult = controller.EditBook(editId, newTitle, newAuthor, newCategory);
                            Console.WriteLine(editResult);
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter a valid number.");
                        }
                        break;

                    case "5":
                        Console.Write("Enter a search term (title, author, or category): ");
                        var searchTerm = Console.ReadLine();
                        var results = controller.SearchBooks(searchTerm);
                        Console.WriteLine("\n--- Search Results ---");
                        foreach (var book in results)
                        {
                            Console.WriteLine($"ID: {book.Id}, Title: {book.Title}, Author: {book.Author}, Category: {book.Category}");
                        }
                        break;

                    case "6":
                        var sortedBooks = controller.SortBooksByTitle();
                        Console.WriteLine("\n--- Sorted Book List ---");
                        foreach (var book in sortedBooks)
                        {
                            Console.WriteLine($"ID: {book.Id}, Title: {book.Title}, Author: {book.Author}, Category: {book.Category}");
                        }
                        break;

                    case "7":
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }

            Console.WriteLine("Exiting the system. Goodbye!");
        }
    }
}
