using System;
using System.Collections.Generic;

namespace GMS.Core.Entities;

public partial class MemMediaAlbum
{
    public long AlbumId { get; set; }

    public long DeceasedId { get; set; }

    public string AlbumName { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsPublic { get; set; }

    public string StatusCode { get; set; } = null!;

    public long? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual GmsAppUser? CreatedByNavigation { get; set; }

    public virtual GmsDeceasedPerson Deceased { get; set; } = null!;

    public virtual ICollection<MemMedium> MemMedia { get; set; } = new List<MemMedium>();
}
