using AutoMapper;
using ChristmasHamper.Application.Contracts.Persistence;
using ChristmasHamper.Application.Features.Organizations.Commands.DeleteOrganization;
using ChristmasHamper.Application.Profiles;
using ChristmasHamper.Application.UnitTests.Mocks;
using Moq;
using Shouldly;

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

        result.IsSuccess.ShouldBeTrue();

        var allOrganizations = await _mockOrganizationRepository.Object.ListAllAsync();
        allOrganizations.Count.ShouldBe(2);
    }

    [Fact]
    public async Task WithInvalidId_FailWithOneValidationError()
    {
        var handler = new DeleteOrganizationCommandHandler(_mockOrganizationRepository.Object);
        var id = 99;
        var validationError = "ID provided does not exist.";

        var command = new DeleteOrganizationCommand(id);
        var result = await handler.Handle(command, CancellationToken.None);

        result.IsSuccess.ShouldBeFalse();
        result.Errors!.Count.ShouldBe(1);

        if(result.Errors!.Count == 1)
        {
            result.Errors[0].Message.ShouldBe(validationError);
        }

        var allOrganizations = await _mockOrganizationRepository.Object.ListAllAsync();
        allOrganizations.Count.ShouldBe(3);
    }
}
