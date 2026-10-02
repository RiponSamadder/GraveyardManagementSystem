using System;
using System.Collections.Generic;

namespace GMS.Core.Entities;

public partial class GmsOrganization
{
    public long OrganizationId { get; set; }

    public string OrganizationCode { get; set; } = null!;

    public string OrganizationName { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public long? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual ICollection<GmsGraveyard> GmsGraveyards { get; set; } = new List<GmsGraveyard>();
}
