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

        [HttpPost("Add")]
        public async Task<ActionResult<ReturnBookDTO>> AddBook(AddBookDTO addbookDTO)
        {
            var newBook = new Book
            {
                Title = addbookDTO.Title,
                Author = addbookDTO.Author,
                Description = addbookDTO.Description,
            };

            await _context.Books.AddAsync(newBook);

            if (await _context.SaveChangesAsync() < 1) 
            {
                throw new DbUpdateException("Error while adding data to database");
            }

            var returnDTO = new ReturnBookDTO
            {
                BookId = newBook.BookId,
                Author = newBook.Author,
                Title = newBook.Title,
                Description = newBook.Description
            };

            return returnDTO;
        }
    }
}
