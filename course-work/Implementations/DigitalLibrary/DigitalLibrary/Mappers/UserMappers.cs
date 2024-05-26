using DigitalLibrary.DTOs.UserDTOs;
using System.Runtime.CompilerServices;
using DigitalLibrary.Models;

namespace DigitalLibrary.Mappers
{
    public static class UserMappers
    {
        public static UserDTO ToUserDTO(this User userModel)
        {
            return new UserDTO
            {
                UserId = userModel.UserId,
                Username = userModel.Username,
                Email = userModel.Email,
                DateOfBirth = userModel.DateOfBirth,
                IsActive = userModel.IsActive,
                Reviews = userModel.Reviews.Select(c => c.ToReviewDTO()).ToList()
            };
        }

        public static User ToUserFromCreateDTO(this CreateUserRequestDTO userDTO)
        {
            return new User
            {
                Username = userDTO.Username,
                Email = userDTO.Email,
                Password = userDTO.Password,
                DateOfBirth = userDTO.DateOfBirth
            };
        }
    }
}
