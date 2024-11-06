using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiGreenway.Models;

/// <summary>
/// Tabela de usuários
/// </summary>
[Table("T_GRW_USER")]
public class User
{
    /// <summary>
    /// Identificador do usuário
    /// </summary>
    [Key]
    [JsonIgnore]
    public int id_user { get; set; }

    /// <summary>
    /// Identificador único do usuário no Facebook
    /// </summary>
    [JsonIgnore]
    public string? ds_uid_fb { get; set; }

    /// <summary>
    /// E-mail do usuário
    /// </summary>
    public string? ds_email { get; set; }

    /// <summary>
    /// Senha do usuário
    /// </summary>
    public string? ds_password { get; set; }

    /// <summary>
    /// Data e hora de criação do usuário
    /// </summary>
    [JsonIgnore]
    public DateTimeOffset dt_created_at { get; set; } = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília

    /// <summary>
    /// Data e hora de atualização do usuário
    /// </summary>
    [JsonIgnore]
    public DateTimeOffset? dt_updated_at { get; set; }

    /// <summary>
    /// Data e hora de finalização do usuário
    /// </summary>
    [JsonIgnore]
    public DateTimeOffset? dt_finished_at { get; set; }

    // Relationships

    /// <summary>
    /// Identificador do tipo de usuário
    /// </summary>
    public int? id_user_type { get; set; }

    /// <summary>
    /// Identificador do Cadastro deste Usuário
    /// </summary>
    public int? id_company_representative { get; set; }
}
