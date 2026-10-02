using System;
using System.Collections.Generic;

namespace GMS.Core.Entities;

public partial class GmsWorker
{
    public long WorkerId { get; set; }

    public long GraveyardId { get; set; }

    public string WorkerCode { get; set; } = null!;

    public string WorkerName { get; set; } = null!;

    public string? Designation { get; set; }

    public string? MobileNo { get; set; }

    public DateOnly? JoiningDate { get; set; }

    public decimal? SalaryAmount { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<GmsGraveMaintenance> GmsGraveMaintenances { get; set; } = new List<GmsGraveMaintenance>();

    public virtual GmsGraveyard Graveyard { get; set; } = null!;
}
