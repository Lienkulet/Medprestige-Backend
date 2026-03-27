using MedPrestige.DAL.Interfaces;
using MedPrestige.Models.Context;
using MedPrestige.Models.Entities;

namespace MedPrestige.DAL.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly AppDbContext _context;

        public ServiceRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Service> GetAll()
        {
            return _context.Services.ToList();
        }

        public Service GetById(int id)
        {
            return _context.Services.FirstOrDefault(s => s.ServiceId == id);
        }

        public List<Service> GetByStatus(string status)
        {
            return _context.Services.Where(s => s.Status == status).ToList();
        }

        public void Add(Service service)
        {
            _context.Services.Add(service);
            _context.SaveChanges();
        }

        public void Update(Service service)
        {
            _context.Services.Update(service);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var service = _context.Services.Find(id);
            if (service != null)
            {
                _context.Services.Remove(service);
                _context.SaveChanges();
            }
        }
    }
}
