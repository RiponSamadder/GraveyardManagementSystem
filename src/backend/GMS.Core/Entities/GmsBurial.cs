using System;
using System.Collections.Generic;

namespace GMS.Core.Entities;

public partial class GmsBurial
{
    public long BurialId { get; set; }

    public long DeceasedId { get; set; }

    public long GraveId { get; set; }

    public string BurialReferenceNo { get; set; } = null!;

    public DateOnly BurialDate { get; set; }

    public TimeOnly? BurialTime { get; set; }

    public string? BurialTypeCode { get; set; }

    public string? BurialPerformedBy { get; set; }

    public string? ResponsiblePerson { get; set; }

    public string? Notes { get; set; }

    public bool IsPrimary { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public long? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual GmsAppUser? CreatedByNavigation { get; set; }

    public virtual GmsDeceasedPerson Deceased { get; set; } = null!;

    public virtual GmsGrave Grave { get; set; } = null!;

    public virtual GmsAppUser? ModifiedByNavigation { get; set; }
}
