namespace Sophia.Infrastructure.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

public enum UserStatus
{
    Valid,
    Invalid,
}

[Index(nameof(Emai), IsUnique = true)]
public class User
{
    [Key]
    public ulong Id { get; set; }

    [Comment("メールアドレス")]
    [Column(TypeName = "varchar(255)")]
    public string? Emai { get; set; }

    [Comment("名前")]
    [Column(TypeName = "varchar(255)")]
    public required string Name { get; set; }

    [Comment("アイコンURL")]
    [Column(TypeName = "varchar(255)")]
    public required string IconUrl { get; set; }

    [Comment("ステータス")]
    [Column(TypeName = "varchar(255)")]
    public UserStatus Status { get; set; } = UserStatus.Valid;

    [Comment("作成日時")]
    public required DateTime CreatedAt { get; set; }

    [Comment("更新日時")]
    public required DateTime UpdatedAt { get; set; }
}
