using SharedLibrary.Abstractions.Entities;
using SharedLibrary.Dtos;
using SharedLibrary.Encryption;

namespace SharedLibrary.Mapping;

public static class DoctorMapping
{
    public static DoctorGeneralInfo ToDoctorGeneralInfo(this Doctor doctor)
    {
        return new(
            doctor.Name,
            doctor.Gender,
            doctor.Specialization
            );
    }

    public static Doctor ToEntity(this CreateDoctorDto doctor)
    {
        return new Doctor()
        {
            Name = doctor.Name,
            Gender = doctor.Gender,
            Username = doctor.Username,
            Password = doctor.Password.GenerateSHA256(),
            Specialization = doctor.Specialization

        };
    }

    public static DoctorFullInfo ToDoctorFullInfo(this Doctor doctor)
    {
        return new(
            doctor.Id,
            doctor.Name,
            doctor.Gender,
            doctor.Username,
            doctor.Specialization,
            doctor.CreatedAt,
            doctor.UpdatedAt
            );

    }


    public static Doctor ToEntity(this UpdateDoctorDto doctor, Guid id)
    {
        return new Doctor()
        {
            Id = id,
            Name = doctor.Name,
            Gender = doctor.Gender,
            Username = doctor.Username,
            Password = doctor.Password.GenerateSHA256(),
            Specialization = doctor.Specialization,
            UpdatedAt = DateTime.Now
        };
    }
}

