using AutoMapper;
using ChristmasHamper.Application.Features.Organizations;
using ChristmasHamper.Application.Features.Organizations.Commands.CreateOrganization;
using ChristmasHamper.Application.Features.Organizations.Commands.UpdateOrganization;
using ChristmasHamper.Domain.Entities;

namespace ChristmasHamper.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Organization, OrganizationDto>().ReverseMap();
        CreateMap<Organization, CreateOrganizationCommand>().ReverseMap();
        CreateMap<Organization, UpdateOrganizationCommand>().ReverseMap();
    }
}

