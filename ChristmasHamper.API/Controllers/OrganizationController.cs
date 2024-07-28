using ChristmasHamper.API.Extensions;
using ChristmasHamper.Application.Features.Organizations.Commands.CreateOrganization;
using ChristmasHamper.Application.Features.Organizations.Commands.DeleteOrganization;
using ChristmasHamper.Application.Features.Organizations.Commands.UpdateOrganization;
using ChristmasHamper.Application.Features.Organizations.Queries.GetOrganization;
using ChristmasHamper.Application.Features.Organizations.Queries.GetOrganizationsList;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ChristmasHamper.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrganizationController : Controller
{
    private readonly IMediator _mediator;

    public OrganizationController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet("all", Name ="GetAllOrganizations")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<List<GetOrganizationsListQueryResponse>>> GetAllOrganizations()
    {
        var response = await _mediator.Send(new GetOrganizationsListQuery());

        if (response.Value.Count == 0)
        {
            return NoContent();
        }

        return Ok(response.Value);
    }

    [HttpGet("{id:int}", Name = "GetOrganizationById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetOrganizationQueryResponse>> GetOrganizationById(int id)
    {
        var response = await _mediator.Send(new GetOrganizationQuery(id));

        if (response.IsFailed) 
        {
            return response.HandleError();
        }
        
        return Ok(response.Value);
    }

    [HttpPost(Name = "AddOrganization")]
    [SwaggerOperation(Summary = "Adds an organization", Description = "Inserts a new organization into the database.")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType(typeof(ProblemDetails))]
    public async Task<ActionResult<CreateOrganizationCommandResponse>> CreateOrganization([FromBody] CreateOrganizationCommand createOrganizationCommand)
    {
        var result = await _mediator.Send(createOrganizationCommand);

        if (result.IsFailed)
        {
             return result.HandleError();
        }

        return CreatedAtAction(nameof(GetOrganizationById), new {id = result.Value.Id}, result.Value);
    }

    [HttpPut(Name = "UpdateOrganization")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Unit>> UpdateOrganization([FromBody] UpdateOrganizationCommand updateOrganizationCommand)
    {
        var result = await _mediator.Send(updateOrganizationCommand);

        if (result.IsFailed)
        {
            return result.HandleError();
        }

        return NoContent();
    }


    [HttpDelete("{id:int}", Name = "DeleteOrganization")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Unit>> DeleteOrganization(int id)
    {
        var result = await _mediator.Send(new DeleteOrganizationCommand(id));

        if (result.IsFailed)
        {
            return result.HandleError();
        }

        return NoContent();
    }
}

