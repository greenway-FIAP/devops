using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiGreenway.Models;


[Table("T_GRW_MEASUREMENT_TYPE")]
public class MeasurementType
{
    [Key]
    public int id_measurement_type { get; set; }


    public required int ds_name { get; set; }
    public required string tx_description { get; set; }

    public DateTimeOffset dt_created_at { get; set; } = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília

    public DateTimeOffset? dt_updated_at { get; set; }

    public DateTimeOffset? dt_finished_at { get; set; }

}
