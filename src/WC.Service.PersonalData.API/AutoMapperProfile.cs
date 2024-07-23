using AutoMapper;
using WC.Library.Web.Models;
using WC.Service.PersonalData.API.Models;
using WC.Service.PersonalData.Domain.Models;

namespace WC.Service.PersonalData.API;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<PersonalDataModel, PersonalDataDto>();

        CreateMap<PersonalDataCreateDto, PersonalDataModel>();

        CreateMap<PersonalDataModel, CreateActionResultDto>();
    }
}
