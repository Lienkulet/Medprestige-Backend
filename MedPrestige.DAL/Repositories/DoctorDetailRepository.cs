using MedPrestige.DAL.Interfaces;
using MedPrestige.Models.Context;
using MedPrestige.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedPrestige.DAL.Repositories
{
    public class DoctorDetailRepository : IDoctorDetailRepository
    {
        private readonly AppDbContext _context;

        public DoctorDetailRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<DoctorDetail> GetByDoctorId(int doctorId)
        {
            return _context.DoctorDetails
                .Where(d => d.DoctorId == doctorId)
                .ToList();
        }

        public void Add(DoctorDetail detail)
        {
            _context.DoctorDetails.Add(detail);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var detail = _context.DoctorDetails.Find(id);
            if (detail != null)
            {
                _context.DoctorDetails.Remove(detail);
                _context.SaveChanges();
            }
        }

        public void DeleteByDoctorId(int doctorId)
        {
            var entries = _context.DoctorDetails.Where(d => d.DoctorId == doctorId).ToList();
            _context.DoctorDetails.RemoveRange(entries);
            _context.SaveChanges();
        }
    }
}
