using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MeetingRoomBooking.Data;
using MeetingRoomBooking.DTOs;
using MeetingRoomBooking.Services;

namespace MeetingRoomBooking.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IBookingService _bookingService;

        public BookingsController(AppDbContext context, IBookingService bookingService)
        {
            _context = context;
            _bookingService = bookingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBookings(
            [FromQuery] int? roomId,
            [FromQuery] DateTime? date,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            if (page < 1) page = 1;
            if (pageSize < 1 || pageSize > 100) pageSize = 10;

            var query = _context.Bookings.AsQueryable();

            if (roomId.HasValue)
            {
                query = query.Where(b => b.RoomId == roomId.Value);
            }

            if (date.HasValue)
            {
                var dayStart = date.Value.Date;
                var dayEnd = dayStart.AddDays(1);
                query = query.Where(b => b.StartTime < dayEnd && b.EndTime > dayStart);
            }

            var totalCount = await query.CountAsync();

            var bookings = await query
                .OrderBy(b => b.StartTime)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(b => new BookingDto
                {
                    Id = b.Id,
                    RoomId = b.RoomId,
                    RoomName = b.Room!.Name,
                    UserName = b.UserName,
                    Title = b.Title,
                    StartTime = b.StartTime,
                    EndTime = b.EndTime
                })
                .ToListAsync();

            var result = new PagedResult<BookingDto>
            {
                Items = bookings,
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount
            };

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBooking(int id)
        {
            var booking = await _context.Bookings
                .Where(b => b.Id == id)
                .Select(b => new BookingDto
                {
                    Id = b.Id,
                    RoomId = b.RoomId,
                    RoomName = b.Room!.Name,
                    UserName = b.UserName,
                    Title = b.Title,
                    StartTime = b.StartTime,
                    EndTime = b.EndTime
                })
                .FirstOrDefaultAsync();

            if (booking == null)
            {
                return NotFound();
            }

            return Ok(booking);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking(CreateBookingDto dto)
        {
            var (success, errorMessage, booking) = await _bookingService.CreateBookingAsync(dto);

            if (!success)
            {
                return Conflict(new { message = errorMessage });
            }

            return CreatedAtAction(nameof(GetBooking), new { id = booking!.Id }, new { booking.Id });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
            {
                return NotFound();
            }

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(int id, UpdateBookingDto dto)
        {
            var (success, errorMessage, booking) = await _bookingService.UpdateBookingAsync(id, dto);

            if (!success)
            {
                if (errorMessage == "Booking not found.")
                {
                    return NotFound(new { message = errorMessage });
                }

                return Conflict(new { message = errorMessage });
            }

            return NoContent();
        }
    }
}