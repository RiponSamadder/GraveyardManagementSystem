using System;
using System.Collections.Generic;

namespace GMS.Core.Entities;

public partial class CfgLookupType
{
    public long LookupTypeId { get; set; }

    public string LookupTypeCode { get; set; } = null!;

    public string LookupTypeName { get; set; } = null!;

    public virtual ICollection<CfgLookupValue> CfgLookupValues { get; set; } = new List<CfgLookupValue>();
}
