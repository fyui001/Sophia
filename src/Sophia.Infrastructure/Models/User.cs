namespace Sophia.Infrastructure.Models;

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

public enum UserStatus
{
    Valid,
    Invalid,
}

[Index(nameof(Auth0Sub), IsUnique = true)]
public class User
{
    [Key]
    public long Id { get; set; }

    [Comment("Auth0 sub")]
    [MaxLength(255)]
    public required string Auth0Sub { get; set; }

    [Comment("名前")]
    [MaxLength(255)]
    public required string Name { get; set; }

    [Comment("アイコンURL")]
    [MaxLength(255)]
    public required string IconUrl { get; set; }

    [Comment("Discord User ID")]
    [MaxLength(255)]
    public string? DiscordUserId { get; set; }

    [Comment("ステータス")]
    [MaxLength(255)]
    public UserStatus Status { get; set; } = UserStatus.Valid;

    [Comment("作成日時")]
    public required DateTime CreatedAt { get; set; }

    [Comment("更新日時")]
    public required DateTime UpdatedAt { get; set; }
}
