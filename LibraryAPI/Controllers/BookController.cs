using LibraryAPI.Data;
using LibraryAPI.DTOs;
using LibraryAPI.Entities;
using LibraryAPI.Interfaces;
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
        public readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        //[Authorize]
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<ReturnBookDTO>>> GetAll()
        {
            var books = await _bookService.GetAllAsync();
            return Ok(books);
        }

        [HttpPost("Add")]
        public async Task<ActionResult<ReturnBookDTO>> AddBook(AddBookDTO addbookDTO)
        {
            var book = await _bookService.AddBookAsync(addbookDTO);
            return Ok(book);
        }

        [HttpPut("Edit")]
        public async Task<ActionResult<ReturnBookDTO>> EditBook(EditBookDTO editBook) 
        {
            var book = await _bookService.EditBookAsync(editBook);
            return Ok(book);
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<ReturnBookDTO>> DeleteBook(int bookId) 
        {
            var book = await _bookService.DeleteBookAsync(bookId);
            return Ok(book);
        }
    }
}
