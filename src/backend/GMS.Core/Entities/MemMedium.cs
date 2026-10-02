using System;
using System.Collections.Generic;

namespace GMS.Core.Entities;

public partial class MemMedium
{
    public long MediaId { get; set; }

    public long? DeceasedId { get; set; }

    public long? AlbumId { get; set; }

    public string MediaTypeCode { get; set; } = null!;

    public string FileName { get; set; } = null!;

    public string FilePath { get; set; } = null!;

    public string? MimeType { get; set; }

    public long? FileSizeBytes { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public DateOnly? CapturedDate { get; set; }

    public bool IsPublic { get; set; }

    public string StatusCode { get; set; } = null!;

    public long? UploadedBy { get; set; }

    public long? ReviewedBy { get; set; }

    public DateTime? ReviewedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual MemMediaAlbum? Album { get; set; }

    public virtual GmsDeceasedPerson? Deceased { get; set; }

    public virtual GmsAppUser? ReviewedByNavigation { get; set; }

    public virtual GmsAppUser? UploadedByNavigation { get; set; }
}
