// DTOs are simple objects that contain only properties. 
// They do not contain any business logic, validation rules, or database-tracking mechanisms. 

namespace DotnetApi.Dtos;

public partial class UserForLoginDto
{
    public string Email { get; set; }
    public string Password { get; set; }

    public UserForLoginDto()
    {
        if(Email == null){
            Email= "";
        }
        if(Password == null){
            Password= "";
        }
    }
}
