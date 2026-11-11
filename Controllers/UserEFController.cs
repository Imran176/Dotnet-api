
using AutoMapper;
using DotnetApi.Data;
using DotnetApi.Dtos;
using DotnetApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetApi.Controllers;

// [ApiController] enables API-specific behaviors such as
// automatic model validation and request body binding.
[ApiController]

// [Route("[controller]")] defines the API route using the controller name.
// For UserEFController, the base route becomes /UserEF.
[Route("[controller]")]
public class UserEFController : ControllerBase
{

    IUserRepository _userRepository;

    // Create an ILoggerFactory instance.
    // AutoMapper uses this for internal logging.
    ILoggerFactory loggerFactory = LoggerFactory.Create(builder => { });

    // Declare an IMapper object.
    // AutoMapper uses this to map properties between different objects.
    IMapper _mapper;

    // Constructor: Initializes the database context and AutoMapper.
    // IConfiguration provides access to application configuration,
    // including the database connection string.
    public UserEFController(IConfiguration config, IUserRepository userRepository)
    {

        _userRepository = userRepository;

        // Initialize AutoMapper and configure object-to-object mapping.
        _mapper = new Mapper(new MapperConfiguration(cfg =>
        {
            // Configure AutoMapper to map properties from
            // UserToAddDto into the User model.
            cfg.CreateMap<UserToAddDto, User>();

        }, loggerFactory));
    }

    [HttpGet("GetUsers")]
    public IEnumerable<User> GetUsersEF()
    {
        IEnumerable<User> users = _userRepository.GetUsersEF();
        return users;
    }

    [HttpGet("GetUsers/{userId}")]
    public User GetUserByIdEF(int userId)
    {
        return _userRepository.GetSingleUserEF(userId);
    }

    [HttpPost("AddUser")]
    public IActionResult AddUserEF(UserToAddDto user)
    {
        User userDb = _mapper.Map<User>(user);
        userDb.FirstName = user.FirstName;
        userDb.LastName = user.LastName;
        userDb.Email = user.Email;
        userDb.Gender = user.Gender;
        userDb.Active = user.Active;

        _userRepository.AddEntity<User>(userDb);
        if (_userRepository.SaveChanges())
        {
            return Created();
            // return Ok("User added successfully!");
        }
        throw new Exception("Failed to add user!");
    }

    [HttpPut("EditUser")]
    public IActionResult EditUserEF(User user)
    {
        User? userDb = _userRepository.GetSingleUserEF(user.UserId);

        if (userDb != null)
        {
            userDb.FirstName = user.FirstName;
            userDb.LastName = user.LastName;
            userDb.Email = user.Email;
            userDb.Gender = user.Gender;
            userDb.Active = user.Active;
            if (_userRepository.SaveChanges())
            {
                return Ok("User updated successfully!");
            }
            return NotFound("User not found");
            // throw new Exception("Failed to update user!");
        }
        throw new Exception("User not found");
    }

    [HttpDelete("DeleteUser/{userId}")]
    public IActionResult DeleteUserByIdEF(int userId)
    {
        User? userDb = _userRepository.GetSingleUserEF(userId);

        if (userDb != null)
        {
            _userRepository.RemoveEntity<User>(userDb);
            if (_userRepository.SaveChanges())
            {
                return Ok("User deleted successfully!");
            }
            throw new Exception("Failed to delete user!");
        }
        throw new Exception("User not found");
    }

    [HttpGet("UserSalary/{userId}")]
    public UserSalary GetUserSalaryEF(int userId)
    {
        return _userRepository.GetUserSalaryEF(userId);
    }

    [HttpPost("UserSalary")]
    public IActionResult PostUserSalaryEf(UserSalary userForInsert)
    {
        _userRepository.AddEntity<UserSalary>(userForInsert);
        if (_userRepository.SaveChanges())
        {
            return Ok();
        }
        throw new Exception("Adding UserSalary failed on save");
    }


    [HttpPut("UserSalary")]
    public IActionResult PutUserSalaryEf(UserSalary userForUpdate)
    {
        UserSalary? userToUpdate = _userRepository.GetUserSalaryEF(userForUpdate.UserId);

        if (userToUpdate != null)
        {
            _mapper.Map(userForUpdate, userToUpdate);
            if (_userRepository.SaveChanges())
            {
                return Ok("User's salary updated successfully!");
            }
            throw new Exception("Failed to Update user's salary");
        }
        return NotFound("User not found");
        // throw new Exception("Failed to find UserSalary to Update");
    }


    [HttpDelete("UserSalary/{userId}")]
    public IActionResult DeleteUserSalaryEf(int userId)
    {
        UserSalary? userToDelete = _userRepository.GetUserSalaryEF(userId);

        if (userToDelete != null)
        {
            _userRepository.RemoveEntity<UserSalary>(userToDelete);
            if (_userRepository.SaveChanges())
            {
                return Ok();
            }
            throw new Exception("Failed to Delete user's salary");
        }
        return NotFound("User not found");
        // throw new Exception("Failed to find UserSalary to delete");
    }


    [HttpGet("UserJobInfo/{userId}")]
    public UserJobInfo GetUserJobInfoEF(int userId)
    {
        return _userRepository.GetUserJobInfoEF(userId);
    }

    [HttpPost("UserJobInfo")]
    public IActionResult PostUserJobInfoEf(UserJobInfo userForInsert)
    {
        _userRepository.AddEntity<UserJobInfo>(userForInsert);
        if (_userRepository.SaveChanges())
        {
            return Ok();
        }
        throw new Exception("Adding UserJobInfo failed on save");
    }


    [HttpPut("UserJobInfo")]
    public IActionResult PutUserJobInfoEf(UserJobInfo userForUpdate)
    {
        UserJobInfo? userToUpdate = _userRepository.GetUserJobInfoEF(userForUpdate.UserId);

        if (userToUpdate != null)
        {
            _mapper.Map(userForUpdate, userToUpdate);
            if (_userRepository.SaveChanges())
            {
                return Ok();
            }
            throw new Exception("Failed to update user's job info");
            // throw new Exception("Updating UserJobInfo failed on save");
        }
        return NotFound("User not found");
        // throw new Exception("Failed to find UserJobInfo to Update");
    }


    [HttpDelete("UserJobInfo/{userId}")]
    public IActionResult DeleteUserJobInfoEf(int userId)
    {
        UserJobInfo? userToDelete = _userRepository.GetUserJobInfoEF(userId);

        if (userToDelete != null)
        {
            _userRepository.RemoveEntity<UserJobInfo>(userToDelete);
            if (_userRepository.SaveChanges())
            {
                return Ok();
            }
            throw new Exception("Failed to delete user's job info");
        }
        return NotFound("User not found");
        // throw new Exception("Failed to find UserJobInfo to delete");
    }

}
