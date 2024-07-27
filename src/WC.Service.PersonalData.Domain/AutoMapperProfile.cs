using AutoMapper;
using WC.Service.PersonalData.Data.Models;
using WC.Service.PersonalData.Domain.Models;

namespace WC.Service.PersonalData.Domain;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<PersonalDataModel, PersonalDataEntity>()
            .ForMember(dest => dest.Role,
                opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.Role) ? "User" : src.Role));

        CreateMap<PersonalDataEntity, PersonalDataModel>();
    }
}
