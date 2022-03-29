using LibraryAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<ReturnBookDTO>> GetAllAsync();
        Task<ReturnBookDTO> AddBookAsync(AddBookDTO addbookDTO);
        Task<ReturnBookDTO> EditBookAsync(EditBookDTO editBook);
        Task<ReturnBookDTO> DeleteBookAsync(int bookId);

    }
}
