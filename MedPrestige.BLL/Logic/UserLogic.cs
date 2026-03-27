using AutoMapper;
using MedPrestige.BLL.Helpers;
using MedPrestige.BLL.Interfaces;
using MedPrestige.DAL.Interfaces;
using MedPrestige.Models.DTOs;
using MedPrestige.Models.Entities;

namespace MedPrestige.BLL.Logic
{
    public class UserLogic : BaseLogic<User, UserDto>, IUserLogic
    {
        private readonly IUserRepository _userRepository;

        public UserLogic(IUserRepository userRepository, IMapper mapper) : base(mapper)
        {
            _userRepository = userRepository;
        }

        public List<UserDto> GetAll()
        {
            return MapToDtoList(_userRepository.GetAll());
        }

        public UserDto GetById(int id)
        {
            return MapToDto(_userRepository.GetById(id));
        }

        public UserDto Login(string email, string password)
        {
            var user = _userRepository.GetByEmail(email);
            if (user == null || !CryptoHelper.VerifyPassword(password, user.Password))
                return null;

            return MapToDto(user);
        }

        public void Add(UserDto dto, string password)
        {
            var user = new User
            {
                Email = dto.Email,
                Name = dto.Name,
                Phone = dto.Phone,
                Status = dto.Status,
                Password = CryptoHelper.HashPassword(password)
            };

            _userRepository.Add(user);
        }

        public void Update(UserDto dto)
        {
            var user = _userRepository.GetById(dto.UserId);
            if (user == null) return;

            user.Name = dto.Name;
            user.Phone = dto.Phone;
            user.Status = dto.Status;

            _userRepository.Update(user);
        }

        public void Delete(int id)
        {
            _userRepository.Delete(id);
        }
    }
}
