using MedPrestige.Models.DTOs;

namespace MedPrestige.BLL.Interfaces
{
    public interface IDoctorLogic
    {
        List<DoctorDto> GetAll();
        DoctorDto GetById(int id);
        List<DoctorDto> GetByStatus(string status);
        void Add(DoctorDto dto);
        void Update(DoctorDto dto);
        void Delete(int id);
    }
}
