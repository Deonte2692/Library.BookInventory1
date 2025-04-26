using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BookInventory.Models
{
        public class Book
        {
            public int Id { get; set; } 
            public required string Title { get; set; }
            public required string Author { get; set; }
            public required string Category { get; set; }
            public bool IsAvailable { get; set; } = true; 
        }
    }

