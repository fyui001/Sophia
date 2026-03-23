namespace Sophia.Api.Requests.User;

using System.ComponentModel.DataAnnotations;

public record RegisterUserRequest
{
    [Required]
    [MaxLength(255)]
    public required string Name { get; init; }
}
