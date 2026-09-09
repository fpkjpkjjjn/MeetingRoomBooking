using System.ComponentModel.DataAnnotations;

namespace MeetingRoomBooking.DTOs {
    public class CreateBookingDto {
        [Required]
        public int RoomId { 
            get; 
            set; 
        }

        [Required]
        [MaxLength(100)]
        public string UserName { 
            get; 
            set; 
        } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string Title { 
            get; 
            set; 
        } = string.Empty;

        [Required]
        public DateTime StartTime { 
            get; 
            set; 
        }

        [Required]
        public DateTime EndTime { 
            get; 
            set; 
        }
    }
}
