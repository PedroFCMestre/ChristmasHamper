using AutoMapper;
using ChristmasHamper.Application.Contracts.Persistence;
using ChristmasHamper.Application.Profiles;
using ChristmasHamper.Application.UnitTests.Mocks;
using Moq;
using ChristmasHamper.Application.Features.Organizations.Commands.CreateOrganization;
using Shouldly;

namespace ChristmasHamper.Application.UnitTests.Organizations.Commands;

public class CreateOrganizationCommandHandlerTests
{
    private readonly Mock<IOrganizationRepository> _mockOrganizationRepository;
    private readonly IMapper _mapper;

    public CreateOrganizationCommandHandlerTests()
    {
        _mockOrganizationRepository = RepositoryMocks.GetOrganizationRepository();

        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        _mapper = configurationProvider.CreateMapper();
    }

    [Fact]
    public async Task CreateOrganization_WithUniqueFields_CreatesSuccessfully()
    {
        var handler = new CreateOrganizationCommandHandler(_mockOrganizationRepository.Object, _mapper);
        var name = "NewOrganization";
        var acronym = "NewAcronym";

        var command = new CreateOrganizationCommand() { Name = name, Acronym = acronym };
        var result = await handler.Handle(command, CancellationToken.None);

        result.Success.ShouldBeTrue();

        var allOrganizations = await _mockOrganizationRepository.Object.ListAllAsync();
        allOrganizations.Count.ShouldBe(4);
    }

    [Fact]
    public async Task ExistsSameName_FailWithOneValidationError()
    {
        var handler = new CreateOrganizationCommandHandler(_mockOrganizationRepository.Object, _mapper);
        var name = "Organization1";
        var acronym = "NewAcronym";
        var validationError = "An organization with the same name already exists.";

        var command = new CreateOrganizationCommand() { Name = name, Acronym = acronym };
        var result = await handler.Handle(command, CancellationToken.None);

        result.Success.ShouldBeFalse();
        result.ValidationErrors!.Count.ShouldBe(1);

        if (result.ValidationErrors!.Count == 1)
        {
            result.ValidationErrors[0].ShouldBe(validationError);
        }

        var allOrganizations = await _mockOrganizationRepository.Object.ListAllAsync();
        allOrganizations.Count.ShouldBe(3);
    }

    [Fact]
    public async Task ExistsSameAcronym_FailWithOneValidationError()
    {
        var handler = new CreateOrganizationCommandHandler(_mockOrganizationRepository.Object, _mapper);
        var name = "NewOrganization";
        var acronym = "Og1";
        var validationError = "An organization with the same acronym already exists.";

        var command = new CreateOrganizationCommand() { Name = name, Acronym = acronym };
        var result = await handler.Handle(command, CancellationToken.None);

        result.Success.ShouldBeFalse();
        result.ValidationErrors!.Count.ShouldBe(1);

        if(result.ValidationErrors!.Count == 1) 
        { 
            result.ValidationErrors[0].ShouldBe(validationError);
        }

        var allOrganizations = await _mockOrganizationRepository.Object.ListAllAsync();
        allOrganizations.Count.ShouldBe(3);
    }

    [Fact]
    public async Task EmptyFields_FailWithTwoValidationErrors()
    {
        var handler = new CreateOrganizationCommandHandler(_mockOrganizationRepository.Object, _mapper);
        var name = String.Empty;
        var acronym = String.Empty;
        var validationError1 = "Name is required.";
        var validationError2 = "Acronym is required.";

        var command = new CreateOrganizationCommand() { Name = name, Acronym = acronym };
        var result = await handler.Handle(command, CancellationToken.None);

        result.Success.ShouldBeFalse();
        result.ValidationErrors!.Count.ShouldBe(2);

        if (result.ValidationErrors!.Count == 2)
        {
            result.ValidationErrors[0].ShouldBe(validationError1);
            result.ValidationErrors[1].ShouldBe(validationError2);
        }

        var allOrganizations = await _mockOrganizationRepository.Object.ListAllAsync();
        allOrganizations.Count.ShouldBe(3);
    }

    [Fact]
    public async Task NameLongerThanExpected_FailWithOneValidationError()
    {
        var handler = new CreateOrganizationCommandHandler(_mockOrganizationRepository.Object, _mapper);
        var name = "this name has more Than 100 characters!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!";
        var acronym = "NewAcronym";
        var validationError = "Name must not exceed 100 characters.";

        var command = new CreateOrganizationCommand() { Name = name, Acronym = acronym };
        var result = await handler.Handle(command, CancellationToken.None);

        result.Success.ShouldBeFalse();
        result.ValidationErrors!.Count.ShouldBe(1);

        if (result.ValidationErrors!.Count == 1)
        {
            result.ValidationErrors[0].ShouldBe(validationError);
        }

        var allOrganizations = await _mockOrganizationRepository.Object.ListAllAsync();
        allOrganizations.Count.ShouldBe(3);
    }

    [Fact]
    public async Task AcronymLongerThanExpected_FailWithOneValidationError()
    {
        var handler = new CreateOrganizationCommandHandler(_mockOrganizationRepository.Object, _mapper);
        var name = "NewOrganization";
        var acronym = "this acronym has more than 10 characters";
        var validationError = "Acronym must not exceed 10 characters.";

        var command = new CreateOrganizationCommand() { Name = name, Acronym = acronym };
        var result = await handler.Handle(command, CancellationToken.None);

        result.Success.ShouldBeFalse();
        result.ValidationErrors!.Count.ShouldBe(1);

        if (result.ValidationErrors!.Count == 1)
        {
            result.ValidationErrors[0].ShouldBe(validationError);
        }

        var allOrganizations = await _mockOrganizationRepository.Object.ListAllAsync();
        allOrganizations.Count.ShouldBe(3);
    }
}

