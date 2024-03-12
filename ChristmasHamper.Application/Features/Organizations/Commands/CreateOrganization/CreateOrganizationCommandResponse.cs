using ChristmasHamper.Application.Responses;

namespace ChristmasHamper.Application.Features.Organizations.Commands.CreateOrganization;

public class CreateOrganizationCommandResponse : BaseResponse
{
    public int OrganizationId { get; set; } = default;

    public CreateOrganizationCommandResponse() : base()
    {
        
    }
}

