using System;
using System.Collections.Generic;

namespace GMS.Core.Entities;

public partial class MemLifeLesson
{
    public long LifeLessonId { get; set; }

    public long DeceasedId { get; set; }

    public string? LessonCategory { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? LessonTypeCode { get; set; }

    public bool IsPublic { get; set; }

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
