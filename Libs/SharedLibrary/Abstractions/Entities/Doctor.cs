using static System.Net.Mime.MediaTypeNames;

namespace SharedLibrary.Abstractions.Entities;

public class Doctor : Entity, IAliveEntity
{
    public string Name { get; set; }
    public string Gender { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Specialization { get; set; }
    public ICollection<Test> Tests { get; set; } = new List<Test>();
}



