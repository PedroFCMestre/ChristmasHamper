﻿using ChristmasHamper.Application.Responses;

namespace ChristmasHamper.Application.Features.Organizations.Commands.CreateOrganization;

public class CreateOrganizationCommandResponse : BaseResponse
{
    public int Id { get; set; } = -1;

    public CreateOrganizationCommandResponse() : base()
    {
        
    }
}

