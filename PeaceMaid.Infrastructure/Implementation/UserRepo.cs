using Microsoft.EntityFrameworkCore;
using PeaceMaid.Application.DTOs;
using PeaceMaid.Application.Interfaces;
using PeaceMaid.Application.Interfaces.Authentication;
using PeaceMaid.Domain.Entities;
using PeaceMaid.Infrastructure.Data;
using PeaceMaid.Infrastructure.Middleware;


namespace PeaceMaid.Infrastructure.Implementation
{
    public class UserRepo(AppDbContext context, IPasswordHasher passwordHasher, UserAuth userAuth) : IUser
    {
        private readonly AppDbContext _context = context;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;
        private readonly UserAuth _userAuth = userAuth;
        async Task<ServiceResponse> IUser.AddAsync(User user)
        {
            var newUser = new User
            {
                Email = user.Email,
                HashedPass = _passwordHasher.Hash(user.Email),
                Username = user.Username,
                Address = user.Address,
                Bookings = user.Bookings,
                Id = user.Id,
                Reviews = user.Reviews,
            };

            // check for duplicates. Only 1 email can be connected to 1 user
            var check = await _context.Users.FirstOrDefaultAsync(_ => 
                _.Email.ToLower() == newUser.Email.ToLower()
            );

            if (check != null)
                return new(false, "User already exists");

            await _context.Users.AddAsync(newUser);
            await SaveChangesAsync();

            return new ServiceResponse(true, "Added");
        }

        async Task<ServiceResponse> IUser.DeleteAsync(int Id)
        {
            var user = await _context.Users.FindAsync(Id);
            if (user == null)
                return new ServiceResponse(false, "User not found");

            _context.Users.Remove(user);
            await SaveChangesAsync();

            return new ServiceResponse(true, "Deleted");
        }

        async Task<List<User>> IUser.GetAllAsync() => await _context.Users.AsNoTracking().ToListAsync();

        async Task<User> IUser.GetByIdAsync(int Id) => await _context.Users.FindAsync(Id);

        async Task <ServiceResponse> IUser.UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await SaveChangesAsync();

            return new ServiceResponse(true, "Updated");
        }

        public async Task<string> LoginAsync(UserDTO userDTO)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userDTO.Email);
            if (user != null && _passwordHasher.Verify(user.HashedPass, userDTO.Password))
            {
                // Create a JWT token
                return _userAuth.CreateToken(user);
            }

            return "Not logged in";
        }

        private async Task SaveChangesAsync() => await _context.SaveChangesAsync();

        
    }
}
