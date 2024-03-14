using ChristmasHamper.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChristmasHamper.Domain.Entities;

public class User: Auditable
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public required Organization Organization { get; set; }
}

