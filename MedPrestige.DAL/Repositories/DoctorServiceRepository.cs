using MedPrestige.DAL.Interfaces;
using MedPrestige.Models.Context;
using MedPrestige.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedPrestige.DAL.Repositories
{
    public class DoctorServiceRepository : IDoctorServiceRepository
    {
        private readonly AppDbContext _context;

        public DoctorServiceRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<DoctorService> GetByDoctorId(int doctorId)
        {
            return _context.DoctorServices
                .Include(ds => ds.Service)
                .Where(ds => ds.DoctorId == doctorId)
                .ToList();
        }

        public List<DoctorService> GetByServiceId(int serviceId)
        {
            return _context.DoctorServices
                .Include(ds => ds.Doctor).ThenInclude(d => d.User)
                .Where(ds => ds.ServiceId == serviceId)
                .ToList();
        }

        public void Add(DoctorService doctorService)
        {
            _context.DoctorServices.Add(doctorService);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var ds = _context.DoctorServices.Find(id);
            if (ds != null)
            {
                _context.DoctorServices.Remove(ds);
                _context.SaveChanges();
            }
        }

        public void DeleteByDoctorId(int doctorId)
        {
            var entries = _context.DoctorServices.Where(ds => ds.DoctorId == doctorId).ToList();
            _context.DoctorServices.RemoveRange(entries);
            _context.SaveChanges();
        }
    }
}
