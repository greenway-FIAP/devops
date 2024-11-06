using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiGreenway.Models;

[Table("T_GRW_ADDRESS")]
public class Address
{
    [Key]
    public int id_address { get; set; }


    public required string ds_street { get; set; }

    [StringLength(8)]
    public required string ds_zip_code { get; set; }

    public required string ds_number { get; set; }

    [StringLength(2)]
    public required string ds_uf { get; set; }
    public required string ds_neighborhood { get; set; }
    public required string ds_city { get; set; }

    public DateTimeOffset dt_created_at { get; set; } = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília

    public DateTimeOffset? dt_updated_at { get; set; }

    public DateTimeOffset? dt_finished_at { get; set; }

    // Relationships
    public int? id_company { get; set; }
}
