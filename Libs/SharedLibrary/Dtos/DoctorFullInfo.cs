

namespace SharedLibrary.Dtos;

public record class DoctorFullInfo(

    Guid Id,
    string Name,
    string Gender,
    string Username,
    string Specialization,
    DateTime CreatedAt,
    DateTime? UpdatedAt
    );
