using MedPrestige.Models.Entities;

namespace MedPrestige.DAL.Interfaces
{
    public interface IAppointmentRepository
    {
        List<Appointment> GetAll();
        Appointment GetById(int id);
        List<Appointment> GetByDoctorId(int doctorId);
        List<Appointment> GetByPatientId(int patientId);
        List<Appointment> GetByStatus(string status);
        void Add(Appointment appointment);
        void Update(Appointment appointment);
        void Delete(int id);
    }
}
