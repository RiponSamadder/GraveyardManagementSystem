using System;
using System.Collections.Generic;

namespace GMS.Core.Entities;

public partial class GmsGraveRow
{
    public long RowId { get; set; }

    public long BlockId { get; set; }

    public string RowCode { get; set; } = null!;

    public string RowName { get; set; } = null!;

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; }

    public virtual GmsGraveBlock Block { get; set; } = null!;

    public virtual ICollection<GmsGrave> GmsGraves { get; set; } = new List<GmsGrave>();
}
