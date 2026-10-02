using System;
using System.Collections.Generic;

namespace GMS.Core.Entities;

public partial class GmsCommittee
{
    public long CommitteeId { get; set; }

    public long GraveyardId { get; set; }

    public string CommitteeCode { get; set; } = null!;

    public string CommitteeName { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string StatusCode { get; set; } = null!;

    public string? Description { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual GmsAppUser? CreatedByNavigation { get; set; }

    public virtual ICollection<GmsCommitteeMeeting> GmsCommitteeMeetings { get; set; } = new List<GmsCommitteeMeeting>();

    public virtual ICollection<GmsCommitteeMember> GmsCommitteeMembers { get; set; } = new List<GmsCommitteeMember>();

    public virtual GmsGraveyard Graveyard { get; set; } = null!;
}
