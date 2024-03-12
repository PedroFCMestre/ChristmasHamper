using ChristmasHamper.Application.Responses;
using MediatR;

namespace ChristmasHamper.Application.Features.Organizations.Commands.DeleteOrganization;

public class DeleteOrganizationCommand : IRequest<BaseResponse>
{
    public int OrganizationId;
}

