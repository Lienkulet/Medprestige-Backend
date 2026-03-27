using MedPrestige.Models.Entities;

namespace MedPrestige.DAL.Interfaces
{
    public interface IDoctorDetailRepository
    {
        List<DoctorDetail> GetByDoctorId(int doctorId);
        void Add(DoctorDetail detail);
        void Delete(int id);
    }
}
