using MedPrestige.DAL.Interfaces;
using MedPrestige.Models.Context;
using MedPrestige.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedPrestige.DAL.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly AppDbContext _context;

        public DoctorRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Doctor> GetAll()
        {
            return _context.Doctors
                .Include(d => d.User)
                .Include(d => d.DoctorDetails)
                .ToList();
        }

        public Doctor GetById(int id)
        {
            return _context.Doctors
                .Include(d => d.User)
                .Include(d => d.DoctorDetails)
                .FirstOrDefault(d => d.DoctorId == id);
        }

        public Doctor GetByUserId(int userId)
        {
            return _context.Doctors
                .Include(d => d.User)
                .Include(d => d.DoctorDetails)
                .FirstOrDefault(d => d.UserId == userId);
        }

        public List<Doctor> GetByStatus(string status)
        {
            return _context.Doctors
                .Include(d => d.User)
                .Where(d => d.Status == status)
                .ToList();
        }

        public void Add(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            _context.SaveChanges();
        }

        public void Update(Doctor doctor)
        {
            _context.Doctors.Update(doctor);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var doctor = _context.Doctors.Find(id);
            if (doctor != null)
            {
                _context.Doctors.Remove(doctor);
                _context.SaveChanges();
            }
        }
    }
}
