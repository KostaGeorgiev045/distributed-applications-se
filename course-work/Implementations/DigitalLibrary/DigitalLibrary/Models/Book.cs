using System.ComponentModel.DataAnnotations;

namespace DigitalLibrary.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Author { get; set; } =string.Empty;

        public DateTime PublicationDate { get; set; } = DateTime.UtcNow;

        public string ISBN { get; set; } = string.Empty;

        public string Genre { get; set; } = string.Empty;

        public decimal Rating { get; set; }

        // Navigation property for the related reviews
        public List<Review> Reviews { get; set; } = new List<Review>();
    }
}
