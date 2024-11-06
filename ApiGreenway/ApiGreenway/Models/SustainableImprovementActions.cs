using ApiGreenway.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiGreenway.Models;

[Table("T_GRW_SUSTAINABLE_IMPROVEMENT_ACTIONS")]
public class SustainableImprovementActions
{
    [Key]
    public int id_sustainable_improvement_action { get; set; }


    public required string ds_name { get; set; }
    public required string tx_instruction { get; set; }

    [StringLength(1)]
    public required StatusProcess st_sustainable_action { get; set; }
    public required int nr_priority { get; set; }

    public DateTimeOffset dt_created_at { get; set; } = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília

    public DateTimeOffset? dt_updated_at { get; set; }

    public DateTimeOffset? dt_finished_at { get; set; }

    // Relationships
    public int? id_sustainable_goal { get; set; }
}
