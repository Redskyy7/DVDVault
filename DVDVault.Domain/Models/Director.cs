using System.Runtime.Serialization;

namespace DVDVault.Domain.Models;
public class Director
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime DeletedAt { get; set; }

}

