using System.ComponentModel.DataAnnotations;

namespace DVDVault.Shared.Attributes;
public class IfNullAttribute : ValidationAttribute
{
    public string GetErrorMessage()
    {
        return ErrorMessage!;
    }
}
