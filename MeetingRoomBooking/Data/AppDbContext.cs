using Microsoft.EntityFrameworkCore;
using MeetingRoomBooking.Models;

namespace MeetingRoomBooking.Data {
    public class AppDbContext : DbContext {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Room> Rooms => Set<Room>();
        public DbSet<Booking> Bookings => Set<Booking>();
    }
}
