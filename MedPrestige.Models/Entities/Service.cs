using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedPrestige.Models.Entities
{
    [Table("services")]
    public class Service
    {
        [Key]
        [Column("service_id")]
        public int ServiceId { get; set; }

        [MaxLength(100)]
        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("duration")]
        public int? Duration { get; set; }

        [Column("price")]
        public decimal? Price { get; set; }

        [MaxLength(255)]
        [Column("image")]
        public string Image { get; set; }

        [MaxLength(20)]
        [Column("status")]
        public string Status { get; set; }

        public virtual ICollection<DoctorService> DoctorServices { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
