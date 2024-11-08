using Microsoft.EntityFrameworkCore;
using PeaceMaid.Application.DTOs;
using PeaceMaid.Application.Interfaces;
using PeaceMaid.Domain.Entities;
using PeaceMaid.Infrastructure.Data;

namespace PeaceMaid.Infrastructure.Implementation
{
    public class BookingRepo(AppDbContext context) : IBooking
    {
        private readonly AppDbContext _context = context;

        public async Task<ServiceResponse> BookAsync(Booking booking)
        {
            await _context.Bookings.AddAsync(booking);
            await SaveChangesAsync();

            // create a new payment record in the db with the status of pending
            var serviceId = booking.ServiceId;
            var bookingId = booking.BookingId;
            var userId = booking.UserId;

            var amount = await _context.Services
                .Where(s => s.ServiceId == serviceId)
                .Select(s => s.Price)
                .FirstOrDefaultAsync();

            Payment payment = new Payment 
            {
                UserId = userId,
                BookingId = bookingId,
                Status = PaymentStatus.Pending,
                Amount = amount
            };

            await _context.Payments.AddAsync(payment);
            await SaveChangesAsync();

            return new(true, "Booked.");
        }

        public async Task<ServiceResponse> CancelAsync(int id)
        {
            var data = await _context.Bookings.FindAsync(id);
            if (data == null) 
                return new(false, "Booking id not found");

            _context.Bookings.Remove(data);
            await SaveChangesAsync();

            return new(true, "Canceled");
        }

        public async Task<Booking?> GetBookingAsync(int id) => 
            await _context.Bookings.FindAsync(id);

        public async Task<List<Booking>> GetBookingsAsync() =>
            await _context.Bookings.Include(b => b.Service).ToListAsync();

        private async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
