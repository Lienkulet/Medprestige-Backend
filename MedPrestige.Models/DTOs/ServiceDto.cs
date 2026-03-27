namespace MedPrestige.Models.DTOs
{
    public class ServiceDto
    {
        public int ServiceId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Duration { get; set; }
        public decimal? Price { get; set; }
        public string Image { get; set; }
        public string Status { get; set; }
    }
}
