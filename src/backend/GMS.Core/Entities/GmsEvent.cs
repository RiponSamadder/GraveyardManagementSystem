using System;
using System.Collections.Generic;

namespace GMS.Core.Entities;

public partial class GmsEvent
{
    public long EventId { get; set; }

    public long GraveyardId { get; set; }

    public string EventCode { get; set; } = null!;

    public string EventName { get; set; } = null!;

    public string? EventTypeCode { get; set; }

    public DateTime StartDateTime { get; set; }

    public DateTime? EndDateTime { get; set; }

    public string? Location { get; set; }

    public string? Description { get; set; }

    public string? Organizer { get; set; }

    public bool IsPublic { get; set; }

    public string StatusCode { get; set; } = null!;

    public long? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual GmsAppUser? CreatedByNavigation { get; set; }

    public virtual GmsGraveyard Graveyard { get; set; } = null!;
}
