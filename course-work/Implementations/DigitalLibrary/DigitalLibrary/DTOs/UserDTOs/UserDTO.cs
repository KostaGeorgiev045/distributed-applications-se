using DigitalLibrary.DTOs.ReviewDTOs;

namespace DigitalLibrary.DTOs.UserDTOs
{
    public class UserDTO
    {
        public int UserId { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public bool IsActive { get; set; }

        public List<ReviewDTO> Reviews { get; set; }
    }
}
