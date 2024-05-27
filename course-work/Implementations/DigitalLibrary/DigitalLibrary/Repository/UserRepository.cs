using DigitalLibrary.Data;
using DigitalLibrary.DTOs.UserDTOs;
using DigitalLibrary.Helpers;
using DigitalLibrary.Interfaces;
using DigitalLibrary.Models;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

namespace DigitalLibrary.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateAsync(User userModel)
        {
            await _context.Users.AddAsync(userModel);
            await _context.SaveChangesAsync();
            return userModel;
        }

        public async Task<User?> DeleteAsync(int id)
        {
            var userModel = await _context.Users.FirstOrDefaultAsync(i => i.UserId == id);

            if(userModel == null)
            {
                return null;
            }

            _context.Remove(userModel);
            await _context.SaveChangesAsync();
            return userModel;
        }

        public async Task<User?> DeleteByUsernameAsync(string username)
        {
            var userModel = await _context.Users.FirstOrDefaultAsync(i => i.Username == username);

            if(userModel == null)
            {
                return null;
            }

            _context.Remove(userModel);
            await _context.SaveChangesAsync();
            return userModel;
        }

        public async Task<List<User>> GetAllAsync(QueryObject query)
        {
            var users = _context.Users.Include(c => c.Reviews).AsQueryable();

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await users.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.Include(c => c.Reviews).FirstOrDefaultAsync(i => i.UserId == id);
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users.Include(c => c.Reviews).FirstOrDefaultAsync(i => i.Username == username);
        }

        public async Task<User?> UpdateAsync(int id, UpdateUserRequestDTO userUpdateDTO)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);

            if(existingUser == null)
            {
                return null;
            }

            existingUser.Username = userUpdateDTO.Username;
            existingUser.Email = userUpdateDTO.Email;
            existingUser.Password = userUpdateDTO.Password;
            existingUser.DateOfBirth = userUpdateDTO.DateOfBirth;
            existingUser.IsActive = userUpdateDTO.IsActive;

            await _context.SaveChangesAsync();
            return existingUser;
        }

        public Task<bool> UserExists(int id)
        {
            return _context.Users.AnyAsync(s => s.UserId == id);
        }
    }
}
