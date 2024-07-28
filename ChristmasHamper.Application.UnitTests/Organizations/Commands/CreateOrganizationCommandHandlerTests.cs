using AutoMapper;
using ChristmasHamper.Application.Contracts.Persistence;
using ChristmasHamper.Application.Profiles;
using ChristmasHamper.Application.UnitTests.Mocks;
using Moq;
using ChristmasHamper.Application.Features.Organizations.Commands.CreateOrganization;
using Microsoft.Extensions.Logging;
using FluentAssertions;

namespace ChristmasHamper.Application.UnitTests.Organizations.Commands;

public class CreateOrganizationCommandHandlerTests
{
    private readonly Mock<IOrganizationRepository> _mockOrganizationRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateOrganizationCommandHandler> _logger;

    public CreateOrganizationCommandHandlerTests()
    {
        _mockOrganizationRepository = RepositoryMocks.GetOrganizationRepository();

        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        _mapper = configurationProvider.CreateMapper();

        _logger = Mock.Of<ILogger<CreateOrganizationCommandHandler>>();
    }

    [Fact]
    public async Task CreateOrganization_WithUniqueFields_CreatesSuccessfully()
    {
        var handler = new CreateOrganizationCommandHandler(_mockOrganizationRepository.Object, _mapper, _logger);
        var name = "NewOrganization";
        var acronym = "NewAcronym";

        var command = new CreateOrganizationCommand() { Name = name, Acronym = acronym };
        var result = await handler.Handle(command, CancellationToken.None);

        //result.Success.Should().BeTrue();
        result.IsSuccess.Should().BeTrue();

        var allOrganizations = await _mockOrganizationRepository.Object.ListAllAsync();
        allOrganizations.Count.Should().Be(4);
    }

    [Fact]
    public async Task ExistsSameName_FailWithOneValidationError()
    {
        var handler = new CreateOrganizationCommandHandler(_mockOrganizationRepository.Object, _mapper, _logger);
        var name = "Organization1";
        var acronym = "NewAcronym";
        var validationError = "An organization with the same name already exists.";

        var command = new CreateOrganizationCommand() { Name = name, Acronym = acronym };
        var result = await handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
        result.Errors!.Count.Should().Be(1);

        result.Errors.FirstOrDefault()!.Message.Should().Be(validationError);
        
        var allOrganizations = await _mockOrganizationRepository.Object.ListAllAsync();
        allOrganizations.Count.Should().Be(3);
    }

    [Fact]
    public async Task ExistsSameAcronym_FailWithOneValidationError()
    {
        var handler = new CreateOrganizationCommandHandler(_mockOrganizationRepository.Object, _mapper, _logger);
        var name = "NewOrganization";
        var acronym = "Og1";
        var validationError = "An organization with the same acronym already exists.";

        var command = new CreateOrganizationCommand() { Name = name, Acronym = acronym };
        var result = await handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
        result.Errors!.Count.Should().Be(1);

        if (!result.Errors!.Any())
        {
            result.Errors[0].Message.Should().Be(validationError);
        }

        var allOrganizations = await _mockOrganizationRepository.Object.ListAllAsync();
        allOrganizations.Count.Should().Be(3);
    }

    [Fact]
    public async Task EmptyFields_FailWithTwoValidationErrors()
    {
        var handler = new CreateOrganizationCommandHandler(_mockOrganizationRepository.Object, _mapper, _logger);
        var name = String.Empty;
        var acronym = String.Empty;
        var validationError1 = "Name is required.";
        var validationError2 = "Acronym is required.";

        var command = new CreateOrganizationCommand() { Name = name, Acronym = acronym };
        var result = await handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
        result.Errors!.Count.Should().Be(2);

        if (result.Errors!.Count == 2)
        {
            result.Errors[0].Message.Should().Be(validationError1);
            result.Errors[1].Message.Should().Be(validationError2);
        }

        var allOrganizations = await _mockOrganizationRepository.Object.ListAllAsync();
        allOrganizations.Count.Should().Be(3);
    }

    [Fact]
    public async Task NameLongerThanExpected_FailWithOneValidationError()
    {
        var handler = new CreateOrganizationCommandHandler(_mockOrganizationRepository.Object, _mapper, _logger);
        var name = "this name has more Than 100 characters!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!";
        var acronym = "NewAcronym";
        var validationError = "Name must not exceed 100 characters.";

        var command = new CreateOrganizationCommand() { Name = name, Acronym = acronym };
        var result = await handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
        result.Errors!.Count.Should().Be(1);

        result.Errors.FirstOrDefault()!.Message.Should().Be(validationError);
        
        var allOrganizations = await _mockOrganizationRepository.Object.ListAllAsync();
        allOrganizations.Count.Should().Be(3);
    }

    [Fact]
    public async Task AcronymLongerThanExpected_FailWithOneValidationError()
    {
        var handler = new CreateOrganizationCommandHandler(_mockOrganizationRepository.Object, _mapper, _logger);
        var name = "NewOrganization";
        var acronym = "this acronym has more than 10 characters";
        var validationError = "Acronym must not exceed 10 characters.";

        var command = new CreateOrganizationCommand() { Name = name, Acronym = acronym };
        var result = await handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
        result.Errors!.Count.Should().Be(1);

        result.Errors.FirstOrDefault()!.Message.Should().Be(validationError);

        var allOrganizations = await _mockOrganizationRepository.Object.ListAllAsync();
        allOrganizations.Count.Should().Be(3);
    }
}

