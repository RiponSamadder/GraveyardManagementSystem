using System;
using System.Collections.Generic;

namespace GMS.Core.Entities;

public partial class GmsUserRole
{
    public long UserRoleId { get; set; }

    public long UserId { get; set; }

    public long RoleId { get; set; }

    public long? GraveyardId { get; set; }

    public DateTime AssignedAt { get; set; }

    public long? AssignedBy { get; set; }

    public bool IsActive { get; set; }

    public virtual GmsAppUser? AssignedByNavigation { get; set; }

    public virtual GmsGraveyard? Graveyard { get; set; }

    public virtual GmsRole Role { get; set; } = null!;

    public virtual GmsAppUser User { get; set; } = null!;
}
