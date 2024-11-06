using ApiGreenway.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiGreenway.Models;

[Table("T_GRW_PROCESS")]
public class Process
{
    [Key]
    public int id_process { get; set; }

    public required string ds_name { get; set; }

    [StringLength(1)]
    public StatusProcess st_process { get; set; }
    public required int nr_priority { get; set; }
    public required DateOnly dt_start { get; set; }
    public required DateOnly dt_end { get; set; }
    public required string tx_description { get; set; }
    public string? tx_comments { get; set; }

    public DateTimeOffset dt_created_at { get; set; } = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília

    public DateTimeOffset? dt_updated_at { get; set; }

    public DateTimeOffset? dt_finished_at { get; set; }

    // Relationships
    public int? id_company { get; set; }
    public int? id_company_representative { get; set; }

}
