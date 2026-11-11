

using DotnetApi.Data;
using DotnetApi.Dtos;
using DotnetApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    DataContextDapper _dapper;
    public UserController(IConfiguration config)
    {
        _dapper = new DataContextDapper(config);
    }

    [HttpGet("TestConnection")]
    public DateTime TestConnection()
    {
        return _dapper.LoadSingleData<DateTime>("SELECT GETDATE()");
    }


    [HttpGet("test/{testValue}")]
    // public IActionResult Test()
    public string[] GetUsers(string testValue)
    {
        string[] responseArray = new string[] {
            "test1", "test2", testValue
        };
        return responseArray;
    }


    [HttpGet("GetUsers")]
    public IEnumerable<User> GetAllUsers()
    {
        // @ allows us to add spaces between the quotes
        string sql = @"
            SELECT [UserId],
                [FirstName],
                [LastName],
                [Email],
                [Gender],
                [Active]
            FROM Project1AppSchema.Users";
        IEnumerable<User> users = _dapper.LoadData<User>(sql);
        return users;
    }


    [HttpGet("GetUsers/{userId}")]
    public IActionResult GetUserByID(int userId)
    {
        string sql = @"
            SELECT [UserId],
                [FirstName],
                [LastName],
                [Email],
                [Gender],
                [Active]
            FROM Project1AppSchema.Users
            WHERE UserId = " + userId.ToString();
        
        User? user = _dapper.LoadSingleData<User>(sql);

        if (user == null)
        {
            throw new Exception("User not found");
        }

        return Ok(user);
    }

    [HttpPost("AddUser")]
    public IActionResult AddUser(UserToAddDto user)
    {
        string sql = @"
            INSERT INTO Project1AppSchema.Users(
                [FirstName],
                [LastName],
                [Email],
                [Gender],
                [Active]
            ) VALUES (
                @FirstName,
                @LastName,
                @Email,
                @Gender,
                @Active
            )";
        if (_dapper.ExecuteSql(sql, user))
        {
            return Ok("User added successfully!");
        }
        throw new Exception("Failed to add user!");
    }

    [HttpPut("EditUser")]
    public IActionResult EditUser(User user)
    {
        string sql = @"
            UPDATE Project1AppSchema.Users
            SET
                FirstName = @FirstName,
                LastName = @LastName,
                Email = @Email,
                Gender = @Gender,
                Active = @Active
            WHERE UserId = @UserId";

        if (_dapper.ExecuteSql(sql, user))
        {
            return Ok("User updated successfully!");
        }
        throw new Exception("Failed to update user!");
    }


    [HttpDelete("DeleteUser/{userId}")]
    public IActionResult DeleteUserByID(int userId)
    {
        string sql = @"
            DELETE FROM Project1AppSchema.Users
            WHERE Project1AppSchema.Users.UserId = " + userId.ToString();
        if (_dapper.ExecuteSql(sql))
        {
            return Ok("User delete successfully!");
        }
        throw new Exception("Failed to delete user!");
    }
}
