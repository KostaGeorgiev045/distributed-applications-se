using System.ComponentModel.DataAnnotations;

namespace DigitalLibrary.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int Rating { get; set; }

        public string ReviewText { get; set; } = string.Empty;

        public DateTime ReviewDate { get; set; } = DateTime.UtcNow;      
    }
}
