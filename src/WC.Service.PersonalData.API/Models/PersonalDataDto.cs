﻿using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using WC.Library.Web.Models;
using WC.Service.PersonalData.Shared.Models;

namespace WC.Service.PersonalData.API.Models;

public class PersonalDataDto : DtoBase
{
    [Required]
    public required Guid EmployeeId { get; set; }

    [Required]
    public required string Email { get; set; }

    [Required]
    public required string Password { get; set; }

    [Required]
    [JsonConverter(typeof(StringEnumConverter))]
    public UserRole Role { get; set; }
}
