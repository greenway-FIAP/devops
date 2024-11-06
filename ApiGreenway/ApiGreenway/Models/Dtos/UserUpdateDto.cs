using System.Text.Json.Serialization;

namespace ApiGreenway.Models.Dtos;

public class UserUpdateDto
{
    [JsonIgnore]
    public int id_user { get; set; }
    public string ds_email { get; set; }
    public string? ds_password { get; set; }
}
