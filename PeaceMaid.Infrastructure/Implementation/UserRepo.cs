using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PeaceMaid.Application.DTOs;
using PeaceMaid.Application.Interfaces;
using PeaceMaid.Application.Interfaces.Authentication;
using PeaceMaid.Domain.Entities;
using PeaceMaid.Infrastructure.Data;
using PeaceMaid.Infrastructure.Middleware;
using System.Text.RegularExpressions;


namespace PeaceMaid.Infrastructure.Implementation
{
    public class UserRepo(AppDbContext context, IPasswordHasher passwordHasher, UserAuth userAuth, IConfiguration configuration) : IUser
    {
        private readonly AppDbContext _context = context;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;
        private readonly UserAuth _userAuth = userAuth;
        private readonly IConfiguration _configuration = configuration;
        public async Task<ServiceResponse> AddAsync(User user)
        {
            if (!Regex.IsMatch(user.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                return new ServiceResponse(false, $"Invalid email address format: {user.Email}");
            }

            var newUser = new User
            {
                Email = user.Email,
                HashedPass = _passwordHasher.Hash(user.HashedPass),
                Username = user.Username,
                Address = user.Address,
                Bookings = user.Bookings,
                Id = user.Id,
                Reviews = user.Reviews,
            };

            // Existing duplicate check logic
            var check = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == user.Email.ToLower());
            if (check != null)
            {
                return new ServiceResponse(false, "User already exists");
            }

            await _context.Users.AddAsync(newUser);
            await SaveChangesAsync();

            // Todo: Create a verification token and send an email to the user to verify their email

            return new ServiceResponse(true, "Added");
        }

        public async Task<ServiceResponse> DeleteAsync(int Id)
        {
            var user = await _context.Users.FindAsync(Id);
            if (user == null)
                return new ServiceResponse(false, "User not found");

            _context.Users.Remove(user);
            await SaveChangesAsync();

            return new ServiceResponse(true, "Deleted");
        }

        public async Task<List<User>> GetAllAsync() => await _context.Users.AsNoTracking().ToListAsync();

        public async Task<User> GetByIdAsync(int Id) => await _context.Users.FindAsync(Id);

        public async Task <ServiceResponse> UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await SaveChangesAsync();

            return new ServiceResponse(true, "Updated");
        }

        public async Task<DataResponse> LoginAsync(UserDTO userDTO)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userDTO.Email);

            if (user == null || !Regex.IsMatch(user.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                return new DataResponse(false, user == null ? "Not logged in" : $"Invalid email address format: {user.Email}", null);
            }

            if (_passwordHasher.Verify(userDTO.Password, user.HashedPass))
            {
                var responseData = new { UserID = user.Id, Username = user.Username, Token = _userAuth.CreateToken(user, _configuration) };
                return new DataResponse(true, "Logged in", responseData);
            }

            return new DataResponse(false, "Invalid credentials", null);
        }

        private async Task SaveChangesAsync() => await _context.SaveChangesAsync();

        public async Task<User?> GetByEmailAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.Contains(email));
            if (user == null)
            {
                return null;
            }

            return user;
        }
    }
}
