using System;
using System.Collections.Generic;

namespace GMS.Core.Entities;

public partial class MemDeceasedRelationship
{
    public long DeceasedRelationshipId { get; set; }

    public long DeceasedId { get; set; }

    public long? RelatedDeceasedId { get; set; }

    public long? RelatedUserId { get; set; }

    public long RelationshipTypeId { get; set; }

    public string? RelationshipName { get; set; }

    public bool IsPublic { get; set; }

    public string? Notes { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual GmsAppUser? CreatedByNavigation { get; set; }

    public virtual GmsDeceasedPerson Deceased { get; set; } = null!;

    public virtual GmsDeceasedPerson? RelatedDeceased { get; set; }

    public virtual GmsAppUser? RelatedUser { get; set; }

    public virtual CfgRelationshipType RelationshipType { get; set; } = null!;
}
