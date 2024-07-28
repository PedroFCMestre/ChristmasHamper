using FluentResults;
using MediatR;

namespace ChristmasHamper.Application.Features.Organizations.Queries.GetOrganizationsList;

public record GetOrganizationsListQuery : IRequest<Result<List<GetOrganizationsListQueryResponse>>>;
