using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChristmasHamper.Domain.Common;

public class Auditable
{
    public string? CreatedBy {  get; set; }
    public DateTime CreatedOn { get; set; }
    public string? LastModifiedBy { get; set; }
    public DateTime LastModifiedOn { get; set; }
}

