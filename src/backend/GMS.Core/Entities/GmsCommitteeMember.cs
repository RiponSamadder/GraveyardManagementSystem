using System;
using System.Collections.Generic;

namespace GMS.Core.Entities;

public partial class GmsCommitteeMember
{
    public long CommitteeMemberId { get; set; }

    public long CommitteeId { get; set; }

    public long? UserId { get; set; }

    public string MemberName { get; set; } = null!;

    public string? DesignationCode { get; set; }

    public string? MobileNo { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string? Responsibilities { get; set; }

    public bool IsActive { get; set; }

    public virtual GmsCommittee Committee { get; set; } = null!;

    public virtual ICollection<GmsMeetingParticipant> GmsMeetingParticipants { get; set; } = new List<GmsMeetingParticipant>();

    public virtual GmsAppUser? User { get; set; }
}
