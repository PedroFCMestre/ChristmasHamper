using FluentResults;
using MediatR;

namespace ChristmasHamper.Application.Features.Organizations.Queries.GetOrganization;

public record GetOrganizationQuery(int Id) : IRequest<Result<GetOrganizationQueryResponse>>;

