using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiGreenway.Models;

[Table("T_GRW_USER")]
public class User
{
    [Key]
    public int id_user { get; set; }

    
    public string ds_email { get; set; } = string.Empty;
    public string ds_password { get; set; } = string.Empty;

    [JsonIgnore]
    public DateTimeOffset dt_created_at { get; set; } = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília

    [JsonIgnore]
    public DateTimeOffset? dt_updated_at { get; set; }

    [JsonIgnore]
    public DateTimeOffset? dt_finished_at { get; set; }

    // Relationships
    public int? id_user_type { get; set; }
    public int? id_company_representative { get; set; }
}
