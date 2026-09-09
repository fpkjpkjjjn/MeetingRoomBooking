using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MeetingRoomBooking.Data;
using MeetingRoomBooking.Models;
using MeetingRoomBooking.DTOs;

namespace MeetingRoomBooking.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase {
        private readonly AppDbContext _context;

        public RoomsController(AppDbContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomDto>>> GetRooms() {
            var rooms = await _context.Rooms.Select(r => new RoomDto {
                    Id = r.Id,
                    Name = r.Name,
                    Capacity = r.Capacity,
                    Location = r.Location
            }).ToListAsync();

            return Ok(rooms);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoomDto>> GetRoom(int id) {
            var room = await _context.Rooms.Where(r => r.Id == id).Select(r => new RoomDto {
                    Id = r.Id,
                    Name = r.Name,
                    Capacity = r.Capacity,
                    Location = r.Location
            }).FirstOrDefaultAsync();

            if (room == null)
                return NotFound();

            return Ok(room);
        }

        [HttpPost]
        public async Task<ActionResult<RoomDto>> CreateRoom(Room room) {
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();

            var dto = new RoomDto {
                Id = room.Id,
                Name = room.Name,
                Capacity = room.Capacity,
                Location = room.Location
            };

            return CreatedAtAction(nameof(GetRoom), new { id = room.Id }, dto);
        }
    }
}