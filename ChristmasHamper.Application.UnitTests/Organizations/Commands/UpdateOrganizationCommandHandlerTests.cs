﻿using AutoMapper;
using ChristmasHamper.Application.Contracts.Persistence;
using ChristmasHamper.Application.Features.Organizations.Commands.UpdateOrganization;
using ChristmasHamper.Application.Profiles;
using ChristmasHamper.Application.UnitTests.Mocks;
using FluentAssertions;
using Moq;

namespace ChristmasHamper.Application.UnitTests.Organizations.Commands;

public class UpdateOrganizationCommandHandlerTests
{
    private readonly Mock<IOrganizationRepository> _mockOrganizationRepository;
    private readonly IMapper _mapper;

    public UpdateOrganizationCommandHandlerTests()
    {
        _mockOrganizationRepository = RepositoryMocks.GetOrganizationRepository();

        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        _mapper = configurationProvider.CreateMapper();
    }

    [Fact]
    public async Task UpdateOrganization_WithUniqueFields_UpdatesSuccessfully()
    {
        var handler = new UpdateOrganizationCommandHandler(_mockOrganizationRepository.Object, _mapper);
        var id = 0;
        var name = "NewOrganization";
        var acronym = "NewAcronym";

        var command = new UpdateOrganizationCommand() { Id = id, Name = name, Acronym = acronym };
        var result = await handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();

        var allOrganizations = await _mockOrganizationRepository.Object.ListAllAsync();
        allOrganizations.Count.Should().Be(3);
    }

    [Fact]
    public async Task IdDoesNotExists_FailWithOneValidationError()
    {
        var handler = new UpdateOrganizationCommandHandler(_mockOrganizationRepository.Object, _mapper);
        var id = 99;
        var name = "NewOrganization";
        var acronym = "NewAcronym";
        var validationError = "ID provided does not exist.";

        var command = new UpdateOrganizationCommand() { Id = id, Name = name, Acronym = acronym };
        var result = await handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
        result.Errors!.Count.Should().Be(1);

        result.Errors.FirstOrDefault()!.Message.Should().Be(validationError);
        
        var allOrganizations = await _mockOrganizationRepository.Object.ListAllAsync();
        allOrganizations.Count.Should().Be(3);
    }

    [Fact]
    public async Task ExistsSameName_FailWithOneValidationError()
    {
        var handler = new UpdateOrganizationCommandHandler(_mockOrganizationRepository.Object, _mapper);
        var id = 1;
        var name = "Organization2";
        var acronym = "NewAcronym";
        var validationError = "An organization with the same name already exists.";

        var command = new UpdateOrganizationCommand() { Id = id, Name = name, Acronym = acronym };
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
        var handler = new UpdateOrganizationCommandHandler(_mockOrganizationRepository.Object, _mapper);
        var id = 1;
        var name = "NewOrganization";
        var acronym = "Og2";
        var validationError = "An organization with the same acronym already exists.";

        var command = new UpdateOrganizationCommand() { Id = id, Name = name, Acronym = acronym };
        var result = await handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
        result.Errors!.Count.Should().Be(1);

        result.Errors.FirstOrDefault()!.Message.Should().Be(validationError);
        
        var allOrganizations = await _mockOrganizationRepository.Object.ListAllAsync();
        allOrganizations.Count.Should().Be(3);
    }

    [Fact]
    public async Task EmptyFields_FailWithTwoValidationErrors()
    {
        var handler = new UpdateOrganizationCommandHandler(_mockOrganizationRepository.Object, _mapper);
        var id = 1;
        var name = String.Empty;
        var acronym = String.Empty;
        var validationError1 = "Name is required.";
        var validationError2 = "Acronym is required.";

        var command = new UpdateOrganizationCommand() { Id = id, Name = name, Acronym = acronym };
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
        var handler = new UpdateOrganizationCommandHandler(_mockOrganizationRepository.Object, _mapper);
        var id = 1;
        var name = "this name has more Than 100 characters!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!";
        var acronym = "NewAcronym";
        var validationError = "Name must not exceed 100 characters.";

        var command = new UpdateOrganizationCommand() { Id = id, Name = name, Acronym = acronym };
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
        var handler = new UpdateOrganizationCommandHandler(_mockOrganizationRepository.Object, _mapper);
        var id = 1;
        var name = "NewOrganization";
        var acronym = "this acronym has more than 10 characters";
        var validationError = "Acronym must not exceed 10 characters.";

        var command = new UpdateOrganizationCommand() { Id = id, Name = name, Acronym = acronym };
        var result = await handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
        result.Errors!.Count.Should().Be(1);

        result.Errors.FirstOrDefault()!.Message.Should().Be(validationError);

        var allOrganizations = await _mockOrganizationRepository.Object.ListAllAsync();
        allOrganizations.Count.Should().Be(3);
    }
}
