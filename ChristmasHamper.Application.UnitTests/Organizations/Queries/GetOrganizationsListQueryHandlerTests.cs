using AutoMapper;
using ChristmasHamper.Application.Contracts.Persistence;
using ChristmasHamper.Application.Features.Organizations;
using ChristmasHamper.Application.Features.Organizations.Queries.GetOrganizationsList;
using ChristmasHamper.Application.Profiles;
using ChristmasHamper.Application.UnitTests.Mocks;
using ChristmasHamper.Domain.Entities;
using Moq;
using Shouldly;

namespace ChristmasHamper.Application.UnitTests.Organizations.Queries;

public class GetOrganizationsListQueryHandlerTests
{
    private readonly Mock<IOrganizationRepository> _mockOrganizationRepository;
    private readonly IMapper _mapper;

    public GetOrganizationsListQueryHandlerTests()
    {
        _mockOrganizationRepository = RepositoryMocks.GetOrganizationRepository();

        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        _mapper = configurationProvider.CreateMapper();
    }

    [Fact]
    public async Task GetOrganizationsListTest()
    {
        var handler = new GetOrganizationsListQueryHandler(_mockOrganizationRepository.Object, _mapper);

        var result = await handler.Handle(new GetOrganizationsListQuery(), CancellationToken.None);

        result.ShouldBeOfType<List<OrganizationDto>>();

        result.Count.ShouldBe(3);
    }
}

