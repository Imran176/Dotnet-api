// DTOs are simple objects that contain only properties. 
// They do not contain any business logic, validation rules, or database-tracking mechanisms. 

namespace DotnetApi.Dtos;

public partial class UserForRegistrationDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }

    public UserForRegistrationDto()
    {
        if(Email == null){
            Email= "";
        }
        if(Password == null){
            Password= "";
        }
        if(ConfirmPassword == null){
            ConfirmPassword= "";
        }
    }
}
