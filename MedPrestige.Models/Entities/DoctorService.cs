using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedPrestige.Models.Entities
{
    [Table("doctor_services")]
    public class DoctorService
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("doctor_id")]
        public int? DoctorId { get; set; }

        [Column("service_id")]
        public int? ServiceId { get; set; }

        [ForeignKey("DoctorId")]
        public virtual Doctor Doctor { get; set; }

        [ForeignKey("ServiceId")]
        public virtual Service Service { get; set; }
    }
}
