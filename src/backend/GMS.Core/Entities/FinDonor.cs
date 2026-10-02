using System;
using System.Collections.Generic;

namespace GMS.Core.Entities;

public partial class FinDonor
{
    public long DonorId { get; set; }

    public long GraveyardId { get; set; }

    public string DonorCode { get; set; } = null!;

    public string DonorTypeCode { get; set; } = null!;

    public string DonorName { get; set; } = null!;

    public string? OrganizationName { get; set; }

    public string? MobileNo { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public bool IsAnonymousDefault { get; set; }

    public bool IsActive { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual GmsAppUser? CreatedByNavigation { get; set; }

    public virtual ICollection<FinDonation> FinDonations { get; set; } = new List<FinDonation>();

    public virtual GmsGraveyard Graveyard { get; set; } = null!;
}
