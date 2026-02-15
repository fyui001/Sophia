namespace Sophia.Infrastructure.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

public enum Role
{
    System,
    Operator,
}

public enum AdminUserStatus
{
    Valid,
    Invalid,
}

public class AdminUser
{
    [Key]
    public ulong Id { get; set; }

    [Comment("メールアドレス")]
    [Column(TypeName = "varchar(255)")]
    public string? Emai { get; set; }

    [Comment("名前")]
    [Column(TypeName = "varchar(255)")]
    public required string Name { get; set; }

    [Comment("ロール")]
    [Column(TypeName = "varchar(255)")]
    public required Role Role { get; set; }

    [Comment("ステータス")]
    [Column(TypeName = "varchar(255)")]
    public AdminUserStatus Status { get; set; } = AdminUserStatus.Valid;

    [Comment("作成日時")]
    public required DateTime CreatedAt { get; set; }

    [Comment("更新日時")]
    public required DateTime UpdatedAt { get; set; }
}
