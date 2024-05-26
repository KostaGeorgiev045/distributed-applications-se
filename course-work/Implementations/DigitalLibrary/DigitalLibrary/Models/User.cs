using System.ComponentModel.DataAnnotations;

namespace DigitalLibrary.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string Username { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;

        // Navigation property for the related reviews
        public List<Review> Reviews { get; set; } = new List<Review>();
    }
}
