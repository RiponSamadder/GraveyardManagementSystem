using System;
using System.Collections.Generic;

namespace GMS.Core.Entities;

public partial class GmsAppUser
{
    public long UserId { get; set; }

    public string UserCode { get; set; } = null!;

    public string? UserName { get; set; }

    public string FullName { get; set; } = null!;

    public string? FullNameBn { get; set; }

    public string? Email { get; set; }

    public string? MobileNo { get; set; }

    public string? PasswordHash { get; set; }

    public string? ProfilePhotoPath { get; set; }

    public string? Address { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public bool IsEmailVerified { get; set; }

    public bool IsMobileVerified { get; set; }

    public bool IsActive { get; set; }

    public bool IsBlocked { get; set; }

    public DateTime? LastLoginAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual ICollection<AudAuditLog> AudAuditLogs { get; set; } = new List<AudAuditLog>();

    public virtual ICollection<FinDonation> FinDonations { get; set; } = new List<FinDonation>();

    public virtual ICollection<FinDonor> FinDonors { get; set; } = new List<FinDonor>();

    public virtual ICollection<FinExpense> FinExpenseApprovedByNavigations { get; set; } = new List<FinExpense>();

    public virtual ICollection<FinExpense> FinExpenseCreatedByNavigations { get; set; } = new List<FinExpense>();

    public virtual ICollection<FinFinancialVoucher> FinFinancialVouchers { get; set; } = new List<FinFinancialVoucher>();

    public virtual ICollection<FinFund> FinFunds { get; set; } = new List<FinFund>();

    public virtual ICollection<FinIncome> FinIncomes { get; set; } = new List<FinIncome>();

    public virtual ICollection<GmsBurial> GmsBurialCreatedByNavigations { get; set; } = new List<GmsBurial>();

    public virtual ICollection<GmsBurial> GmsBurialModifiedByNavigations { get; set; } = new List<GmsBurial>();

    public virtual ICollection<GmsCommitteeMeeting> GmsCommitteeMeetings { get; set; } = new List<GmsCommitteeMeeting>();

    public virtual ICollection<GmsCommitteeMember> GmsCommitteeMembers { get; set; } = new List<GmsCommitteeMember>();

    public virtual ICollection<GmsCommittee> GmsCommittees { get; set; } = new List<GmsCommittee>();

    public virtual ICollection<GmsDeceasedPerson> GmsDeceasedPersonApprovedByNavigations { get; set; } = new List<GmsDeceasedPerson>();

    public virtual ICollection<GmsDeceasedPerson> GmsDeceasedPersonCreatedByNavigations { get; set; } = new List<GmsDeceasedPerson>();

    public virtual ICollection<GmsDeceasedPerson> GmsDeceasedPersonModifiedByNavigations { get; set; } = new List<GmsDeceasedPerson>();

    public virtual ICollection<GmsDeceasedPerson> GmsDeceasedPersonVerifiedByNavigations { get; set; } = new List<GmsDeceasedPerson>();

    public virtual ICollection<GmsEvent> GmsEvents { get; set; } = new List<GmsEvent>();

    public virtual ICollection<GmsGrave> GmsGraveCreatedByNavigations { get; set; } = new List<GmsGrave>();

    public virtual ICollection<GmsGraveMaintenance> GmsGraveMaintenances { get; set; } = new List<GmsGraveMaintenance>();

    public virtual ICollection<GmsGrave> GmsGraveModifiedByNavigations { get; set; } = new List<GmsGrave>();

    public virtual ICollection<GmsGraveyardSection> GmsGraveyardSectionCreatedByNavigations { get; set; } = new List<GmsGraveyardSection>();

    public virtual ICollection<GmsGraveyardSection> GmsGraveyardSectionModifiedByNavigations { get; set; } = new List<GmsGraveyardSection>();

    public virtual ICollection<GmsNotice> GmsNotices { get; set; } = new List<GmsNotice>();

    public virtual ICollection<GmsNotification> GmsNotifications { get; set; } = new List<GmsNotification>();

    public virtual ICollection<GmsQrcode> GmsQrcodes { get; set; } = new List<GmsQrcode>();

    public virtual ICollection<GmsQrscanLog> GmsQrscanLogs { get; set; } = new List<GmsQrscanLog>();

    public virtual ICollection<GmsUserRole> GmsUserRoleAssignedByNavigations { get; set; } = new List<GmsUserRole>();

    public virtual ICollection<GmsUserRole> GmsUserRoleUsers { get; set; } = new List<GmsUserRole>();

    public virtual ICollection<MemDeceasedRelationship> MemDeceasedRelationshipCreatedByNavigations { get; set; } = new List<MemDeceasedRelationship>();

    public virtual ICollection<MemDeceasedRelationship> MemDeceasedRelationshipRelatedUsers { get; set; } = new List<MemDeceasedRelationship>();

    public virtual ICollection<MemDocument> MemDocuments { get; set; } = new List<MemDocument>();

    public virtual ICollection<MemFeedback> MemFeedbackAssignedToNavigations { get; set; } = new List<MemFeedback>();

    public virtual ICollection<MemFeedback> MemFeedbackUsers { get; set; } = new List<MemFeedback>();

    public virtual ICollection<MemLifeLesson> MemLifeLessonApprovedByNavigations { get; set; } = new List<MemLifeLesson>();

    public virtual ICollection<MemLifeLesson> MemLifeLessonCreatedByNavigations { get; set; } = new List<MemLifeLesson>();

    public virtual ICollection<MemLifeLesson> MemLifeLessonModifiedByNavigations { get; set; } = new List<MemLifeLesson>();

    public virtual ICollection<MemLifeTimeline> MemLifeTimelineCreatedByNavigations { get; set; } = new List<MemLifeTimeline>();

    public virtual ICollection<MemLifeTimeline> MemLifeTimelineModifiedByNavigations { get; set; } = new List<MemLifeTimeline>();

    public virtual ICollection<MemMediaAlbum> MemMediaAlbums { get; set; } = new List<MemMediaAlbum>();

    public virtual ICollection<MemMedium> MemMediumReviewedByNavigations { get; set; } = new List<MemMedium>();

    public virtual ICollection<MemMedium> MemMediumUploadedByNavigations { get; set; } = new List<MemMedium>();

    public virtual ICollection<MemMemorialProfile> MemMemorialProfileApprovedByNavigations { get; set; } = new List<MemMemorialProfile>();

    public virtual ICollection<MemMemorialProfile> MemMemorialProfileCreatedByNavigations { get; set; } = new List<MemMemorialProfile>();

    public virtual ICollection<MemMemorialProfile> MemMemorialProfileModifiedByNavigations { get; set; } = new List<MemMemorialProfile>();

    public virtual ICollection<MemMemory> MemMemoryReviewedByNavigations { get; set; } = new List<MemMemory>();

    public virtual ICollection<MemMemory> MemMemoryUsers { get; set; } = new List<MemMemory>();

    public virtual ICollection<MemTribute> MemTributeReviewedByNavigations { get; set; } = new List<MemTribute>();

    public virtual ICollection<MemTribute> MemTributeUsers { get; set; } = new List<MemTribute>();
}
