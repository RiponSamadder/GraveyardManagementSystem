using System;
using System.Collections.Generic;

namespace GMS.Core.Entities;

public partial class CfgRelationshipType
{
    public long RelationshipTypeId { get; set; }

    public string RelationshipCode { get; set; } = null!;

    public string RelationshipName { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual ICollection<MemDeceasedRelationship> MemDeceasedRelationships { get; set; } = new List<MemDeceasedRelationship>();
}
