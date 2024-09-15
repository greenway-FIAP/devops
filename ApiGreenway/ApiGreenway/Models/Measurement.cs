using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiGreenway.Models;

[Table("T_GRW_MEASUREMENT")]
public class Measurement
{
    [Key]
    public int id_measurement { get; set; }


    public required string ds_name { get; set; }
    public required string tx_description { get; set; }

    [JsonIgnore]
    public DateTimeOffset dt_created_at { get; set; } = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília

    [JsonIgnore]
    public DateTimeOffset? dt_updated_at { get; set; }

    [JsonIgnore]
    public DateTimeOffset? dt_finished_at { get; set; }

    // Relationships
    public int? id_measurement_type { get; set; }
    public int? id_improvement_measurement { get; set; }
    public int? id_sustainable_goal { get; set; }
}
