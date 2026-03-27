using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedPrestige.Models.Entities
{
    [Table("doctor_details")]
    public class DoctorDetail
    {
        [Key]
        [Column("detail_id")]
        public int DetailId { get; set; }

        [Column("doctor_id")]
        public int? DoctorId { get; set; }

        [MaxLength(50)]
        [Column("type")]
        public string Type { get; set; }

        [Column("value")]
        public string Value { get; set; }

        [ForeignKey("DoctorId")]
        public virtual Doctor Doctor { get; set; }
    }
}
