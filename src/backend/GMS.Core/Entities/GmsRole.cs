using System;
using System.Collections.Generic;

namespace GMS.Core.Entities;

public partial class GmsRole
{
    public long RoleId { get; set; }

    public string RoleCode { get; set; } = null!;

    public string RoleName { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsSystemRole { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<GmsRolePermission> GmsRolePermissions { get; set; } = new List<GmsRolePermission>();

    public virtual ICollection<GmsUserRole> GmsUserRoles { get; set; } = new List<GmsUserRole>();
}
