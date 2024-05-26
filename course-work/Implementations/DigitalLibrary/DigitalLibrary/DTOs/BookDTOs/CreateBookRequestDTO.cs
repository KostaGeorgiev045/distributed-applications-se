using System.ComponentModel.DataAnnotations;

namespace DigitalLibrary.DTOs.BookDTOs
{
    public class CreateBookRequestDTO
    {
        [Required]
        [MaxLength(255, ErrorMessage = "Title is too long")]
        public string Title { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Name is too long")]
        public string Author { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Genre name is too long")]
        public string Genre { get; set; }

        [Required]
        public DateTime PublishedDate { get; set; }

        [Required]
        [MaxLength(13, ErrorMessage = "ISBN too long")]
        public string ISBN { get; set; } 
    }
}
