using System;
using System.Collections.Generic;

namespace GMS.Core.Entities;

public partial class FinDonation
{
    public long DonationId { get; set; }

    public long GraveyardId { get; set; }

    public string DonationNo { get; set; } = null!;

    public long? DonorId { get; set; }

    public long FundId { get; set; }

    public long? AccountId { get; set; }

    public DateOnly DonationDate { get; set; }

    public decimal Amount { get; set; }

    public string PaymentMethodCode { get; set; } = null!;

    public string? PaymentReference { get; set; }

    public string? PurposeCode { get; set; }

    public bool IsAnonymous { get; set; }

    public string? Remarks { get; set; }

    public string StatusCode { get; set; } = null!;

    public string? ReceiptNo { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual FinFinancialAccount? Account { get; set; }

    public virtual GmsAppUser? CreatedByNavigation { get; set; }

    public virtual FinDonor? Donor { get; set; }

    public virtual FinFund Fund { get; set; } = null!;

    public virtual GmsGraveyard Graveyard { get; set; } = null!;
}
