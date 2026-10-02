using System;
using System.Collections.Generic;

namespace GMS.Core.Entities;

public partial class MemMemorialProfile
{
    public long MemorialProfileId { get; set; }

    public long DeceasedId { get; set; }

    public string? MemorialTitle { get; set; }

    public string MemorialSlug { get; set; } = null!;

    public string? ShortBiography { get; set; }

    public string? FullBiography { get; set; }

    public string? ChildhoodStory { get; set; }

    public string? EducationStory { get; set; }

    public string? CareerStory { get; set; }

    public string? FamilyLifeStory { get; set; }

    public string? SocialContribution { get; set; }

    public string? AchievementStory { get; set; }

    public string? ChallengeStory { get; set; }

    public string? FinalMessage { get; set; }

    public string VisibilityCode { get; set; } = null!;

    public string StatusCode { get; set; } = null!;

    public long? ApprovedBy { get; set; }

    public DateTime? ApprovedAt { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public long? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual GmsAppUser? ApprovedByNavigation { get; set; }

    public virtual GmsAppUser? CreatedByNavigation { get; set; }

    public virtual GmsDeceasedPerson Deceased { get; set; } = null!;

    public virtual GmsAppUser? ModifiedByNavigation { get; set; }
}
