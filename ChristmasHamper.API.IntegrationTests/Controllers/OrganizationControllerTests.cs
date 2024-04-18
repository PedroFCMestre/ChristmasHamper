using ChristmasHamper.API.IntegrationTests.Base;
using ChristmasHamper.Application.Features.Organizations;
using ChristmasHamper.Application.Features.Organizations.Commands.CreateOrganization;
using ChristmasHamper.Application.Features.Organizations.Commands.UpdateOrganization;
using ChristmasHamper.Application.Responses;
using FluentAssertions;
using System.Net;
using System.Text;
using System.Text.Json;
using Xunit;

namespace ChristmasHamper.API.IntegrationTests.Controllers;

public class OrganizationControllerTests(CustomWebApplicationFactory<Program> factory) : IClassFixture<CustomWebApplicationFactory<Program>>
{
    //private readonly CustomWebApplicationFactory<Program> _factory = factory ?? throw new ArgumentNullException(nameof(CustomWebApplicationFactory<Program>));
    private readonly HttpClient _client = factory.CreateClient() ?? throw new ArgumentNullException(nameof(factory));
    private readonly string _controller = "organization";

    [Fact]
    public async Task GetAllOrganizations_ReturnsSuccessResult()
    {
        //Arrange

        
        //Act
        var response = await _client.GetAsync($"/api/{_controller}/all");

        //Assert
        response.EnsureSuccessStatusCode();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<List<OrganizationDto>>(responseString, Utilities.options);

        //Assert.IsType<List<OrganizationDto>>(result);
        //Assert.NotEmpty(result);

        result.Should().BeOfType<List<OrganizationDto>>();
        result.Should().NotBeEmpty();
    }

    [Fact]
    public async Task GetOrganizationById_ValidId_ReturnsSuccessResult()
    {
        //Arrange
        var organizationId = 1;

        //Act
        var response = await _client.GetAsync($"/api/{_controller}/{organizationId}");

        //Assert
        response.EnsureSuccessStatusCode();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<OrganizationDto>(responseString, Utilities.options);

        result.Should().BeOfType<OrganizationDto>();
        result.Should().NotBeNull();
        result?.Id.Should().Be(organizationId);
    }

    [Fact]
    public async Task GetOrganizationById_NonexistentId_ReturnsNotFoundResult()
    {
        //Arrange
        var organizationId = 99;

        //Act
        var response = await _client.GetAsync($"/api/{_controller}/{organizationId}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetOrganizationById_InvalidId_ReturnsBadRequestResult()
    {
        //Arrange
        var organizationId = "test";

        //Act
        var response = await _client.GetAsync($"/api/{_controller}/{organizationId}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
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
        var response = await _client.PostAsync($"/api/{_controller}", content);

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
        var response = await _client.PostAsync($"/api/{_controller}", content);

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
        var response = await _client.PutAsync($"/api/{_controller}", content);

        //Assert
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<BaseResponse>(responseContent, Utilities.options);

        result.Should().BeOfType<BaseResponse>();
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
        var response = await _client.PutAsync($"/api/{_controller}", content);

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
        var response = await _client.DeleteAsync($"/api/{_controller}/{id}");

        //Assert
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<BaseResponse>(responseContent, Utilities.options);

        result.Should().BeOfType<BaseResponse>();
    }

    [Fact]
    public async Task DeleteOrganization_InvalidData_ReturnsNotFound()
    {
        //Arrange
        var id = 99;

        //Act
        var response = await _client.DeleteAsync($"/api/{_controller}/{id}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        var responseContent = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<BaseResponse>(responseContent, Utilities.options);

        result.Should().BeOfType<BaseResponse>();
    }
}
