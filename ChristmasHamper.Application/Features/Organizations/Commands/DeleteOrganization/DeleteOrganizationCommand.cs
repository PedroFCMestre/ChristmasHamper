using FluentResults;
using MediatR;

namespace ChristmasHamper.Application.Features.Organizations.Commands.DeleteOrganization;

public class DeleteOrganizationCommand(int id) : IRequest<Result>
{
    public int Id { get; set; } = id;
}

