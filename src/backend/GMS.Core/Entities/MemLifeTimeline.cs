using System;
using System.Collections.Generic;

namespace GMS.Core.Entities;

public partial class MemLifeTimeline
{
    public long TimelineId { get; set; }

    public long DeceasedId { get; set; }

    public DateOnly? EventDate { get; set; }

    public short? ApproximateYear { get; set; }

    public string EventTitle { get; set; } = null!;

    public string? EventDescription { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsPublic { get; set; }

    public string StatusCode { get; set; } = null!;

    public long? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public long? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual GmsAppUser? CreatedByNavigation { get; set; }

    public virtual GmsDeceasedPerson Deceased { get; set; } = null!;

    public virtual GmsAppUser? ModifiedByNavigation { get; set; }
}
