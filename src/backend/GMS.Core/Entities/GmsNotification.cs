using System;
using System.Collections.Generic;

namespace GMS.Core.Entities;

public partial class GmsNotification
{
    public long NotificationId { get; set; }

    public long UserId { get; set; }

    public string? NotificationType { get; set; }

    public string Title { get; set; } = null!;

    public string Message { get; set; } = null!;

    public string? ReferenceTable { get; set; }

    public long? ReferenceId { get; set; }

    public bool IsRead { get; set; }

    public DateTime? ReadAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual GmsAppUser User { get; set; } = null!;
}
