using DVDVault.Domain.Enums;
using DVDVault.Shared.Entities;
using DVDVault.Shared.Extensions;
using System.Xml.Linq;
using Errors = System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, string>>;

namespace DVDVault.Domain.Models;
public class DVD : Entity
{
    public DVD() { }

    public DVD(string title, string genre, DateOnly published, int copies, int directorId)
    {
        Title = title;
        Genre = genre;
        Copies = copies;
        Published = published;
        Available = true;
        DirectorId = directorId;
        CreatedAt = DateTime.Now;
    }

    public void Validate()
    {
        var errors = new Errors();
        errors.AddRange(this.CheckIfPropertiesIsNull());
        if (errors.Count > 0 )
        {
            AddNotification(errors);
        }
    }

    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Genre { get; set; } = null!;

    public DateOnly Published { get; set; }

    public int Copies { get; set; }

    public bool Available { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public int DirectorId { get; set; }

    public Director? Director { get; set; }

    public void UpdateTitle(string title)
    {
        Title = title;
        UpdatedAt = DateTime.Now;
        Validate();
    }

    public void UpdateGenre(string genre)
    {
        Genre = genre;
        UpdatedAt = DateTime.Now;
        Validate();
    }

    public void SoftDelete()
    {
        Available = false;
        DeletedAt = DateTime.Now;
        Validate();
    }

    public void Activate()
    {
        Available = true;
        DeletedAt = null;
        Validate();
    }

    public void RentCopy()
    {
        Copies -= 1;
    }

    public void ReturnCopy()
    {
        Copies += 1;
    }
}
