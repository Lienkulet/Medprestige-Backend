using MedPrestige.DAL.Interfaces;
using MedPrestige.Models.Context;
using MedPrestige.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedPrestige.DAL.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly AppDbContext _context;

        public PatientRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Patient> GetAll()
        {
            return _context.Patients.Include(p => p.User).ToList();
        }

        public Patient GetById(int id)
        {
            return _context.Patients.Include(p => p.User).FirstOrDefault(p => p.PatientId == id);
        }

        public Patient GetByUserId(int userId)
        {
            return _context.Patients.Include(p => p.User).FirstOrDefault(p => p.UserId == userId);
        }

        public void Add(Patient patient)
        {
            _context.Patients.Add(patient);
            _context.SaveChanges();
        }

        public void Update(Patient patient)
        {
            _context.Patients.Update(patient);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var patient = _context.Patients.Find(id);
            if (patient != null)
            {
                _context.Patients.Remove(patient);
                _context.SaveChanges();
            }
        }
    }
}
