using System;
using System.Collections.Generic;

namespace GMS.Core.Entities;

public partial class GmsCommitteeMeeting
{
    public long MeetingId { get; set; }

    public long CommitteeId { get; set; }

    public string MeetingNo { get; set; } = null!;

    public DateOnly MeetingDate { get; set; }

    public TimeOnly? MeetingTime { get; set; }

    public string? Location { get; set; }

    public string? Agenda { get; set; }

    public string? Minutes { get; set; }

    public string StatusCode { get; set; } = null!;

    public long? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual GmsCommittee Committee { get; set; } = null!;

    public virtual GmsAppUser? CreatedByNavigation { get; set; }

    public virtual ICollection<GmsCommitteeResolution> GmsCommitteeResolutions { get; set; } = new List<GmsCommitteeResolution>();

    public virtual ICollection<GmsMeetingParticipant> GmsMeetingParticipants { get; set; } = new List<GmsMeetingParticipant>();
}
