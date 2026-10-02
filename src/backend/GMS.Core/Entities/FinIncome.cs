using System;
using System.Collections.Generic;

namespace GMS.Core.Entities;

public partial class FinIncome
{
    public long IncomeId { get; set; }

    public long GraveyardId { get; set; }

    public string IncomeNo { get; set; } = null!;

    public long FundId { get; set; }

    public long? AccountId { get; set; }

    public DateOnly IncomeDate { get; set; }

    public string IncomeCategoryCode { get; set; } = null!;

    public decimal Amount { get; set; }

    public string? ReferenceNo { get; set; }

    public string? Description { get; set; }

    public string StatusCode { get; set; } = null!;

    public long? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual FinFinancialAccount? Account { get; set; }

    public virtual GmsAppUser? CreatedByNavigation { get; set; }

    public virtual FinFund Fund { get; set; } = null!;

    public virtual GmsGraveyard Graveyard { get; set; } = null!;
}
