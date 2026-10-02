using System;
using System.Collections.Generic;

namespace GMS.Core.Entities;

public partial class FinFinancialAccount
{
    public long AccountId { get; set; }

    public long GraveyardId { get; set; }

    public string AccountCode { get; set; } = null!;

    public string AccountName { get; set; } = null!;

    public string AccountTypeCode { get; set; } = null!;

    public string? BankName { get; set; }

    public string? AccountNumber { get; set; }

    public string? BranchName { get; set; }

    public decimal OpeningBalance { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<FinDonation> FinDonations { get; set; } = new List<FinDonation>();

    public virtual ICollection<FinExpense> FinExpenses { get; set; } = new List<FinExpense>();

    public virtual ICollection<FinIncome> FinIncomes { get; set; } = new List<FinIncome>();

    public virtual GmsGraveyard Graveyard { get; set; } = null!;
}
