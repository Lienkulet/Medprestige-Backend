using MedPrestige.Models.Entities;

namespace MedPrestige.DAL.Interfaces
{
    public interface IServiceRepository
    {
        List<Service> GetAll();
        Service GetById(int id);
        List<Service> GetByStatus(string status);
        void Add(Service service);
        void Update(Service service);
        void Delete(int id);
    }
}
