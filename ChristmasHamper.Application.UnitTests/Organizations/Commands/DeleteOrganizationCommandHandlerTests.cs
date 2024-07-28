using ChristmasHamper.Application.Contracts.Persistence;
using ChristmasHamper.Application.Features.Organizations.Commands.DeleteOrganization;
using ChristmasHamper.Application.UnitTests.Mocks;
using FluentAssertions;
using Moq;

namespace ChristmasHamper.Application.UnitTests.Organizations.Commands;

public class DeleteOrganizationCommandHandlerTests
{
    private readonly Mock<IOrganizationRepository> _mockOrganizationRepository;

    public DeleteOrganizationCommandHandlerTests()
    {
        _mockOrganizationRepository = RepositoryMocks.GetOrganizationRepository();
    }

    [Fact]
    public async Task DeleteOrganization_WithValidId_DeletesSuccessfully()
    {
        var handler = new DeleteOrganizationCommandHandler(_mockOrganizationRepository.Object);
        var id = 1;

        var command = new DeleteOrganizationCommand(id);
        var result = await handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();

        var allOrganizations = await _mockOrganizationRepository.Object.ListAllAsync();
        allOrganizations.Count.Should().Be(2);
    }

    [Fact]
    public async Task WithInvalidId_FailWithOneValidationError()
    {
        var handler = new DeleteOrganizationCommandHandler(_mockOrganizationRepository.Object);
        var id = 99;
        var validationError = "ID provided does not exist.";

        var command = new DeleteOrganizationCommand(id);
        var result = await handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
        result.Errors!.Count.Should().Be(1);

        result.Errors.FirstOrDefault()!.Message.Should().Be(validationError);

        var allOrganizations = await _mockOrganizationRepository.Object.ListAllAsync();
        allOrganizations.Count.Should().Be(3);
    }
}
