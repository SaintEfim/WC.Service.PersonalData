using Microsoft.Extensions.Options;
using Sieve.Models;
using Sieve.Services;
using WC.Service.PersonalData.Data.Models;

namespace WC.Service.PersonalData.Data.Profile;

public class PersonalDataEntityFilterProfile : SieveProcessor
{
    public PersonalDataEntityFilterProfile(
        IOptions<SieveOptions> options)
        : base(options)
    {
    }

    protected override SievePropertyMapper MapProperties(
        SievePropertyMapper mapper)
    {
        mapper.Property<PersonalDataEntity>(p => p.Id)
            .CanFilter();

        mapper.Property<PersonalDataEntity>(p => p.EmployeeId)
            .CanFilter();

        mapper.Property<PersonalDataEntity>(p => p.Email)
            .CanFilter()
            .CanSort();

        mapper.Property<PersonalDataEntity>(p => p.Password)
            .CanFilter()
            .CanSort();

        return mapper;
    }
}
