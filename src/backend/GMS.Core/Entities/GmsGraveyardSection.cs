using System;
using System.Collections.Generic;

namespace GMS.Core.Entities;

public partial class GmsGraveyardSection
{
    public long SectionId { get; set; }

    public long GraveyardId { get; set; }

    public string SectionCode { get; set; } = null!;

    public string SectionName { get; set; } = null!;

    public string? Description { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public long? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual GmsAppUser? CreatedByNavigation { get; set; }

    public virtual ICollection<GmsGraveBlock> GmsGraveBlocks { get; set; } = new List<GmsGraveBlock>();

    public virtual ICollection<GmsGrave> GmsGraves { get; set; } = new List<GmsGrave>();

    public virtual GmsGraveyard Graveyard { get; set; } = null!;

    public virtual GmsAppUser? ModifiedByNavigation { get; set; }
}
