using System;
using System.Collections.Generic;

namespace GMS.Core.Entities;

public partial class MemTribute
{
    public long TributeId { get; set; }

    public long DeceasedId { get; set; }

    public long? UserId { get; set; }

    public string AuthorName { get; set; } = null!;

    public string TributeText { get; set; } = null!;

    public bool IsAnonymous { get; set; }

    public string StatusCode { get; set; } = null!;

    public bool IsPublic { get; set; }

    public long? ReviewedBy { get; set; }

    public DateTime? ReviewedAt { get; set; }

    public string? ReviewRemarks { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual GmsDeceasedPerson Deceased { get; set; } = null!;

    public virtual GmsAppUser? ReviewedByNavigation { get; set; }

    public virtual GmsAppUser? User { get; set; }
}
