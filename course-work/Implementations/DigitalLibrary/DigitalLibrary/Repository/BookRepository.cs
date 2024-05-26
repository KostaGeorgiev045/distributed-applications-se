using DigitalLibrary.Data;
using DigitalLibrary.DTOs.BookDTOs;
using DigitalLibrary.Helpers;
using DigitalLibrary.Interfaces;
using DigitalLibrary.Models;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace DigitalLibrary.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetAllAsync(QueryObject query)
        {
            var books = _context.Books.Include(c => c.Reviews).AsQueryable();

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await books.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _context.Books.Include(c => c.Reviews).FirstOrDefaultAsync(i => i.BookId == id);
        }

        public async Task<Book?> CreateAsync(Book bookModel)
        {
            await _context.Books.AddAsync(bookModel);
            await _context.SaveChangesAsync();
            return bookModel;
        }

        public async Task<Book?> DeleteAsync(int id)
        {
            var bookModel = await _context.Books.FirstOrDefaultAsync(x => x.BookId == id);

            if(bookModel == null)
            {
                return null;
            }

            _context.Books.Remove(bookModel);
            await _context.SaveChangesAsync();
            return bookModel;
        }

        public async Task<Book?> UpdateAsync(int id, UpdateBookRequestDTO bookUpdateDTO)
        {
            var existingBook = await _context.Books.FirstOrDefaultAsync(x => x.BookId == id);

            if(existingBook == null)
            {
                return null;
            }

            existingBook.Title = bookUpdateDTO.Title;
            existingBook.Author = bookUpdateDTO.Author;
            existingBook.PublicationDate = bookUpdateDTO.PublicationDate;
            existingBook.ISBN = bookUpdateDTO.ISBN;
            existingBook.Genre = bookUpdateDTO.Genre;

            await _context.SaveChangesAsync();

            return existingBook;
        }

        public Task<bool> BookExists(int id)
        {
            return _context.Books.AnyAsync(s => s.BookId == id);
        }

        public async Task<Book?> GetByTitleAsync(string title)
        {
            return await _context.Books.Include(c => c.Reviews).FirstOrDefaultAsync(i => i.Title == title);
        }
    }
}
