using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedPrestige.Models.Entities
{
    [Table("doctors")]
    public class Doctor
    {
        [Key]
        [Column("doctor_id")]
        public int DoctorId { get; set; }

        [Column("user_id")]
        public int? UserId { get; set; }

        [MaxLength(100)]
        [Column("occupation")]
        public string Occupation { get; set; }

        [Column("bio")]
        public string Bio { get; set; }

        [MaxLength(255)]
        [Column("location")]
        public string Location { get; set; }

        [Column("experience")]
        public int? Experience { get; set; }

        [MaxLength(20)]
        [Column("status")]
        public string Status { get; set; }

        [MaxLength(255)]
        [Column("image")]
        public string Image { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<DoctorDetail> DoctorDetails { get; set; }
        public virtual ICollection<DoctorService> DoctorServices { get; set; }
    }
}