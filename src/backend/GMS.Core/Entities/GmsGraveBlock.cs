using System;
using System.Collections.Generic;

namespace GMS.Core.Entities;

public partial class GmsGraveBlock
{
    public long BlockId { get; set; }

    public long SectionId { get; set; }

    public string BlockCode { get; set; } = null!;

    public string BlockName { get; set; } = null!;

    public string? Description { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<GmsGraveRow> GmsGraveRows { get; set; } = new List<GmsGraveRow>();

    public virtual ICollection<GmsGrave> GmsGraves { get; set; } = new List<GmsGrave>();

    public virtual GmsGraveyardSection Section { get; set; } = null!;
}
