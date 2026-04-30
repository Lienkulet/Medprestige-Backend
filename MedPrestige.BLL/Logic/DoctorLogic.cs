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
        private readonly IDoctorDetailRepository _doctorDetailRepository;
        private readonly IUserRepository _userRepository;

        public DoctorLogic(IDoctorRepository doctorRepository, IDoctorServiceRepository doctorServiceRepository, IDoctorDetailRepository doctorDetailRepository, IUserRepository userRepository, IMapper mapper) : base(mapper)
        {
            _doctorRepository = doctorRepository;
            _doctorServiceRepository = doctorServiceRepository;
            _doctorDetailRepository = doctorDetailRepository;
            _userRepository = userRepository;
        }

        private void SaveDetails(int doctorId, DoctorDto dto)
        {
            _doctorDetailRepository.DeleteByDoctorId(doctorId);
            if (!string.IsNullOrWhiteSpace(dto.WorkingHours))
                _doctorDetailRepository.Add(new DoctorDetail { DoctorId = doctorId, Type = "working_hours", Value = dto.WorkingHours });
            if (!string.IsNullOrWhiteSpace(dto.Qualifications))
                _doctorDetailRepository.Add(new DoctorDetail { DoctorId = doctorId, Type = "qualifications", Value = dto.Qualifications });
            if (dto.Details != null)
            {
                foreach (var d in dto.Details.Where(d => d.Type != "working_hours" && d.Type != "qualifications" && !string.IsNullOrWhiteSpace(d.Value)))
                    _doctorDetailRepository.Add(new DoctorDetail { DoctorId = doctorId, Type = d.Type, Value = d.Value });
            }
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
            // Create the linked user first so Name/Email/Phone are persisted
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                Password = "changeme",
                Status = "Active"
            };
            _userRepository.Add(user);

            var doctor = new Doctor
            {
                UserId = user.UserId,
                Occupation = dto.Occupation,
                Bio = dto.Bio,
                Location = dto.Location,
                Experience = dto.Experience,
                Status = dto.Status,
                Image = dto.Image
            };
            _doctorRepository.Add(doctor);

            if (dto.ServiceIds != null)
            {
                foreach (var serviceId in dto.ServiceIds)
                    _doctorServiceRepository.Add(new DoctorService { DoctorId = doctor.DoctorId, ServiceId = serviceId });
            }

            SaveDetails(doctor.DoctorId, dto);
        }

        public void Update(DoctorDto dto)
        {
            var doctor = _doctorRepository.GetById(dto.DoctorId);
            if (doctor == null) return;

            // Update or create the linked user so Name/Email/Phone are persisted
            if (doctor.UserId.HasValue)
            {
                var user = _userRepository.GetById(doctor.UserId.Value);
                if (user != null)
                {
                    user.Name = dto.Name;
                    user.Email = dto.Email;
                    user.Phone = dto.Phone;
                    _userRepository.Update(user);
                }
            }
            else
            {
                var user = new User
                {
                    Name = dto.Name,
                    Email = dto.Email,
                    Phone = dto.Phone,
                    Password = "changeme",
                    Status = "Active"
                };
                _userRepository.Add(user);
                doctor.UserId = user.UserId;
            }

            doctor.Occupation = dto.Occupation;
            doctor.Bio = dto.Bio;
            doctor.Location = dto.Location;
            doctor.Experience = dto.Experience;
            doctor.Status = dto.Status;
            doctor.Image = dto.Image;
            _doctorRepository.Update(doctor);

            if (dto.ServiceIds != null)
            {
                _doctorServiceRepository.DeleteByDoctorId(dto.DoctorId);
                foreach (var serviceId in dto.ServiceIds)
                    _doctorServiceRepository.Add(new DoctorService { DoctorId = dto.DoctorId, ServiceId = serviceId });
            }

            SaveDetails(dto.DoctorId, dto);
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
            var doctor = _doctorRepository.GetById(id);
            if (doctor == null) return;

            _doctorServiceRepository.DeleteByDoctorId(id);
            _doctorDetailRepository.DeleteByDoctorId(id);
            _doctorRepository.Delete(id);

            if (doctor.UserId.HasValue)
                _userRepository.Delete(doctor.UserId.Value);
        }
    }
}
