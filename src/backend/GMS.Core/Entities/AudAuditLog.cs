using System;
using System.Collections.Generic;

namespace GMS.Core.Entities;

public partial class AudAuditLog
{
    public long AuditLogId { get; set; }

    public long? UserId { get; set; }

    public long? GraveyardId { get; set; }

    public string ModuleName { get; set; } = null!;

    public string? TableName { get; set; }

    public long? RecordId { get; set; }

    public string ActionCode { get; set; } = null!;

    public string? OldValues { get; set; }

    public string? NewValues { get; set; }

    public string? Ipaddress { get; set; }

    public string? UserAgent { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual GmsGraveyard? Graveyard { get; set; }

    public virtual GmsAppUser? User { get; set; }
}
