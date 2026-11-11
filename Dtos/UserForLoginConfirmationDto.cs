// DTOs are simple objects that contain only properties. 
// They do not contain any business logic, validation rules, or database-tracking mechanisms. 

namespace DotnetApi.Dtos;

public partial class UserForLoginConfirmationDto
{
    byte[] PasswordHash { get; set; }
    byte[] PasswordSalt { get; set; }

    public UserForLoginConfirmationDto()
    {
        if(PasswordHash == null){
            PasswordHash= new byte[0];
        }
        if(PasswordSalt == null){
            PasswordSalt= new byte[0];
        }
    }
}
