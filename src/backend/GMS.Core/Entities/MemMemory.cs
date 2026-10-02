using System;
using System.Collections.Generic;

namespace GMS.Core.Entities;

public partial class MemMemory
{
    public long MemoryId { get; set; }

    public long DeceasedId { get; set; }

    public long? UserId { get; set; }

    public string AuthorName { get; set; } = null!;

    public string? RelationshipToDeceased { get; set; }

    public string? Title { get; set; }

    public string MemoryText { get; set; } = null!;

    public DateOnly? MemoryDate { get; set; }

    public bool IsAnonymous { get; set; }

    public string StatusCode { get; set; } = null!;

    public bool IsPublic { get; set; }

    public long? ReviewedBy { get; set; }

    public DateTime? ReviewedAt { get; set; }

    public string? ReviewRemarks { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual GmsDeceasedPerson Deceased { get; set; } = null!;

    public virtual GmsAppUser? ReviewedByNavigation { get; set; }

    public virtual GmsAppUser? User { get; set; }
}
