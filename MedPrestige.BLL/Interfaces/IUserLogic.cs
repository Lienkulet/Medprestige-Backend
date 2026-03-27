using MedPrestige.Models.DTOs;

namespace MedPrestige.BLL.Interfaces
{
    public interface IUserLogic
    {
        List<UserDto> GetAll();
        UserDto GetById(int id);
        UserDto Login(string email, string password);
        void Add(UserDto dto, string password);
        void Update(UserDto dto);
        void Delete(int id);
    }
}
