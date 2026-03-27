using MedPrestige.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedPrestige.Models.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<DoctorDetail> DoctorDetails { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<DoctorService> DoctorServices { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
    }
}
