using PeaceMaid.Application.DTOs;
using PeaceMaid.Domain.Entities;

namespace PeaceMaid.Application.Interfaces
{
    public interface IBooking
    {
        Task<List<Booking>> GetBookingsAsync();
        Task<Booking?> GetBookingAsync(int id);
        Task<ServiceResponse> BookAsync(Booking booking);
        Task<ServiceResponse> CancelAsync(int id);
    }
}
