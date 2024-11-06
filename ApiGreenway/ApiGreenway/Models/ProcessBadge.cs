using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiGreenway.Models;

[Table("T_GRW_PROCESS_BADGE")]
public class ProcessBadge
{
    [Key]
    public int id_process_badge { get; set; }


    public required DateOnly dt_expiration_date { get; set; }
    public required string url_badge { get; set; }

    public DateTimeOffset dt_created_at { get; set; } = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília

    public DateTimeOffset? dt_updated_at { get; set; }

    public DateTimeOffset? dt_finished_at { get; set; }

    // Relationships
    public int? id_process { get; set; }
    public int? id_badge { get; set; }
}
