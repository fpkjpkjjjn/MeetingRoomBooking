namespace MeetingRoomBooking.DTOs {
    public class BookingDto {
        public int Id { 
            get; 
            set; 
        }
        public int RoomId { 
            get; 
            set; 
        }
        public string RoomName { 
            get; 
            set; 
        } = string.Empty;
        public string UserName { 
            get; 
            set; 
        } = string.Empty;
        public string Title { 
            get; 
            set; 
        } = string.Empty;
        public DateTime StartTime { 
            get; 
            set; 
        }
        public DateTime EndTime { 
            get; 
            set; 
        }
    }
}
