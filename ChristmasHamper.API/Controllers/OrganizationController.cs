using ChristmasHamper.Application.Features.Organizations;
using ChristmasHamper.Application.Features.Organizations.Commands.CreateOrganization;
using ChristmasHamper.Application.Features.Organizations.Commands.DeleteOrganization;
using ChristmasHamper.Application.Features.Organizations.Commands.UpdateOrganization;
using ChristmasHamper.Application.Features.Organizations.Queries.GetOrganization;
using ChristmasHamper.Application.Features.Organizations.Queries.GetOrganizationsList;
using ChristmasHamper.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChristmasHamper.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrganizationController(IMediator mediator) : Controller
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpGet("all", Name ="GetAllOrganizations")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<OrganizationDto>>> GetAllOrganizations()
    {
        var dto = await _mediator.Send(new GetOrganizationsListQuery());
        return Ok(dto);
    }

    [HttpGet("{id}", Name = "GetOrganizationById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OrganizationDto>> GetOrganizationById(int id)
    {
        var dto = await _mediator.Send(new GetOrganizationQuery(id));

        if (dto == null) 
        { 
            return NotFound();
        }
        
        return Ok(dto);
    }

    [HttpPost(Name = "AddOrganization")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreateOrganizationCommandResponse>> CreateOrganization([FromBody] CreateOrganizationCommand createOrganizationCommand)
    {
        var response = await _mediator.Send(createOrganizationCommand);
        
        if (response.Success)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpPut(Name = "UpdateOrganization")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResponse>> UpdateOrganization([FromBody] UpdateOrganizationCommand updateOrganizationCommand)
    {
        var response = await _mediator.Send(updateOrganizationCommand);

        if (response.Success)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }


    [HttpDelete("{id}", Name = "DeleteOrganization")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BaseResponse>> DeleteOrganization(int id)
    {
        var response = await _mediator.Send(new DeleteOrganizationCommand(id));

        if(response.Success) { 
            return Ok(response);
        }
        
        return NotFound(response);
    }
}

