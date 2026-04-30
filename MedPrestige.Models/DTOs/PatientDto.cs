namespace MedPrestige.Models.DTOs
{
    public class PatientDto
    {
        public int PatientId { get; set; }
        public int? UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
    }
}
