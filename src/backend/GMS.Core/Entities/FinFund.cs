using System;
using System.Collections.Generic;

namespace GMS.Core.Entities;

public partial class FinFund
{
    public long FundId { get; set; }

    public long GraveyardId { get; set; }

    public string FundCode { get; set; } = null!;

    public string FundName { get; set; } = null!;

    public string? Description { get; set; }

    public decimal OpeningBalance { get; set; }

    public bool IsActive { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual GmsAppUser? CreatedByNavigation { get; set; }

    public virtual ICollection<FinDonation> FinDonations { get; set; } = new List<FinDonation>();

    public virtual ICollection<FinExpense> FinExpenses { get; set; } = new List<FinExpense>();

    public virtual ICollection<FinIncome> FinIncomes { get; set; } = new List<FinIncome>();

    public virtual GmsGraveyard Graveyard { get; set; } = null!;
}
