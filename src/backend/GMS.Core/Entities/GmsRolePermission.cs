using System;
using System.Collections.Generic;

namespace GMS.Core.Entities;

public partial class GmsRolePermission
{
    public long RolePermissionId { get; set; }

    public long RoleId { get; set; }

    public long PermissionId { get; set; }

    public bool IsAllowed { get; set; }

    public virtual GmsPermission Permission { get; set; } = null!;

    public virtual GmsRole Role { get; set; } = null!;
}
