using LibraryAPI.Data;
using LibraryAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly AppDbContext _context;


        public BookController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("GetAll")]
        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await _context.Books.ToListAsync();
        }

        [HttpPost()]
        public async Task<ActionResult<Book>> AddBook(Book book)
        {

            _context.Books.Add(book);

            _context.SaveChanges();

            return await _context.FindAsync<Book>(book);
        }
    }
}
