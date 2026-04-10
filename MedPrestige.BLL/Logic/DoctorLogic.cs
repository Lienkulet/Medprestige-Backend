using AutoMapper;
using MedPrestige.BLL.Interfaces;
using MedPrestige.DAL.Interfaces;
using MedPrestige.Models.DTOs;
using MedPrestige.Models.Entities;

namespace MedPrestige.BLL.Logic
{
    public class DoctorLogic : BaseLogic<Doctor, DoctorDto>, IDoctorLogic
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IDoctorServiceRepository _doctorServiceRepository;

        public DoctorLogic(IDoctorRepository doctorRepository, IDoctorServiceRepository doctorServiceRepository, IMapper mapper) : base(mapper)
        {
            _doctorRepository = doctorRepository;
            _doctorServiceRepository = doctorServiceRepository;
        }

        public List<DoctorDto> GetAll()
        {
            return MapToDtoList(_doctorRepository.GetAll());
        }

        public DoctorDto GetById(int id)
        {
            return MapToDto(_doctorRepository.GetById(id));
        }

        public List<DoctorDto> GetByStatus(string status)
        {
            return MapToDtoList(_doctorRepository.GetByStatus(status));
        }

        public void Add(DoctorDto dto)
        {
            var doctor = new Doctor
            {
                UserId = dto.UserId,
                Occupation = dto.Occupation,
                Bio = dto.Bio,
                Location = dto.Location,
                Experience = dto.Experience,
                Status = dto.Status,
                Image = dto.Image
            };

            _doctorRepository.Add(doctor);
        }

        public void Update(DoctorDto dto)
        {
            var doctor = _doctorRepository.GetById(dto.DoctorId);
            if (doctor == null) return;

            doctor.Occupation = dto.Occupation;
            doctor.Bio = dto.Bio;
            doctor.Location = dto.Location;
            doctor.Experience = dto.Experience;
            doctor.Status = dto.Status;
            doctor.Image = dto.Image;

            _doctorRepository.Update(doctor);
        }

        public List<DoctorDto> GetByServiceId(int serviceId)
        {
            var doctorServices = _doctorServiceRepository.GetByServiceId(serviceId);
            var doctors = doctorServices
                .Where(ds => ds.Doctor != null)
                .Select(ds => ds.Doctor)
                .ToList();
            return MapToDtoList(doctors);
        }

        public void Delete(int id)
        {
            _doctorRepository.Delete(id);
        }
    }
}
