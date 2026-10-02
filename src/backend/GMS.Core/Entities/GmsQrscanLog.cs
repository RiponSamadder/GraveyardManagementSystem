using System;
using System.Collections.Generic;

namespace GMS.Core.Entities;

public partial class GmsQrscanLog
{
    public long QrscanLogId { get; set; }

    public long QrcodeId { get; set; }

    public long? UserId { get; set; }

    public DateTime ScannedAt { get; set; }

    public string? Ipaddress { get; set; }

    public string? UserAgent { get; set; }

    public decimal? Latitude { get; set; }

    public decimal? Longitude { get; set; }

    public virtual GmsQrcode Qrcode { get; set; } = null!;

    public virtual GmsAppUser? User { get; set; }
}
