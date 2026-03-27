using MedPrestige.DAL.Interfaces;
using MedPrestige.Models.Context;
using MedPrestige.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedPrestige.DAL.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly AppDbContext _context;

        public AppointmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Appointment> GetAll()
        {
            return _context.Appointments
                .Include(a => a.Patient).ThenInclude(p => p.User)
                .Include(a => a.Doctor).ThenInclude(d => d.User)
                .Include(a => a.Service)
                .ToList();
        }

        public Appointment GetById(int id)
        {
            return _context.Appointments
                .Include(a => a.Patient).ThenInclude(p => p.User)
                .Include(a => a.Doctor).ThenInclude(d => d.User)
                .Include(a => a.Service)
                .FirstOrDefault(a => a.AppointmentId == id);
        }

        public List<Appointment> GetByDoctorId(int doctorId)
        {
            return _context.Appointments
                .Include(a => a.Patient).ThenInclude(p => p.User)
                .Include(a => a.Service)
                .Where(a => a.DoctorId == doctorId)
                .ToList();
        }

        public List<Appointment> GetByPatientId(int patientId)
        {
            return _context.Appointments
                .Include(a => a.Doctor).ThenInclude(d => d.User)
                .Include(a => a.Service)
                .Where(a => a.PatientId == patientId)
                .ToList();
        }

        public List<Appointment> GetByStatus(string status)
        {
            return _context.Appointments
                .Include(a => a.Patient).ThenInclude(p => p.User)
                .Include(a => a.Doctor).ThenInclude(d => d.User)
                .Include(a => a.Service)
                .Where(a => a.Status == status)
                .ToList();
        }

        public void Add(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            _context.SaveChanges();
        }

        public void Update(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var appointment = _context.Appointments.Find(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                _context.SaveChanges();
            }
        }
    }
}
