using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedPrestige.Models.Entities
{
    [Table("patients")]
    public class Patient
    {
        [Key]
        [Column("patient_id")]
        public int PatientId { get; set; }

        [Column("user_id")]
        public int? UserId { get; set; }

        [Column("date_of_birth")]
        public DateTime? DateOfBirth { get; set; }

        [MaxLength(10)]
        [Column("gender")]
        public string Gender { get; set; }

        [Column("address")]
        public string Address { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
