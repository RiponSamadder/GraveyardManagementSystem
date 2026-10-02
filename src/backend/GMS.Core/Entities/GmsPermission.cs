using System;
using System.Collections.Generic;

namespace GMS.Core.Entities;

public partial class GmsPermission
{
    public long PermissionId { get; set; }

    public string PermissionCode { get; set; } = null!;

    public string PermissionName { get; set; } = null!;

    public string ModuleName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<GmsRolePermission> GmsRolePermissions { get; set; } = new List<GmsRolePermission>();
}
