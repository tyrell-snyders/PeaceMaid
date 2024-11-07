using Microsoft.AspNetCore.Mvc;
using PeaceMaid.Application.Interfaces;
using PeaceMaid.Domain.Entities;

namespace PeaceMaid.Presentation.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController(IBooking booking) : ControllerBase
    {
        private readonly IBooking _booking = booking;

        [HttpGet]
        public async Task<IActionResult> GetBookings()
        {
            var result = await _booking.GetBookingsAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBooking(int id)
        {
            var result =  await _booking.GetBookingAsync(id);
            return Ok(result);
        }

        [HttpPost("book")]
        public async Task<IActionResult> PostBooking(Booking bkng)
        {
            var result = await _booking.BookAsync(bkng);
            return Ok(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var result = await _booking.CancelAsync(id);
            return Ok(result);
        }
    }
}
