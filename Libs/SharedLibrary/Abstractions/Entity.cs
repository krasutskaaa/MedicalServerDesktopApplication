using System.ComponentModel.DataAnnotations;

namespace SharedLibrary.Abstractions;

public abstract class Entity
{

    [Key]
    public Guid Id { get; init; } = Guid.NewGuid();
    public DateTime CreatedAt { get; init; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
}
