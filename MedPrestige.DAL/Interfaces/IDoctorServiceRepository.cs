using MedPrestige.Models.Entities;

namespace MedPrestige.DAL.Interfaces
{
    public interface IDoctorServiceRepository
    {
        List<DoctorService> GetByDoctorId(int doctorId);
        List<DoctorService> GetByServiceId(int serviceId);
        void Add(DoctorService doctorService);
        void Delete(int id);
    }
}
