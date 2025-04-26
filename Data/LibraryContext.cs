using Microsoft.EntityFrameworkCore;
using Library.BookInventory.Models;

namespace Library.BookInventory.Data
{
    public class LibraryContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base(options)
        {
        }

        // public LibraryContext()
        // {
        // }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     if (!optionsBuilder.IsConfigured)
        //     {
        //         optionsBuilder.UseMySql(
        //             "server=localhost;database=librarydb;user=root;password=Deojer2004!;",
        //             ServerVersion.AutoDetect("server=localhost;database=librarydb;user=root;password=Deojer2004!;")
        //         );
        //     }
        // }
    }
}
