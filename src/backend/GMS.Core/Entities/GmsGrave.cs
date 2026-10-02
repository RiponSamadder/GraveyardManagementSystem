using System;
using System.Collections.Generic;

namespace GMS.Core.Entities;

public partial class GmsGrave
{
    public long GraveId { get; set; }

    public long GraveyardId { get; set; }

    public long? SectionId { get; set; }

    public long? BlockId { get; set; }

    public long? RowId { get; set; }

    public string GraveCode { get; set; } = null!;

    public string? GraveNumber { get; set; }

    public string? GraveTypeCode { get; set; }

    public string GraveStatusCode { get; set; } = null!;

    public decimal? Latitude { get; set; }

    public decimal? Longitude { get; set; }

    public string? MapReference { get; set; }

    public string? Description { get; set; }

    public string? PhotoFilePath { get; set; }

    public string? QrcodeValue { get; set; }

    public bool IsActive { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public long? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual GmsGraveBlock? Block { get; set; }

    public virtual GmsAppUser? CreatedByNavigation { get; set; }

    public virtual ICollection<GmsBurial> GmsBurials { get; set; } = new List<GmsBurial>();

    public virtual ICollection<GmsGraveMaintenance> GmsGraveMaintenances { get; set; } = new List<GmsGraveMaintenance>();

    public virtual ICollection<GmsQrcode> GmsQrcodes { get; set; } = new List<GmsQrcode>();

    public virtual GmsGraveyard Graveyard { get; set; } = null!;

    public virtual ICollection<MemDocument> MemDocuments { get; set; } = new List<MemDocument>();

    public virtual GmsAppUser? ModifiedByNavigation { get; set; }

    public virtual GmsGraveRow? Row { get; set; }

    public virtual GmsGraveyardSection? Section { get; set; }
}
