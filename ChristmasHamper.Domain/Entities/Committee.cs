using ChristmasHamper.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChristmasHamper.Domain.Entities;

public class Committee: Auditable
{
    public int CommitteeId { get; set; }

    public required Organization Organization { get; set; }

    public required string Name { get; set;}

    public int Year { get; set; }

    public required IEnumerable<User> Members { get; set; }
}

