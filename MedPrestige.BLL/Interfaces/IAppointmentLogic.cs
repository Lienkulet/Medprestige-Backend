using MedPrestige.Models.DTOs;

namespace MedPrestige.BLL.Interfaces
{
    public interface IAppointmentLogic
    {
        List<AppointmentDto> GetAll();
        AppointmentDto GetById(int id);
        List<AppointmentDto> GetByDoctorId(int doctorId);
        List<AppointmentDto> GetByPatientId(int patientId);
        List<AppointmentDto> GetByStatus(string status);
        void Add(AppointmentDto dto);
        void Update(AppointmentDto dto);
        void Delete(int id);
    }
}
