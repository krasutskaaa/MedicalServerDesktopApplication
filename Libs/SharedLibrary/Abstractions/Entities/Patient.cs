using static System.Net.Mime.MediaTypeNames;

namespace SharedLibrary.Abstractions.Entities;

public class Patient : Entity, IAliveEntity
{
    public string Name { get; set; }
    public string Gender { get; set; }
    public DateOnly Birthday { get; set; }
    public string? Description { get; set; }


}
