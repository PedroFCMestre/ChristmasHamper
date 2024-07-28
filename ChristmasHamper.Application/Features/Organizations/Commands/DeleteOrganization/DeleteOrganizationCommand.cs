using FluentResults;
using MediatR;

namespace ChristmasHamper.Application.Features.Organizations.Commands.DeleteOrganization;

public record DeleteOrganizationCommand(int id) : IRequest<Result<Unit>>
{
    public int Id { get; set; } = id;
}

