using ChristmasHamper.API.IntegrationTests.Base;
using ChristmasHamper.Application.Features.Organizations;
using ChristmasHamper.Application.Features.Organizations.Commands.CreateOrganization;
using ChristmasHamper.Application.Features.Organizations.Commands.UpdateOrganization;
using ChristmasHamper.Application.Features.Organizations.Queries.GetOrganization;
using ChristmasHamper.Application.Features.Organizations.Queries.GetOrganizationsList;
using ChristmasHamper.Application.Responses;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text;
using System.Text.Json;
using Xunit;

namespace ChristmasHamper.API.IntegrationTests.Controllers;

public class OrganizationControllerTests: IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly string _apiUri = "/api/organization";

    public OrganizationControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient() ?? throw new ArgumentNullException(nameof(factory));
    }

    [Fact]
    public async Task GetAllOrganizations_ReturnsOk()
    {
        //Arrange

        
        //Act
        var response = await _client.GetAsync($"{_apiUri}/all");

        //Assert
        response.EnsureSuccessStatusCode();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<List<GetOrganizationsListQueryResponse>>(responseString, Utilities.options);

        result.Should().BeOfType<List<GetOrganizationsListQueryResponse>>();
        result.Should().NotBeEmpty();
    }

    [Fact]
    public async Task GetOrganizationById_ValidId_ReturnsOk()
    {
        //Arrange
        var organizationId = 1;

        //Act
        var response = await _client.GetAsync($"{_apiUri}/{organizationId}");

        //Assert
        response.EnsureSuccessStatusCode();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<GetOrganizationQueryResponse>(responseString, Utilities.options);

        result.Should().BeOfType<GetOrganizationQueryResponse>();
        result.Should().NotBeNull();
        result?.Id.Should().Be(organizationId);
    }

    [Fact]
    public async Task GetOrganizationById_NonexistentId_ReturnsNotFound()
    {
        //Arrange
        var organizationId = 99;

        //Act
        var response = await _client.GetAsync($"{_apiUri}/{organizationId}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetOrganizationById_InvalidId_ReturnsNotFound()
    {
        //Arrange
        var organizationId = "test";

        //Act
        var response = await _client.GetAsync($"{_apiUri}/{organizationId}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CreateOrganization_ValidData_ReturnsOk()
    {
        //Arrange
        var createCommand = new CreateOrganizationCommand
        {
            Name = "Organization99",
            Acronym = "Og99"
        };

        var content = new StringContent(JsonSerializer.Serialize(createCommand), Encoding.UTF8, "application/json");

        //Act
        var response = await _client.PostAsync($"{_apiUri}", content);

        //Assert
        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<CreateOrganizationCommandResponse>(responseContent, Utilities.options);

        result.Should().BeOfType<CreateOrganizationCommandResponse>();
        result?.Id.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task CreateOrganization_InvalidData_ReturnsBadRequest()
    {
        //Arrange
        var createCommand = new CreateOrganizationCommand
        {
            Name = string.Empty,
            Acronym = string.Empty
        };

        var content = new StringContent(JsonSerializer.Serialize(createCommand), Encoding.UTF8, "application/json");

        //Act
        var response = await _client.PostAsync($"{_apiUri}", content);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var responseContent = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<CreateOrganizationCommandResponse>(responseContent, Utilities.options);

        result.Should().BeOfType<CreateOrganizationCommandResponse>();
    }

    [Fact]
    public async Task UpdateOrganization_ValidData_ReturnsOk()
    {
        //Arrange
        var updateCommand = new UpdateOrganizationCommand
        {
            Id = 1,
            Name = "UpdateOrganization1",
            Acronym = "UpdateOg1"
        };

        var content = new StringContent(JsonSerializer.Serialize(updateCommand), Encoding.UTF8, "application/json");

        //Act
        var response = await _client.PutAsync($"{_apiUri}", content);

        //Assert
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task UpdateOrganization_InvalidData_ReturnsBadRequest()
    {
        //Arrange
        var updateCommand = new UpdateOrganizationCommand
        {
            Id = 99,
            Name = string.Empty,
            Acronym = string.Empty
        };

        var content = new StringContent(JsonSerializer.Serialize(updateCommand), Encoding.UTF8, "application/json");

        //Act
        var response = await _client.PutAsync($"{_apiUri}", content);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var responseContent = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<BaseResponse>(responseContent, Utilities.options);

        result.Should().BeOfType<BaseResponse>();
    }

    [Fact]
    public async Task DeleteOrganization_ValidData_ReturnsOk()
    {
        //Arrange
        var id = 3;

        //Act
        var response = await _client.DeleteAsync($"{_apiUri}/{id}");

        //Assert
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task DeleteOrganization_InvalidData_ReturnsNotFound()
    {
        //Arrange
        var id = 99;

        //Act
        var response = await _client.DeleteAsync($"{_apiUri}/{id}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        var responseContent = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<ProblemDetails>(responseContent, Utilities.options);

        result.Should().BeOfType<ProblemDetails>();
    }
}
