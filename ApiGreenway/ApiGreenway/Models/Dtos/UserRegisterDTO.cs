namespace ApiGreenway.Models.Dtos;

public class UserRegisterDTO : User
{
    public string? ds_email { get; set; }
    public string? ds_password { get; set; }
}
