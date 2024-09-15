using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiGreenway.Models;

[Table("T_GRW_COMPANY_REPRESENTATIVE")]
public class CompanyRepresentative
{
    [Key]
    public int id_company_representative { get; set; }

    public required string ds_name { get; set; }
    public required string ds_role { get; set; }
    
    [StringLength(11)]
    public required string nr_phone { get; set; }

    [JsonIgnore]
    public DateTimeOffset dt_created_at { get; set; } = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília

    [JsonIgnore]
    public DateTimeOffset? dt_updated_at { get; set; }

    [JsonIgnore]
    public DateTimeOffset? dt_finished_at { get; set; }

    // Relationships
    public int? id_user { get; set; }
    public int? id_company { get; set; }

}
