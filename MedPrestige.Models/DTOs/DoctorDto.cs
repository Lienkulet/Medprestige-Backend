namespace MedPrestige.Models.DTOs
{
    public class DoctorDto
    {
        public int DoctorId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Occupation { get; set; }
        public string Bio { get; set; }
        public string Location { get; set; }
        public int? Experience { get; set; }
        public string Status { get; set; }
        public string Image { get; set; }
        public List<DoctorDetailDto> Details { get; set; }
    }
}
