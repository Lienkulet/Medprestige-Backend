using AutoMapper;
using MedPrestige.BLL.Interfaces;
using MedPrestige.DAL.Interfaces;
using MedPrestige.Models.DTOs;
using MedPrestige.Models.Entities;

namespace MedPrestige.BLL.Logic
{
    public class AppointmentLogic : BaseLogic<Appointment, AppointmentDto>, IAppointmentLogic
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentLogic(IAppointmentRepository appointmentRepository, IMapper mapper) : base(mapper)
        {
            _appointmentRepository = appointmentRepository;
        }

        public List<AppointmentDto> GetAll()
        {
            return MapToDtoList(_appointmentRepository.GetAll());
        }

        public AppointmentDto GetById(int id)
        {
            return MapToDto(_appointmentRepository.GetById(id));
        }

        public List<AppointmentDto> GetByDoctorId(int doctorId)
        {
            return MapToDtoList(_appointmentRepository.GetByDoctorId(doctorId));
        }

        public List<AppointmentDto> GetByPatientId(int patientId)
        {
            return MapToDtoList(_appointmentRepository.GetByPatientId(patientId));
        }

        public List<AppointmentDto> GetByStatus(string status)
        {
            return MapToDtoList(_appointmentRepository.GetByStatus(status));
        }

        public void Add(AppointmentDto dto)
        {
            var appointment = new Appointment
            {
                PatientId = dto.AppointmentId,
                StartAt = dto.StartAt,
                EndAt = dto.EndAt,
                Status = dto.Status
            };

            _appointmentRepository.Add(appointment);
        }

        public void Update(AppointmentDto dto)
        {
            var appointment = _appointmentRepository.GetById(dto.AppointmentId);
            if (appointment == null) return;

            appointment.StartAt = dto.StartAt;
            appointment.EndAt = dto.EndAt;
            appointment.Status = dto.Status;

            _appointmentRepository.Update(appointment);
        }

        public void Delete(int id)
        {
            _appointmentRepository.Delete(id);
        }
    }
}
