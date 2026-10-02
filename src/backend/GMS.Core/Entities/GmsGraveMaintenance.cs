using System;
using System.Collections.Generic;

namespace GMS.Core.Entities;

public partial class GmsGraveMaintenance
{
    public long MaintenanceId { get; set; }

    public long GraveId { get; set; }

    public DateOnly MaintenanceDate { get; set; }

    public string WorkTypeCode { get; set; } = null!;

    public string? Description { get; set; }

    public decimal? CostAmount { get; set; }

    public long? WorkerId { get; set; }

    public string? BeforePhotoPath { get; set; }

    public string? AfterPhotoPath { get; set; }

    public string StatusCode { get; set; } = null!;

    public long? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual GmsAppUser? CreatedByNavigation { get; set; }

    public virtual GmsGrave Grave { get; set; } = null!;

    public virtual GmsWorker? Worker { get; set; }
}
