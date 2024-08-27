using System.ComponentModel.DataAnnotations;

namespace SharedLibrary.Dtos;

public record class UpdateTestDto
(
    [Required]
    Guid PatientId,
    [Required]
    Guid DoctorId,
    [Required]
    string Name,
    [Required]
    float Value,
    [Required]
    float MinValue,
    [Required]
    float MaxValue,
    [Required]
    string UMeasurement

    );
