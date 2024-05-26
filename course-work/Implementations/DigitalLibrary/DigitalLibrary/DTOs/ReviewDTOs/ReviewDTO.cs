using DigitalLibrary.DTOs.BookDTOs;
using DigitalLibrary.DTOs.UserDTOs;
using System.ComponentModel.DataAnnotations;

namespace DigitalLibrary.DTOs.ReviewDTOs
{
    public class ReviewDTO
    {
        public int ReviewId { get; set; }

        public int BookId { get; set; }

        //public BookDTO Book { get; set; }

        public int UserId { get; set; }

        //public UserDTO User { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "The rating can be from 1-5")]
        public int Rating { get; set; }

        [Required]
        [MaxLength(2000, ErrorMessage = "Review too long")]
        public string ReviewText { get; set; }

        public DateTime ReviewDate { get; set; }
    }
}
