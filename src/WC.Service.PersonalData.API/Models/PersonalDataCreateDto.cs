using System.ComponentModel.DataAnnotations;

namespace WC.Service.PersonalData.API.Models;

public class PersonalDataCreateDto
{
    [Required]
    public required Guid EmployeeId { get; set; }

    [Required]
    public required string Email { get; set; }

    [Required]
    public required string Password { get; set; }
}
