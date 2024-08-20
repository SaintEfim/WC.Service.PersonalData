using System.ComponentModel.DataAnnotations;
using WC.Service.PersonalData.Shared.Models;

namespace WC.Service.PersonalData.API.Models;

public class PersonalDataUpdateDto
{
    [Required]
    public required string Email { get; set; }

    [Required]
    public required string Password { get; set; }

    [Required]
    public required UserRole Role { get; set; }
}
