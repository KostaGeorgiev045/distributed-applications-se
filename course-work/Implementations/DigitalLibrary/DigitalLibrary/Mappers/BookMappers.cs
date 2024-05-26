using DigitalLibrary.DTOs.BookDTOs;
using DigitalLibrary.Models;

namespace DigitalLibrary.Mappers
{
    public static class BookMappers
    {
        public static BookDTO ToBookDTO(this Book bookModel)
        {
            return new BookDTO
            {
                BookId = bookModel.BookId,
                Title = bookModel.Title,
                Author = bookModel.Author,
                PublicationDate = bookModel.PublicationDate,
                ISBN = bookModel.ISBN,
                Genre = bookModel.Genre,
                Rating = bookModel.Rating,
                Reviews = bookModel.Reviews.Select(c => c.ToReviewDTO()).ToList()
            };
        }

        public static Book ToBookFromCreateDTO(this CreateBookRequestDTO bookDto)
        {
            return new Book
            {
                Title = bookDto.Title,
                Author = bookDto.Author,
                Genre = bookDto.Genre,
                PublicationDate = bookDto.PublishedDate,
                ISBN = bookDto.ISBN
            };
        }

        
    }
}
