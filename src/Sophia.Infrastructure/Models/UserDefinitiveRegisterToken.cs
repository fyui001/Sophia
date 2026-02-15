namespace Sophia.Infrastructure.Models;

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

[Index(nameof(UserId))]
public class UserDefinitiveRegisterToken
{
    [Key]
    public long Id { get; set; }

    public long UserId { get; set; }

    [Comment("トークン")]
    public required string Token { get; set; }

    public required bool IsVerified { get; set; } = false;

    [Comment("有効期限")]
    public required DateTime ExpiredAt { get; set; }

    [Comment("作成日時")]
    public required DateTime CreatedAt { get; set; }

    [Comment("更新日時")]
    public required DateTime UpdatedAt { get; set; }
}
