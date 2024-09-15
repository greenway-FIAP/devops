using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiGreenway.Models;

[Table("T_GRW_COMPANY")]
public class Company
{
    [Key]
    public int id_company { get; set; }


    public required string ds_name { get; set; }
    public required string tx_description { get; set; }
    public required double vl_current_revenue { get; set; }
    public required int nr_size { get; set; }

    [StringLength(14)]
    public required string nr_cnpj { get; set; }

    [JsonIgnore]
    public DateTimeOffset dt_created_at { get; set; } = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília

    [JsonIgnore]
    public DateTimeOffset? dt_updated_at { get; set; }

    [JsonIgnore]
    public DateTimeOffset? dt_finished_at { get; set; }

    // Relationships
    public int? id_sector { get; set; }
    public int? id_address { get; set; }

}
