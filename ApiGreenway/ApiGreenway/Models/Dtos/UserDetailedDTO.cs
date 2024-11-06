using System.Text.Json.Serialization;

namespace ApiGreenway.Models.Dtos;

public class UserDetailedDTO
{
    public int id_user { get; set; }
    public string? ds_email { get; set; }
}
