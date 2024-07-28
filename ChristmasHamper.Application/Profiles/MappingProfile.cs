using AutoMapper;
using ChristmasHamper.Application.Features.Organizations;
using ChristmasHamper.Application.Features.Organizations.Commands.CreateOrganization;
using ChristmasHamper.Application.Features.Organizations.Commands.UpdateOrganization;
using ChristmasHamper.Application.Features.Organizations.Queries.GetOrganization;
using ChristmasHamper.Application.Features.Organizations.Queries.GetOrganizationsList;
using ChristmasHamper.Domain.Entities;

namespace ChristmasHamper.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateOrganizationCommand, Organization>();
        CreateMap<UpdateOrganizationCommand, Organization>();

        CreateMap<Organization, GetOrganizationQueryResponse>();
        CreateMap<Organization, GetOrganizationsListQueryResponse>();
        CreateMap<Organization, CreateOrganizationCommandResponse>();
    }
}

