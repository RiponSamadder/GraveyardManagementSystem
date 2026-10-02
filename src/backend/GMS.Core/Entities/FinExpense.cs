using System;
using System.Collections.Generic;

namespace GMS.Core.Entities;

public partial class FinExpense
{
    public long ExpenseId { get; set; }

    public long GraveyardId { get; set; }

    public string ExpenseNo { get; set; } = null!;

    public long FundId { get; set; }

    public long? AccountId { get; set; }

    public DateOnly ExpenseDate { get; set; }

    public string ExpenseCategoryCode { get; set; } = null!;

    public decimal Amount { get; set; }

    public string? PayeeName { get; set; }

    public string? ReferenceNo { get; set; }

    public string? Description { get; set; }

    public string ApprovalStatusCode { get; set; } = null!;

    public long? ApprovedBy { get; set; }

    public DateTime? ApprovedAt { get; set; }

    public string PaymentStatusCode { get; set; } = null!;

    public long? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual FinFinancialAccount? Account { get; set; }

    public virtual GmsAppUser? ApprovedByNavigation { get; set; }

    public virtual GmsAppUser? CreatedByNavigation { get; set; }

    public virtual FinFund Fund { get; set; } = null!;

    public virtual GmsGraveyard Graveyard { get; set; } = null!;
}
