using AutoMapper;
using ChristmasHamper.Application.Features.Organizations;
using ChristmasHamper.Domain.Entities;

namespace ChristmasHamper.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Organization, OrganizationDto>().ReverseMap();
    }
}

