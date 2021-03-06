using LibraryAPI.Data;
using LibraryAPI.DTOs;
using LibraryAPI.Entities;
using LibraryAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.Services
{
    public class BookService : IBookService
    {
        private readonly AppDbContext _context;
        public BookService(AppDbContext context) 
        {
            _context = context;
        }
        public async Task<ReturnBookDTO> AddBookAsync(AddBookDTO addbookDTO)
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

        public async Task<ReturnBookDTO> DeleteBookAsync(int bookId)
        {
            var book = await _context.Books.SingleOrDefaultAsync(b => b.BookId == bookId);

            _context.Books.Remove(book);

            if (await _context.SaveChangesAsync() < 1)
            {
                throw new DbUpdateException("Error while removig data from database");
            }

            var returnDTO = new ReturnBookDTO
            {
                BookId = book.BookId,
                Title = book.Title,
                Author = book.Author,
                Description = book.Description
            };

            return returnDTO;
        }

        public async Task<ReturnBookDTO> EditBookAsync(EditBookDTO editBook)
        {
            var book = await _context.Books.SingleOrDefaultAsync(b => b.BookId == editBook.BookId);

            book.Title = editBook.Title;
            book.Author = editBook.Author;
            book.Description = editBook.Description;

            _context.Books.Update(book);

            if (await _context.SaveChangesAsync() < 1)
            {
                throw new DbUpdateException("Error while editing data in database");
            }

            var returnDTO = new ReturnBookDTO
            {
                BookId = book.BookId,
                Title = book.Title,
                Author = book.Author,
                Description = book.Description
            };

            return returnDTO;
        }

        public async Task<IEnumerable<ReturnBookDTO>> GetAllAsync()
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
    }
}
