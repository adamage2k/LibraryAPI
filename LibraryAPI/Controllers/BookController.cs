using LibraryAPI.Data;
using LibraryAPI.DTOs;
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
        public async Task<IEnumerable<ReturnBookDTO>> GetAllBooks()
        {
            var books = await _context.Books.ToListAsync();
            var booksReturn = new List<ReturnBookDTO>();

            foreach (var book in books)
            {
                var bookReturn = new ReturnBookDTO
                {
                    BookId = book.BookId,
                    Author = book.Author,
                    Title = book.Title,
                    Description = book.Description
                };

                booksReturn.Add(bookReturn);
            }
            return booksReturn;
        }

        [HttpPost()]
        public async Task<ActionResult<Book>> AddBook(AddBookDTO addbookDTO)
        {
            var newBook = new Book();
            newBook.Title = addbookDTO.Title;
            newBook.Author = addbookDTO.Author;
            newBook.Description = addbookDTO.Description;

            _context.Books.Add(newBook);

            _context.SaveChanges();

            return await _context.FindAsync<Book>(newBook);
        }
    }
}
