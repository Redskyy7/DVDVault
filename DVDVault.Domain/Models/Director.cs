using DVDVault.Shared.Attributes;
using DVDVault.Shared.Entities;
using DVDVault.Domain.Interfaces.Abstractions;
using Errors = System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, string>>;
using DVDVault.Shared.Extensions;

namespace DVDVault.Domain.Models;
public class Director : Entity, IValidate
{
    public Director() {}
    public Director(string name, string surname)
    {
        Name = name;
        Surname = surname;
        CreatedAt = DateTime.Now;
        Validate();
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

    [IfNull(ErrorMessage = "Invalid Name.")]
    public string Name { get; set; } = null!;

    [IfNull(ErrorMessage = "Invalid Surname.")]
    public string Surname { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public ICollection<DVD>? DVDs { get; set; }

    public string GetFullName()
    {
        return $"{Name} {Surname}";
    }
}

