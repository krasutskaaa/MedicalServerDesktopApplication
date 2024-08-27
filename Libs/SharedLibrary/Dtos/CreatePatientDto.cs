
using System.ComponentModel.DataAnnotations;

namespace SharedLibrary.Dtos;

public record class CreatePatientDto(
    [Required][StringLength(50)]
    string Name,
    [Required]
    string Gender,
    [Required]
    DateOnly Birthday,
    string? Description

);
