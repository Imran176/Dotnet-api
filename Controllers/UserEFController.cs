
using DotnetApi.Data;
using DotnetApi.Dtos;
using DotnetApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserEFController : ControllerBase
{
    DataContextEF _entityFramework;
    public UserEFController(IConfiguration config)
    {
        _entityFramework = new DataContextEF(config);
    }

    [HttpGet("GetUsers")]
    public IEnumerable<User> GetAllUsers()
    {
        IEnumerable<User> users = _entityFramework.Users.ToList<User>();
        return users;
    }


    [HttpGet("GetUsers/{userId}")]
    public IActionResult GetUserByID(int userId)
    {
        User? user = _entityFramework.Users
                        .Where(u => u.UserId == userId)
                        .FirstOrDefault<User>();

        if (user == null)
        {
            throw new Exception("User not found");
        }

        return Ok(user);
    }

    [HttpPost("AddUser")]
    public IActionResult AddUser(UserToAddDto user)
    {
        User userDb = new User();
        
        userDb.FirstName = user.FirstName;
        userDb.LastName = user.LastName;
        userDb.Email = user.Email;
        userDb.Gender = user.Gender;
        userDb.Active = user.Active;
        _entityFramework.Add(userDb);
        if (_entityFramework.SaveChanges() > 0)
        {
            return Ok("User added successfully!");
        }
        throw new Exception("Failed to add user!");
    }

    [HttpPut("EditUser")]
    public IActionResult EditUser(User user)
    {
        User? userDb = _entityFramework.Users
                        .Where(u => u.UserId == user.UserId)
                        .FirstOrDefault<User>();

        if (userDb != null)
        {
            userDb.FirstName = user.FirstName;
            userDb.LastName = user.LastName;
            userDb.Email = user.Email;
            userDb.Gender = user.Gender;
            userDb.Active = user.Active;
            if (_entityFramework.SaveChanges() > 0)
            {
                return Ok("User updated successfully!");
            }
            throw new Exception("Failed to update user!");
        }
        throw new Exception("User not found");
    }


    [HttpDelete("DeleteUser/{userId}")]
    public IActionResult DeleteUserByID(int userId)
    {
        User? userDb = _entityFramework.Users
                        .Where(u => u.UserId == userId)
                        .FirstOrDefault<User>();

        if (userDb != null)
        {
            _entityFramework.Users.Remove(userDb);
            if (_entityFramework.SaveChanges() > 0)
            {
                return Ok("User deleted successfully!");
            }
            throw new Exception("Failed to delete user!");
        }
        throw new Exception("User not found");
    }
}
