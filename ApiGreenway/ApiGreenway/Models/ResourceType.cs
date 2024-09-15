using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiGreenway.Models;

[Table("T_GRW_RESOURCE_TYPE")]
public class ResourceType
{
    [Key]
    public int id_resource_type { get; set; }


    public required string ds_name { get; set; }
    public required string tx_description { get; set; }

    [JsonIgnore]
    public DateTimeOffset dt_created_at { get; set; } = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília

    [JsonIgnore]
    public DateTimeOffset? dt_updated_at { get; set; }

    [JsonIgnore]
    public DateTimeOffset? dt_finished_at { get; set; }
}
