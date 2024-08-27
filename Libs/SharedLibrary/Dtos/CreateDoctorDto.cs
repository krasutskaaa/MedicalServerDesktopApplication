
using System.ComponentModel.DataAnnotations;

namespace SharedLibrary.Dtos;

public record class CreateDoctorDto(
    [Required][StringLength(50)]
    string Name,
    [Required]
    string Gender,
    [Required][RegularExpression(@"^[A-Za-z0-9]+$",
    ErrorMessage =
    "The username must have at least one of the charachters(A-Z,a-z,0-9)")]
    string Username,
    [Required][MinLength(8,
    ErrorMessage = "The field must be al least 8 charachters")]
    string Password,
    [Required]
    string Specialization

);
