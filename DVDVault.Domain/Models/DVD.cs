using DVDVault.Domain.Enums;

namespace DVDVault.Domain.Models;
public class DVD
{
    public int ID { get; set; }

    public string Title { get; set; } = null!;

    public GenreEnum Genre { get; set; }

    public DateTime Published { get; set; }

    public int Copies { get; set; }

    public bool Available { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
}
