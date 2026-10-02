using System;
using System.Collections.Generic;

namespace GMS.Core.Entities;

public partial class GmsNotice
{
    public long NoticeId { get; set; }

    public long GraveyardId { get; set; }

    public string NoticeNo { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string NoticeText { get; set; } = null!;

    public DateOnly PublishDate { get; set; }

    public DateOnly? ExpiryDate { get; set; }

    public string? PriorityCode { get; set; }

    public bool IsPublic { get; set; }

    public string StatusCode { get; set; } = null!;

    public long? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual GmsAppUser? CreatedByNavigation { get; set; }

    public virtual GmsGraveyard Graveyard { get; set; } = null!;
}
