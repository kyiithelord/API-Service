using System.Linq;
using System.Net;
using System.Web.Http;
using BookServiceAPI.Models;

namespace BookServiceAPI.Controllers
{
    public class BooksController : ApiController
    {
        private readonly BookDbContext _context = new BookDbContext();

        [HttpGet]
        public IHttpActionResult GetBooks()
        {
            return Ok(_context.Books.ToList());
        }

        [HttpGet]
        public IHttpActionResult GetBook(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        [HttpPost]
        public IHttpActionResult AddBook([FromBody] Book newBook)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _context.Books.Add(newBook);
            _context.SaveChanges();
            return Created($"api/books/{newBook.Id}", newBook);
        }

        [HttpPut]
        public IHttpActionResult UpdateBook(int id, [FromBody] Book updatedBook)
        {
            var book = _context.Books.Find(id);
            if (book == null) return NotFound();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            _context.SaveChanges();
            return Ok(book);
        }

        [HttpDelete]
        public IHttpActionResult DeleteBook(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null) return NotFound();

            _context.Books.Remove(book);
            _context.SaveChanges();
            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) _context.Dispose();
            base.Dispose(disposing);
        }
    }
}
