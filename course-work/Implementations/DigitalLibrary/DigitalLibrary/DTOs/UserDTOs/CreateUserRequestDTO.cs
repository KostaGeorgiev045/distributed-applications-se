using System.ComponentModel.DataAnnotations;

namespace DigitalLibrary.DTOs.UserDTOs
{
    public class CreateUserRequestDTO
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Username is too long")]
        public string Username { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Email is too long")]
        public string Email { get; set; }

        [Required]
        [MaxLength(255, ErrorMessage = "Password is too long")]
        public string Password { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }
    }
}
