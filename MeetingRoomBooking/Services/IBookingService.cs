using MeetingRoomBooking.DTOs;
using MeetingRoomBooking.Models;

namespace MeetingRoomBooking.Services {
    public interface IBookingService {
        Task<(bool Success, string? ErrorMessage, Booking? Booking)> CreateBookingAsync(CreateBookingDto dto);
        Task<(bool Success, string? ErrorMessage, Booking? Booking)> UpdateBookingAsync(int id, UpdateBookingDto dto);
    }
}