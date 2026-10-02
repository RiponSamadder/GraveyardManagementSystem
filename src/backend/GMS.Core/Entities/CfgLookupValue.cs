using System;
using System.Collections.Generic;

namespace GMS.Core.Entities;

public partial class CfgLookupValue
{
    public long LookupValueId { get; set; }

    public long LookupTypeId { get; set; }

    public string LookupCode { get; set; } = null!;

    public string LookupName { get; set; } = null!;

    public string? LookupNameBn { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; }

    public virtual CfgLookupType LookupType { get; set; } = null!;
}
