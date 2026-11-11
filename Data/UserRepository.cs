
using DotnetApi.Models;

namespace DotnetApi.Data
{
    public class UserRepository : IUserRepository
    {
        // Declare a DataContextEF object to interact with the database
        // using Entity Framework Core.
        DataContextEF _entityFramework;

        // Constructor: Initializes the database context and AutoMapper.
        // IConfiguration provides access to application configuration,
        // including the database connection string.
        public UserRepository(IConfiguration config)
        {
            // Create an instance of DataContextEF and pass the configuration.
            _entityFramework = new DataContextEF(config);
        }

        public bool SaveChanges()
        {
            return _entityFramework.SaveChanges() > 0;
        }

        public void AddEntity<T>(T entityToAdd)
        {
            if (entityToAdd != null)
            {
                _entityFramework.Add(entityToAdd); ;
            }
        }

        public void RemoveEntity<T>(T entityToRemove)
        {
            if (entityToRemove != null)
            {
                _entityFramework.Remove(entityToRemove); ;
            }
        }

        public IEnumerable<User> GetUsersEF()
        {
            IEnumerable<User> users = _entityFramework.Users.ToList<User>();
            return users;
        }
        
        public User GetSingleUserEF(int userId)
        {
            User? user = _entityFramework.Users
                            .Where(u => u.UserId == userId)
                            .FirstOrDefault<User>();

            if (user != null)
            {
                return user;
            }
            throw new Exception("User not found");
        }

        public UserSalary GetUserSalaryEF(int userId)
        {
            UserSalary? userSalary = _entityFramework.UserSalary
                            .Where(u => u.UserId == userId)
                            .FirstOrDefault<UserSalary>();

            if (userSalary != null)
            {
                return userSalary;
            }
            throw new Exception("User not found");
        }
 
        public UserJobInfo GetUserJobInfoEF(int userId)
        {
            UserJobInfo? userJobInfo = _entityFramework.UserJobInfo
                            .Where(u => u.UserId == userId)
                            .FirstOrDefault<UserJobInfo>();

            if (userJobInfo != null)
            {
                return userJobInfo;
            }
            throw new Exception("User not found");
        }

    }
}