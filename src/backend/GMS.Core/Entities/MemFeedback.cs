using System;
using System.Collections.Generic;

namespace GMS.Core.Entities;

public partial class MemFeedback
{
    public long FeedbackId { get; set; }

    public long? UserId { get; set; }

    public long? DeceasedId { get; set; }

    public string FeedbackTypeCode { get; set; } = null!;

    public string? Subject { get; set; }

    public string FeedbackText { get; set; } = null!;

    public string? ContactName { get; set; }

    public string? ContactEmail { get; set; }

    public string? ContactMobile { get; set; }

    public string StatusCode { get; set; } = null!;

    public long? AssignedTo { get; set; }

    public string? ResolutionRemarks { get; set; }

    public DateTime? ResolvedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual GmsAppUser? AssignedToNavigation { get; set; }

    public virtual GmsDeceasedPerson? Deceased { get; set; }

    public virtual GmsAppUser? User { get; set; }
}
