using System;
using System.Collections.Generic;

namespace GMS.Core.Entities;

public partial class MemDocument
{
    public long DocumentId { get; set; }

    public long? DeceasedId { get; set; }

    public long? GraveId { get; set; }

    public string DocumentTypeCode { get; set; } = null!;

    public string? DocumentNo { get; set; }

    public string DocumentTitle { get; set; } = null!;

    public string FileName { get; set; } = null!;

    public string FilePath { get; set; } = null!;

    public string? MimeType { get; set; }

    public long? FileSizeBytes { get; set; }

    public bool IsPublic { get; set; }

    public string StatusCode { get; set; } = null!;

    public long? UploadedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual GmsDeceasedPerson? Deceased { get; set; }

    public virtual GmsGrave? Grave { get; set; }

    public virtual GmsAppUser? UploadedByNavigation { get; set; }
}
