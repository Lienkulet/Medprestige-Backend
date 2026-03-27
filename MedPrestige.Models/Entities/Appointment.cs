using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedPrestige.Models.Entities
{
    [Table("appointments")]
    public class Appointment
    {
        [Key]
        [Column("appointment_id")]
        public int AppointmentId { get; set; }

        [Column("patient_id")]
        public int? PatientId { get; set; }

        [Column("doctor_id")]
        public int? DoctorId { get; set; }

        [Column("service_id")]
        public int? ServiceId { get; set; }

        [Column("start_at")]
        public DateTime? StartAt { get; set; }

        [Column("end_at")]
        public DateTime? EndAt { get; set; }

        [MaxLength(20)]
        [Column("status")]
        public string Status { get; set; }

        [ForeignKey("PatientId")]
        public virtual Patient Patient { get; set; }

        [ForeignKey("DoctorId")]
        public virtual Doctor Doctor { get; set; }

        [ForeignKey("ServiceId")]
        public virtual Service Service { get; set; }
    }
}
