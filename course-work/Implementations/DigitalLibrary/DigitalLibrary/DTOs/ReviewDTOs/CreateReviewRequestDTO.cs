using System.ComponentModel.DataAnnotations;

namespace DigitalLibrary.DTOs.ReviewDTOs
{
    public class CreateReviewRequestDTO
    {
        [Required]
        [Range(1, 5, ErrorMessage = "The rating can be from 1-5")]
        public int Rating { get; set; }

        [Required]
        [MaxLength(2000, ErrorMessage = "Review too long")]
        public string ReviewText { get; set; }

        [Required]
        public DateTime ReviewDate { get; set; }
    }
}
