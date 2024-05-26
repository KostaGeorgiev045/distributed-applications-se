using DigitalLibrary.DTOs.ReviewDTOs;
using DigitalLibrary.Models;
using System.Runtime.CompilerServices;

namespace DigitalLibrary.Mappers
{
    public static class ReviewMappers
    {
        public static ReviewDTO ToReviewDTO(this Review reviewModel)
        {
            return new ReviewDTO
            {
                ReviewId = reviewModel.ReviewId,
                BookId = reviewModel.BookId,
                UserId = reviewModel.UserId,
                Rating = reviewModel.Rating,
                ReviewText = reviewModel.ReviewText,
                ReviewDate = reviewModel.ReviewDate,
            };
        }

        public static Review ToReviewFromCreateDTO(this CreateReviewRequestDTO reviewDTO, 
            int userId, int bookId)
        {
            return new Review
            {
                Rating = reviewDTO.Rating,
                ReviewText = reviewDTO.ReviewText,
                ReviewDate = reviewDTO.ReviewDate,
                UserId = userId,
                BookId = bookId,
            };
        }

        public static Review ToReviewFromUpdateDTO(this UpdateReviewRequestDTO reviewDTO)
        {
            return new Review
            {
                Rating = reviewDTO.Rating,
                ReviewText = reviewDTO.ReviewText,
                ReviewDate = reviewDTO.ReviewDate,
            };
        }
    }
}
