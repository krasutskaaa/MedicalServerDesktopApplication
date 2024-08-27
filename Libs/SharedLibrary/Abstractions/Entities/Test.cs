using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace SharedLibrary.Abstractions.Entities;

public class Test : Entity
{
    public Guid PatientId { get; set; }
    public Guid DoctorId { get; set; }
    public string Name { get; set; }
    public float Value { get; set; }
    public float MinValue { get; set; }
    public float MaxValue { get; set; }
    public string UMeasurement { get; set; }
    public Doctor Doctor { get; set; }
    public Patient Patient { get; set; }


}
