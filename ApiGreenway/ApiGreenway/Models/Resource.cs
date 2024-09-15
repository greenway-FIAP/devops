using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiGreenway.Models;

[Table("T_GRW_RESOURCE")]
public class Resource
{
    [Key]
    public int id_resource { get; set; }


    public required string ds_name { get; set; }
    public required string tx_description { get; set; }
    public required double vl_cost_per_unity { get; set; }
    public required string ds_unit_of_measurement { get; set; }
    public required double nr_availability { get; set; }

    [JsonIgnore]
    public DateTimeOffset dt_created_at { get; set; } = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília

    [JsonIgnore]
    public DateTimeOffset? dt_updated_at { get; set; }

    [JsonIgnore]
    public DateTimeOffset? dt_finished_at { get; set; }

    // Relationships
    public int? id_resource_type { get; set; }
}
