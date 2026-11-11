// DTOs are simple objects that contain only properties. 
// They do not contain any business logic, validation rules, or database-tracking mechanisms. 

namespace DotnetApi.Dtos;

public partial class UserToAddDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Gender { get; set; }
    public bool Active { get; set; }

    public UserToAddDto()
    {
        if(FirstName == null)
        {
            FirstName= "";
        }
        if(LastName == null){
            LastName= "";
        }
        if(Email == null){
            Email= "";
        }
        if(Gender == null){
            Gender= "";
        }
    }
}
