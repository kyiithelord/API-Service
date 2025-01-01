using System.Data.Entity;

namespace BookServiceAPI.Models
{
    public class BookDbContext : DbContext
    {
        public BookDbContext() : base("BookDbContext") { }

        public DbSet<Book> Books { get; set; }
    }
}
