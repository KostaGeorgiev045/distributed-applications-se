using DigitalLibrary.DTOs.BookDTOs;
using DigitalLibrary.Helpers;
using DigitalLibrary.Models;

namespace DigitalLibrary.Interfaces
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllAsync(QueryObject query);
        Task<Book?> GetByIdAsync(int id);
        Task<Book?> GetByTitleAsync(string title);
        Task<Book> CreateAsync(Book bookModel);
        Task<Book?> UpdateAsync(int id, UpdateBookRequestDTO bookModel);
        Task<Book?> DeleteAsync(int id);
        Task<bool> BookExists(int id);
    }
}
