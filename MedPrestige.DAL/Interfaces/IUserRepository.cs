using MedPrestige.Models.Entities;

namespace MedPrestige.DAL.Interfaces
{
    public interface IUserRepository
    {
        List<User> GetAll();
        User GetById(int id);
        User GetByEmail(string email);
        void Add(User user);
        void Update(User user);
        void Delete(int id);
    }
}
