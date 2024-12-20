namespace clinic.models
{
    public class Doctor_update
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Person_id { get; set; }
        public string? Password { get; set; }
        public IFormFile? Profile_img { get; set; }
    }
}
