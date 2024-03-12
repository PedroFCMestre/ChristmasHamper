using ChristmasHamper.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChristmasHamper.Domain.Entities;

public class Organization: Auditable
{
    public int OrganizationId { get; set; }
    public required string Name { get; set;}
    public required string Acronym { get; set;}
}

