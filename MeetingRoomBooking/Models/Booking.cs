namespace MeetingRoomBooking.Models {
    public class Booking {
        public int Id { 
            get; 
            set; 
        }
        public int RoomId { 
            get; 
            set; 
        }
        public Room? Room { 
            get; 
            set; 
        }

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
