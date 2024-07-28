using AutoMapper;
using ChristmasHamper.Application.Contracts.Persistence;
using ChristmasHamper.Application.Features.Organizations;
using ChristmasHamper.Application.UnitTests.Mocks;
using Moq;
using ChristmasHamper.Application.Profiles;
using ChristmasHamper.Application.Features.Organizations.Queries.GetOrganization;
using FluentAssertions;

namespace ChristmasHamper.Application.UnitTests.Organizations.Queries;

public class GetOrganizationQueryHandlerTests
{
    private readonly Mock<IOrganizationRepository> _mockOrganizationRepository;
    private readonly IMapper _mapper;

    public GetOrganizationQueryHandlerTests()
    {
        _mockOrganizationRepository = RepositoryMocks.GetOrganizationRepository();

        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        _mapper = configurationProvider.CreateMapper();
    }

    [Fact]
    public async Task WithExistentId_ReturnOrganization()
    {
        var handler = new GetOrganizationQueryHandler(_mockOrganizationRepository.Object, _mapper);
        var id = 1;

        var result = await handler.Handle(new GetOrganizationQuery(id), CancellationToken.None);

        result.Value.Should().BeOfType<GetOrganizationQueryResponse>();
        result.Value.Id.Should().Be(id);
    }

    [Fact]
    public async Task WithUnexistentId_ReturnNull()
    {
        var handler = new GetOrganizationQueryHandler(_mockOrganizationRepository.Object, _mapper);
        var id = 99;

        var result = await handler.Handle(new GetOrganizationQuery(id), CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
    }
}
