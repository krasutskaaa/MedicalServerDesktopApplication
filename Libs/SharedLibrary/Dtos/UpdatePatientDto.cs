using SharedLibrary.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace SharedLibrary.Dtos;

public record class UpdatePatientDto(
    [Required][StringLength(50)]
    string Name,
    [Required]
    string Gender,
    [Required]
    DateOnly Birthday,
    string? Description
);
