

namespace SharedLibrary.Dtos;

public record class PatientGeneralInfo(
    string Name,
    string Gender,
    DateOnly Birthday,
    string? Description
    );
