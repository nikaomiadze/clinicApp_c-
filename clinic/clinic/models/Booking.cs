namespace clinic.models
{
    public class Booking
    {
        public int Id { get; set; } 
        public int? UserId { get; set; }
        public int? DoctorId { get; set; }
        public DateTime? Booking_date { get; set; }
        public string? Description { get; set; }

    }
}
