using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Sophia.Api.Models;

[Index(nameof(DiscordUserId), IsUnique = true)]
public class User
{
    [Key]
    public ulong Id { get; set; }
    
    [Comment("ユーザーID")]
    public ulong DiscordUserId { get; set; }
    
    [Comment("名前")]
    [Column(TypeName = "varchar(255)")]
    public required string Name { get; set; }
    
    [Comment("アイコンURL")]
    [Column(TypeName = "varchar(255)")]
    public required string IconUrl { get; set; }
    
    [Comment("パスワード")]
    [Column(TypeName = "varchar(255)")]
    public required string Password { get; set; }
    
    [Comment("ステータス")]
    public bool Status { get; set; }
    
    [Comment("作成日時")]
    public required DateTime CreatedAt { get; set; }
    
    [Comment("更新日時")]
    public required DateTime UpdatedAt { get; set; }
    
}