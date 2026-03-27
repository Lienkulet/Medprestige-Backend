using MedPrestige.Models.Entities;

namespace MedPrestige.DAL.Interfaces
{
    public interface IPatientRepository
    {
        List<Patient> GetAll();
        Patient GetById(int id);
        Patient GetByUserId(int userId);
        void Add(Patient patient);
        void Update(Patient patient);
        void Delete(int id);
    }
}
