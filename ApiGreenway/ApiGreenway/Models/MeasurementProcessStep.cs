using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiGreenway.Models;


[Table("T_GRW_MEASUREMENT_PROCESS_STEP")]
public class MeasurementProcessStep
{
    [Key]
    public int id_measurement_process_step { get; set; }

    public required double nr_result { get; set; }

    public DateTimeOffset dt_created_at { get; set; } = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília

    public DateTimeOffset? dt_updated_at { get; set; }

    public DateTimeOffset? dt_finished_at { get; set; }

    // Relationships
    public int? id_measurement { get; set; }
    public int? id_process_step { get; set; }
}
