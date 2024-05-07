using DVDVault.Domain.Enums;

namespace DVDVault.Domain.DTO;
public class DVDDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Genre { get; set; } = null!;
    public DateOnly Published {  get; set; }
    public int Copies { get; set; }
    public bool Available { get; set; }
    public DateTime CreatedAt {  get; set; }
    public DateTime? UpdatedAt {  get; set; }
    public DateTime? DeletedAt {  get; set; }
    public int DirectorId {  get; set; }
}
