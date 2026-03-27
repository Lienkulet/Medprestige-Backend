namespace MedPrestige.Models.DTOs
{
    public class DoctorDetailDto
    {
        public int DetailId { get; set; }
        public int? DoctorId { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
