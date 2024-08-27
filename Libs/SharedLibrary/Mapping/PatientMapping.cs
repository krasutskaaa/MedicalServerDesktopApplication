using SharedLibrary.Abstractions.Entities;
using SharedLibrary.Dtos;

namespace SharedLibrary.Mapping;

public static class PatientMapping
{

    public static PatientGeneralInfo ToPatientGeneralInfo(this Patient patient)
    {
        return new(
            patient.Name,
            patient.Gender,
            patient.Birthday,
            patient.Description
            );
    }
    public static Patient ToEntity(this CreatePatientDto patient)
    {
        return new Patient()
        {
            Name = patient.Name,
            Gender = patient.Gender,
            Birthday = patient.Birthday,
            Description = patient.Description


        };
    }
    public static PatientFullInfo ToPatientFullInfo(this Patient patient)
    {
        return new(
            patient.Id,
            patient.Name,
            patient.Gender,
            patient.Birthday,
            patient.Description,
            patient.CreatedAt,
            patient.UpdatedAt
            );

    }
    public static Patient ToEntity(this UpdatePatientDto patient, Guid id)
    {
        return new Patient()
        {
            Id = id,
            Name = patient.Name,
            Gender = patient.Gender,
            Birthday = patient.Birthday,
            Description = patient.Description,
            UpdatedAt = DateTime.Now
        };
    }
}

