using SharedLibrary.Abstractions.Entities;
using SharedLibrary.Dtos;

namespace SharedLibrary.Mapping;

public static class TestMapping
{

    public static TestGeneralInfo ToTestGeneralInfo(this Test test)
    {
        return new(
            test.PatientId,
            test.DoctorId,
            test.Name,
            test.Value,
            test.MinValue,
            test.MaxValue,
            test.UMeasurement
            );
    }

    public static Test ToEntity(this CreateTestDto test)
    {
        return new Test()
        {
            PatientId = test.PatientId,
            DoctorId = test.DoctorId,
            Name = test.Name,
            Value = test.Value,
            MinValue = test.MinValue,
            MaxValue = test.MaxValue,
            UMeasurement = test.UMeasurement
        };
    }


    public static TestFullInfo ToTestFullInfo(this Test test)
    {
        return new(
            test.Id,
            test.PatientId,
            test.DoctorId,
            test.Name,
            test.Value,
            test.MinValue,
            test.MaxValue,
            test.UMeasurement,
            test.CreatedAt,
            test.UpdatedAt
            );
    }


    public static Test ToEntity(this UpdateTestDto test, Guid id)
    {
        return new Test()
        {
            Id = id,
            PatientId = test.PatientId,
            DoctorId = test.DoctorId,
            Name = test.Name,
            Value = test.Value,
            MinValue = test.MinValue,
            MaxValue = test.MaxValue,
            UMeasurement= test.UMeasurement,
            UpdatedAt = DateTime.Now
        };
    }
}
