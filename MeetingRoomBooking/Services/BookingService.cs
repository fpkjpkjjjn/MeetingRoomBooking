using Microsoft.EntityFrameworkCore;
using MeetingRoomBooking.Data;
using MeetingRoomBooking.DTOs;
using MeetingRoomBooking.Models;

namespace MeetingRoomBooking.Services {
    public class BookingService : IBookingService {
        private readonly AppDbContext _context;

        public BookingService(AppDbContext context) {
            _context = context;
        }

        public async Task<(bool Success, string? ErrorMessage, Booking? Booking)> CreateBookingAsync(CreateBookingDto dto) {
            if(dto.EndTime <= dto.StartTime)
                return (false, "The end time must be later than the start time.", null);

            var roomExists = await _context.Rooms.AnyAsync(r => r.Id == dto.RoomId);
            if(!roomExists)
                return (false, "A room with this ID not found.", null);

            bool hasConflict = await _context.Bookings.AnyAsync(b => b.RoomId == dto.RoomId && b.StartTime < dto.EndTime && b.EndTime > dto.StartTime);

            if(hasConflict)
                return (false, "The room is already booked for an overlapping time.", null);

            var booking = new Booking {
                RoomId = dto.RoomId,
                UserName = dto.UserName,
                Title = dto.Title,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return (true, null, booking);
        }

        public async Task<(bool Success, string? ErrorMessage, Booking? Booking)> UpdateBookingAsync(int id, UpdateBookingDto dto) {
            var booking = await _context.Bookings.FindAsync(id);
            if(booking == null)
                return (false, "Booking not found.", null);

            if(dto.EndTime <= dto.StartTime)
                return (false, "The end time must be later than the start time.", null);

            var roomExists = await _context.Rooms.AnyAsync(r => r.Id == dto.RoomId);
            if(!roomExists)
                return (false, "A room with this ID not found.", null);

            bool hasConflict = await _context.Bookings.AnyAsync(b => b.Id != id && b.RoomId == dto.RoomId && b.StartTime < dto.EndTime && b.EndTime > dto.StartTime);

            if(hasConflict)
                return (false, "The room is already booked for an overlapping time.", null);

            booking.RoomId = dto.RoomId;
            booking.UserName = dto.UserName;
            booking.Title = dto.Title;
            booking.StartTime = dto.StartTime;
            booking.EndTime = dto.EndTime;

            await _context.SaveChangesAsync();

            return (true, null, booking);
        }
    }
}