namespace SharedLibrary.Dtos;

public record class TestFullInfo
(
    Guid Id,
    Guid PatientId,
    Guid DoctorId,
    string Name,
    float Value,
    float MinValue,
    float MaxValue,
    string UMeasurement,
    DateTime CreatedAt,
    DateTime? UpdatedAt
    );
