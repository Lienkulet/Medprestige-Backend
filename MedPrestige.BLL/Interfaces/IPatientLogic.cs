using MedPrestige.Models.DTOs;

namespace MedPrestige.BLL.Interfaces
{
    public interface IPatientLogic
    {
        List<PatientDto> GetAll();
        PatientDto GetById(int id);
        PatientDto GetByUserId(int userId);
        void Add(PatientDto dto);
        void Update(PatientDto dto);
        void Delete(int id);
    }
}
