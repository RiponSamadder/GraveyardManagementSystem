using System;
using System.Collections.Generic;

namespace GMS.Core.Entities;

public partial class GmsGraveyard
{
    public long GraveyardId { get; set; }

    public long OrganizationId { get; set; }

    public string GraveyardCode { get; set; } = null!;

    public string GraveyardName { get; set; } = null!;

    public string? GraveyardNameBn { get; set; }

    public string? AddressLine1 { get; set; }

    public string? AddressLine2 { get; set; }

    public string? Area { get; set; }

    public string? City { get; set; }

    public string? District { get; set; }

    public string? PostalCode { get; set; }

    public string? Country { get; set; }

    public string? ContactPhone { get; set; }

    public string? ContactEmail { get; set; }

    public string? Website { get; set; }

    public DateOnly? EstablishmentDate { get; set; }

    public decimal? Latitude { get; set; }

    public decimal? Longitude { get; set; }

    public string? Description { get; set; }

    public string? VisitingHours { get; set; }

    public string? LogoFilePath { get; set; }

    public string? PhotoFilePath { get; set; }

    public bool IsActive { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public long? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual ICollection<AudAuditLog> AudAuditLogs { get; set; } = new List<AudAuditLog>();

    public virtual ICollection<FinDonation> FinDonations { get; set; } = new List<FinDonation>();

    public virtual ICollection<FinDonor> FinDonors { get; set; } = new List<FinDonor>();

    public virtual ICollection<FinExpense> FinExpenses { get; set; } = new List<FinExpense>();

    public virtual ICollection<FinFinancialAccount> FinFinancialAccounts { get; set; } = new List<FinFinancialAccount>();

    public virtual ICollection<FinFinancialVoucher> FinFinancialVouchers { get; set; } = new List<FinFinancialVoucher>();

    public virtual ICollection<FinFund> FinFunds { get; set; } = new List<FinFund>();

    public virtual ICollection<FinIncome> FinIncomes { get; set; } = new List<FinIncome>();

    public virtual ICollection<GmsCommittee> GmsCommittees { get; set; } = new List<GmsCommittee>();

    public virtual ICollection<GmsDeceasedPerson> GmsDeceasedPeople { get; set; } = new List<GmsDeceasedPerson>();

    public virtual ICollection<GmsEvent> GmsEvents { get; set; } = new List<GmsEvent>();

    public virtual ICollection<GmsGrave> GmsGraves { get; set; } = new List<GmsGrave>();

    public virtual ICollection<GmsGraveyardSection> GmsGraveyardSections { get; set; } = new List<GmsGraveyardSection>();

    public virtual ICollection<GmsNotice> GmsNotices { get; set; } = new List<GmsNotice>();

    public virtual ICollection<GmsUserRole> GmsUserRoles { get; set; } = new List<GmsUserRole>();

    public virtual ICollection<GmsWorker> GmsWorkers { get; set; } = new List<GmsWorker>();

    public virtual GmsOrganization Organization { get; set; } = null!;
}
