using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedPrestige.Models.Entities
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("user_id")]
        public int UserId { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("email")]
        public string Email { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("password")]
        public string Password { get; set; }

        [MaxLength(100)]
        [Column("name")]
        public string Name { get; set; }

        [MaxLength(20)]
        [Column("phone")]
        public string Phone { get; set; }

        [Column("role_id")]
        public int? RoleId { get; set; }

        [MaxLength(20)]
        [Column("status")]
        public string Status { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }

        public virtual Doctor Doctor { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
