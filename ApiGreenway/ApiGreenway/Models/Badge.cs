using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using ApiGreenway.Enums;

namespace ApiGreenway.Models;

[Table("T_GRW_BADGE")]
public class Badge
{
    [Key]
    public int id_badge { get; set; }


    public required string ds_name { get; set; }
    public required string tx_description { get; set; }
    public required string ds_criteria { get; set; }

    [StringLength(1)]
    public required StatusProcess st_badge { get; set; }
    public required string url_image { get; set; }

    // Não existe no banco relacionnal, verificar se coloco mesmo.
    public DateTimeOffset dt_created_at { get; set; } = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília

    public DateTimeOffset? dt_updated_at { get; set; }

    public DateTimeOffset? dt_finished_at { get; set; }

    // Relationships
    public int? id_badge_level { get; set; }
    public int? id_sustainable_goal { get; set; }
}
