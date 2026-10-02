using System;
using System.Collections.Generic;

namespace GMS.Core.Entities;

public partial class GmsMeetingParticipant
{
    public long MeetingParticipantId { get; set; }

    public long MeetingId { get; set; }

    public long CommitteeMemberId { get; set; }

    public string AttendanceStatusCode { get; set; } = null!;

    public string? Remarks { get; set; }

    public virtual GmsCommitteeMember CommitteeMember { get; set; } = null!;

    public virtual GmsCommitteeMeeting Meeting { get; set; } = null!;
}
