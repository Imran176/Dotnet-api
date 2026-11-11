
using DotnetApi.Models;

namespace DotnetApi.Data
{
    public interface IUserRepository
    {
        public bool SaveChanges();

        public void AddEntity<T>(T entityToAdd);

        public void RemoveEntity<T>(T entityToRemove);

        public IEnumerable<User> GetUsersEF();

        public User GetSingleUserEF(int userId);

        public UserSalary GetUserSalaryEF(int userId);

        public UserJobInfo GetUserJobInfoEF(int userId);
    }
}
