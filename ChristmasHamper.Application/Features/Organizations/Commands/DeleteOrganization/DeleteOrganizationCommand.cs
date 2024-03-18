using ChristmasHamper.Application.Responses;
using MediatR;

namespace ChristmasHamper.Application.Features.Organizations.Commands.DeleteOrganization;

public class DeleteOrganizationCommand(int id) : IRequest<BaseResponse>
{
    public int Id { get; set; } = id;
}

