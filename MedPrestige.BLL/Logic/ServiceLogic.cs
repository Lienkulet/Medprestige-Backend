using AutoMapper;
using MedPrestige.BLL.Interfaces;
using MedPrestige.DAL.Interfaces;
using MedPrestige.Models.DTOs;
using MedPrestige.Models.Entities;

namespace MedPrestige.BLL.Logic
{
    public class ServiceLogic : BaseLogic<Service, ServiceDto>, IServiceLogic
    {
        private readonly IServiceRepository _serviceRepository;

        public ServiceLogic(IServiceRepository serviceRepository, IMapper mapper) : base(mapper)
        {
            _serviceRepository = serviceRepository;
        }

        public List<ServiceDto> GetAll()
        {
            return MapToDtoList(_serviceRepository.GetAll());
        }

        public ServiceDto GetById(int id)
        {
            return MapToDto(_serviceRepository.GetById(id));
        }

        public List<ServiceDto> GetByStatus(string status)
        {
            return MapToDtoList(_serviceRepository.GetByStatus(status));
        }

        public void Add(ServiceDto dto)
        {
            var service = new Service
            {
                Name = dto.Name,
                Description = dto.Description,
                Duration = dto.Duration,
                Price = dto.Price,
                Image = dto.Image,
                Status = dto.Status
            };

            _serviceRepository.Add(service);
        }

        public void Update(ServiceDto dto)
        {
            var service = _serviceRepository.GetById(dto.ServiceId);
            if (service == null) return;

            service.Name = dto.Name;
            service.Description = dto.Description;
            service.Duration = dto.Duration;
            service.Price = dto.Price;
            service.Image = dto.Image;
            service.Status = dto.Status;

            _serviceRepository.Update(service);
        }

        public void Delete(int id)
        {
            _serviceRepository.Delete(id);
        }
    }
}
