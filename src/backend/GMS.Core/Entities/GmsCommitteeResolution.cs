using System;
using System.Collections.Generic;

namespace GMS.Core.Entities;

public partial class GmsCommitteeResolution
{
    public long ResolutionId { get; set; }

    public long MeetingId { get; set; }

    public string ResolutionNo { get; set; } = null!;

    public string Subject { get; set; } = null!;

    public string ResolutionText { get; set; } = null!;

    public string? ResponsiblePerson { get; set; }

    public DateOnly? TargetDate { get; set; }

    public string StatusCode { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual GmsCommitteeMeeting Meeting { get; set; } = null!;
}
