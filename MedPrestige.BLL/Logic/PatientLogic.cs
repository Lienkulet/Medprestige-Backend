using AutoMapper;
using MedPrestige.BLL.Interfaces;
using MedPrestige.DAL.Interfaces;
using MedPrestige.Models.DTOs;
using MedPrestige.Models.Entities;

namespace MedPrestige.BLL.Logic
{
    public class PatientLogic : BaseLogic<Patient, PatientDto>, IPatientLogic
    {
        private readonly IPatientRepository _patientRepository;

        public PatientLogic(IPatientRepository patientRepository, IMapper mapper) : base(mapper)
        {
            _patientRepository = patientRepository;
        }

        public List<PatientDto> GetAll()
        {
            return MapToDtoList(_patientRepository.GetAll());
        }

        public PatientDto GetById(int id)
        {
            return MapToDto(_patientRepository.GetById(id));
        }

        public PatientDto GetByUserId(int userId)
        {
            return MapToDto(_patientRepository.GetByUserId(userId));
        }

        public void Add(PatientDto dto)
        {
            var patient = new Patient
            {
                UserId = dto.UserId,
                DateOfBirth = dto.DateOfBirth,
                Gender = dto.Gender,
                Address = dto.Address
            };

            _patientRepository.Add(patient);
        }

        public void Update(PatientDto dto)
        {
            var patient = _patientRepository.GetById(dto.PatientId);
            if (patient == null) return;

            patient.DateOfBirth = dto.DateOfBirth;
            patient.Gender = dto.Gender;
            patient.Address = dto.Address;

            _patientRepository.Update(patient);
        }

        public void Delete(int id)
        {
            _patientRepository.Delete(id);
        }
    }
}
