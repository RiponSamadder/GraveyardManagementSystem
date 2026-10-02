using System;
using System.Collections.Generic;

namespace GMS.Core.Entities;

public partial class FinFinancialVoucher
{
    public long VoucherId { get; set; }

    public long GraveyardId { get; set; }

    public string VoucherNo { get; set; } = null!;

    public string VoucherTypeCode { get; set; } = null!;

    public DateOnly VoucherDate { get; set; }

    public string? ReferenceNo { get; set; }

    public string? Narration { get; set; }

    public decimal TotalAmount { get; set; }

    public string StatusCode { get; set; } = null!;

    public long? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual GmsAppUser? CreatedByNavigation { get; set; }

    public virtual GmsGraveyard Graveyard { get; set; } = null!;
}
