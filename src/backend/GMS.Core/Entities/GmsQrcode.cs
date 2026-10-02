using System;
using System.Collections.Generic;

namespace GMS.Core.Entities;

public partial class GmsQrcode
{
    public long QrcodeId { get; set; }

    public long GraveId { get; set; }

    public string QrcodeValue { get; set; } = null!;

    public string? PublicUrl { get; set; }

    public DateTime GeneratedAt { get; set; }

    public long? GeneratedBy { get; set; }

    public string? FilePath { get; set; }

    public bool IsActive { get; set; }

    public virtual GmsAppUser? GeneratedByNavigation { get; set; }

    public virtual ICollection<GmsQrscanLog> GmsQrscanLogs { get; set; } = new List<GmsQrscanLog>();

    public virtual GmsGrave Grave { get; set; } = null!;
}
