using MedPrestige.Models.DTOs;

namespace MedPrestige.BLL.Interfaces
{
    public interface IServiceLogic
    {
        List<ServiceDto> GetAll();
        ServiceDto GetById(int id);
        List<ServiceDto> GetByStatus(string status);
        void Add(ServiceDto dto);
        void Update(ServiceDto dto);
        void Delete(int id);
    }
}
