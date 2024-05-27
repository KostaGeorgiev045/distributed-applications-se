using DigitalLibrary.DTOs.UserDTOs;
using DigitalLibrary.Helpers;
using DigitalLibrary.Models;

namespace DigitalLibrary.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync(QueryObject query);
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByUsernameAsync(string username);
        Task<User> CreateAsync(User userModel);
        Task<User?> UpdateAsync(int id, UpdateUserRequestDTO userModel);
        Task<User?> DeleteAsync(int id);
        Task<User?> DeleteByUsernameAsync(string username);
        Task<bool> UserExists(int id);
    }
}
