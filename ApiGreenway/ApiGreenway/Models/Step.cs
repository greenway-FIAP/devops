using ApiGreenway.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiGreenway.Models;

[Table("T_GRW_STEP")]
public class Step
{
    [Key]
    public int id_step { get; set; }


    public required string ds_name { get; set; }
    public required string tx_description { get; set; }
    public required double nr_estimated_time { get; set; }

    [StringLength(1)]
    public required StatusProcess st_step { get; set; }
    public DateOnly dt_start { get; set; }
    public DateOnly dt_end { get; set; }

    public DateTimeOffset dt_created_at { get; set; } = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília

    public DateTimeOffset? dt_updated_at { get; set; }

    public DateTimeOffset? dt_finished_at { get; set; }
}
