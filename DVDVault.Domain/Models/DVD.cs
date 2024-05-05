using DVDVault.Domain.Enums;
using DVDVault.Shared.Entities;

namespace DVDVault.Domain.Models;
public class DVD : Entity
{

    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public GenreEnum Genre { get; set; }

    public DateTime Published { get; set; }

    public int Copies { get; set; }

    public bool Available { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public int DirectorId { get; set; }

    public Director? Director { get; set; }
}
