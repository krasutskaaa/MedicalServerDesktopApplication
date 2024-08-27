namespace SharedLibrary.Dtos;

public record class TestGeneralInfo
(
    Guid PatientId,
    Guid DoctorId,
    string Name,
    float Value,
    float MinValue,
    float MaxValue,
    string UMeasurement
    );
