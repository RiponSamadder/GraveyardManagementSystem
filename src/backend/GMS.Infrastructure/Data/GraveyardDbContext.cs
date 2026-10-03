using GMS.Core.Entities;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GMS.Infrastructure.Data;

public partial class GraveyardDbContext : DbContext
{
    public GraveyardDbContext()
    {
    }

    public GraveyardDbContext(DbContextOptions<GraveyardDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AudAuditLog> AudAuditLogs { get; set; }

    public virtual DbSet<CfgLookupType> CfgLookupTypes { get; set; }

    public virtual DbSet<CfgLookupValue> CfgLookupValues { get; set; }

    public virtual DbSet<CfgRelationshipType> CfgRelationshipTypes { get; set; }

    public virtual DbSet<CfgSystemSetting> CfgSystemSettings { get; set; }

    public virtual DbSet<FinDonation> FinDonations { get; set; }

    public virtual DbSet<FinDonor> FinDonors { get; set; }

    public virtual DbSet<FinExpense> FinExpenses { get; set; }

    public virtual DbSet<FinFinancialAccount> FinFinancialAccounts { get; set; }

    public virtual DbSet<FinFinancialVoucher> FinFinancialVouchers { get; set; }

    public virtual DbSet<FinFund> FinFunds { get; set; }

    public virtual DbSet<FinIncome> FinIncomes { get; set; }

    public virtual DbSet<GmsAppUser> GmsAppUsers { get; set; }

    public virtual DbSet<GmsBurial> GmsBurials { get; set; }

    public virtual DbSet<GmsCommittee> GmsCommittees { get; set; }

    public virtual DbSet<GmsCommitteeMeeting> GmsCommitteeMeetings { get; set; }

    public virtual DbSet<GmsCommitteeMember> GmsCommitteeMembers { get; set; }

    public virtual DbSet<GmsCommitteeResolution> GmsCommitteeResolutions { get; set; }

    public virtual DbSet<GmsDeceasedPerson> GmsDeceasedPeople { get; set; }

    public virtual DbSet<GmsEvent> GmsEvents { get; set; }

    public virtual DbSet<GmsGrave> GmsGraves { get; set; }

    public virtual DbSet<GmsGraveBlock> GmsGraveBlocks { get; set; }

    public virtual DbSet<GmsGraveMaintenance> GmsGraveMaintenances { get; set; }

    public virtual DbSet<GmsGraveRow> GmsGraveRows { get; set; }

    public virtual DbSet<GmsGraveyard> GmsGraveyards { get; set; }

    public virtual DbSet<GmsGraveyardSection> GmsGraveyardSections { get; set; }

    public virtual DbSet<GmsMeetingParticipant> GmsMeetingParticipants { get; set; }

    public virtual DbSet<GmsNotice> GmsNotices { get; set; }

    public virtual DbSet<GmsNotification> GmsNotifications { get; set; }

    public virtual DbSet<GmsOrganization> GmsOrganizations { get; set; }

    public virtual DbSet<GmsPermission> GmsPermissions { get; set; }

    public virtual DbSet<GmsQrcode> GmsQrcodes { get; set; }

    public virtual DbSet<GmsQrscanLog> GmsQrscanLogs { get; set; }

    public virtual DbSet<GmsRole> GmsRoles { get; set; }

    public virtual DbSet<GmsRolePermission> GmsRolePermissions { get; set; }

    public virtual DbSet<GmsUserRole> GmsUserRoles { get; set; }

    public virtual DbSet<GmsWorker> GmsWorkers { get; set; }

    public virtual DbSet<MemDeceasedRelationship> MemDeceasedRelationships { get; set; }

    public virtual DbSet<MemDocument> MemDocuments { get; set; }

    public virtual DbSet<MemFeedback> MemFeedbacks { get; set; }

    public virtual DbSet<MemLifeLesson> MemLifeLessons { get; set; }

    public virtual DbSet<MemLifeTimeline> MemLifeTimelines { get; set; }

    public virtual DbSet<MemMediaAlbum> MemMediaAlbums { get; set; }

    public virtual DbSet<MemMedium> MemMedia { get; set; }

    public virtual DbSet<MemMemorialProfile> MemMemorialProfiles { get; set; }

    public virtual DbSet<MemMemory> MemMemories { get; set; }

    public virtual DbSet<MemTribute> MemTributes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-B6E86RN;Database=Graveyard;Integrated Security=True;TrustServerCertificate=True;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AudAuditLog>(entity =>
        {
            entity.HasKey(e => e.AuditLogId);
            entity.ToTable("AUD_AuditLog");
            entity.Property(e => e.AuditLogId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<CfgLookupType>(entity =>
        {
            entity.HasKey(e => e.LookupTypeId).HasName("PK_LookupType");

            entity.ToTable("CFG_LookupType");

            entity.HasIndex(e => e.LookupTypeCode, "UQ_LookupType_Code").IsUnique();

            entity.Property(e => e.LookupTypeCode).HasMaxLength(50);
            entity.Property(e => e.LookupTypeName).HasMaxLength(100);
        });

        modelBuilder.Entity<CfgLookupValue>(entity =>
        {
            entity.HasKey(e => e.LookupValueId).HasName("PK_LookupValue");

            entity.ToTable("CFG_LookupValue");

            entity.HasIndex(e => new { e.LookupTypeId, e.LookupCode }, "UQ_LookupValue_Code").IsUnique();

            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LookupCode).HasMaxLength(50);
            entity.Property(e => e.LookupName).HasMaxLength(150);
            entity.Property(e => e.LookupNameBn).HasMaxLength(150);

            entity.HasOne(d => d.LookupType).WithMany(p => p.CfgLookupValues)
                .HasForeignKey(d => d.LookupTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LookupValue_Type");
        });

        modelBuilder.Entity<CfgRelationshipType>(entity =>
        {
            entity.HasKey(e => e.RelationshipTypeId).HasName("PK_RelationshipType");

            entity.ToTable("CFG_RelationshipType");

            entity.HasIndex(e => e.RelationshipCode, "UQ_RelationshipType_Code").IsUnique();

            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.RelationshipCode).HasMaxLength(50);
            entity.Property(e => e.RelationshipName).HasMaxLength(100);
        });

        modelBuilder.Entity<CfgSystemSetting>(entity =>
        {
            entity.HasKey(e => e.SettingId).HasName("PK_SystemSetting");

            entity.ToTable("CFG_SystemSetting");

            entity.HasIndex(e => e.SettingKey, "UQ_SystemSetting_Key").IsUnique();

            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.SettingKey).HasMaxLength(150);
        });

        modelBuilder.Entity<FinDonation>(entity =>
        {
            entity.HasKey(e => e.DonationId).HasName("PK_Donation");

            entity.ToTable("FIN_Donation");

            entity.HasIndex(e => new { e.GraveyardId, e.DonationDate }, "IX_Donation_Date");

            entity.HasIndex(e => e.DonorId, "IX_Donation_Donor");

            entity.HasIndex(e => new { e.GraveyardId, e.DonationNo }, "UQ_Donation_No").IsUnique();

            entity.HasIndex(e => new { e.GraveyardId, e.ReceiptNo }, "UQ_Donation_Receipt").IsUnique();

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.DonationNo).HasMaxLength(50);
            entity.Property(e => e.PaymentMethodCode).HasMaxLength(50);
            entity.Property(e => e.PaymentReference).HasMaxLength(150);
            entity.Property(e => e.PurposeCode).HasMaxLength(50);
            entity.Property(e => e.ReceiptNo).HasMaxLength(50);
            entity.Property(e => e.Remarks).HasMaxLength(1000);
            entity.Property(e => e.StatusCode)
                .HasMaxLength(50)
                .HasDefaultValue("POSTED");

            entity.HasOne(d => d.Account).WithMany(p => p.FinDonations)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK_Donation_Account");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.FinDonations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_Donation_CreatedBy");

            entity.HasOne(d => d.Donor).WithMany(p => p.FinDonations)
                .HasForeignKey(d => d.DonorId)
                .HasConstraintName("FK_Donation_Donor");

            entity.HasOne(d => d.Fund).WithMany(p => p.FinDonations)
                .HasForeignKey(d => d.FundId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Donation_Fund");

            entity.HasOne(d => d.Graveyard).WithMany(p => p.FinDonations)
                .HasForeignKey(d => d.GraveyardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Donation_Graveyard");
        });

        modelBuilder.Entity<FinDonor>(entity =>
        {
            entity.HasKey(e => e.DonorId).HasName("PK_Donor");

            entity.ToTable("FIN_Donor");

            entity.HasIndex(e => e.GraveyardId, "IX_Donor_Graveyard");

            entity.HasIndex(e => new { e.GraveyardId, e.DonorCode }, "UQ_Donor_Code").IsUnique();

            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.DonorCode).HasMaxLength(50);
            entity.Property(e => e.DonorName).HasMaxLength(250);
            entity.Property(e => e.DonorTypeCode).HasMaxLength(30);
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.MobileNo).HasMaxLength(50);
            entity.Property(e => e.OrganizationName).HasMaxLength(250);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.FinDonors)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_Donor_CreatedBy");

            entity.HasOne(d => d.Graveyard).WithMany(p => p.FinDonors)
                .HasForeignKey(d => d.GraveyardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Donor_Graveyard");
        });

        modelBuilder.Entity<FinExpense>(entity =>
        {
            entity.HasKey(e => e.ExpenseId).HasName("PK_Expense");

            entity.ToTable("FIN_Expense");

            entity.HasIndex(e => new { e.ApprovalStatusCode, e.PaymentStatusCode }, "IX_Expense_Approval");

            entity.HasIndex(e => new { e.GraveyardId, e.ExpenseDate }, "IX_Expense_Date");

            entity.HasIndex(e => new { e.GraveyardId, e.ExpenseNo }, "UQ_Expense_No").IsUnique();

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ApprovalStatusCode)
                .HasMaxLength(50)
                .HasDefaultValue("PENDING");
            entity.Property(e => e.ApprovedAt).HasPrecision(0);
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.ExpenseCategoryCode).HasMaxLength(50);
            entity.Property(e => e.ExpenseNo).HasMaxLength(50);
            entity.Property(e => e.PayeeName).HasMaxLength(250);
            entity.Property(e => e.PaymentStatusCode)
                .HasMaxLength(50)
                .HasDefaultValue("UNPAID");
            entity.Property(e => e.ReferenceNo).HasMaxLength(150);

            entity.HasOne(d => d.Account).WithMany(p => p.FinExpenses)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK_Expense_Account");

            entity.HasOne(d => d.ApprovedByNavigation).WithMany(p => p.FinExpenseApprovedByNavigations)
                .HasForeignKey(d => d.ApprovedBy)
                .HasConstraintName("FK_Expense_ApprovedBy");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.FinExpenseCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_Expense_CreatedBy");

            entity.HasOne(d => d.Fund).WithMany(p => p.FinExpenses)
                .HasForeignKey(d => d.FundId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Expense_Fund");

            entity.HasOne(d => d.Graveyard).WithMany(p => p.FinExpenses)
                .HasForeignKey(d => d.GraveyardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Expense_Graveyard");
        });

        modelBuilder.Entity<FinFinancialAccount>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK_FinancialAccount");

            entity.ToTable("FIN_FinancialAccount");

            entity.HasIndex(e => new { e.GraveyardId, e.AccountCode }, "UQ_FinancialAccount_Code").IsUnique();

            entity.Property(e => e.AccountCode).HasMaxLength(50);
            entity.Property(e => e.AccountName).HasMaxLength(200);
            entity.Property(e => e.AccountNumber).HasMaxLength(100);
            entity.Property(e => e.AccountTypeCode).HasMaxLength(50);
            entity.Property(e => e.BankName).HasMaxLength(200);
            entity.Property(e => e.BranchName).HasMaxLength(200);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.OpeningBalance).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Graveyard).WithMany(p => p.FinFinancialAccounts)
                .HasForeignKey(d => d.GraveyardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FinancialAccount_Graveyard");
        });

        modelBuilder.Entity<FinFinancialVoucher>(entity =>
        {
            entity.HasKey(e => e.VoucherId).HasName("PK_FinancialVoucher");

            entity.ToTable("FIN_FinancialVoucher");

            entity.HasIndex(e => new { e.GraveyardId, e.VoucherNo }, "UQ_FinancialVoucher_No").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Narration).HasMaxLength(1000);
            entity.Property(e => e.ReferenceNo).HasMaxLength(150);
            entity.Property(e => e.StatusCode)
                .HasMaxLength(50)
                .HasDefaultValue("POSTED");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.VoucherNo).HasMaxLength(50);
            entity.Property(e => e.VoucherTypeCode).HasMaxLength(30);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.FinFinancialVouchers)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_FinancialVoucher_CreatedBy");

            entity.HasOne(d => d.Graveyard).WithMany(p => p.FinFinancialVouchers)
                .HasForeignKey(d => d.GraveyardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FinancialVoucher_Graveyard");
        });

        modelBuilder.Entity<FinFund>(entity =>
        {
            entity.HasKey(e => e.FundId).HasName("PK_Fund");

            entity.ToTable("FIN_Fund");

            entity.HasIndex(e => new { e.GraveyardId, e.FundCode }, "UQ_Fund_Code").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.FundCode).HasMaxLength(50);
            entity.Property(e => e.FundName).HasMaxLength(200);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.OpeningBalance).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.FinFunds)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_Fund_CreatedBy");

            entity.HasOne(d => d.Graveyard).WithMany(p => p.FinFunds)
                .HasForeignKey(d => d.GraveyardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Fund_Graveyard");
        });

        modelBuilder.Entity<FinIncome>(entity =>
        {
            entity.HasKey(e => e.IncomeId).HasName("PK_Income");

            entity.ToTable("FIN_Income");

            entity.HasIndex(e => new { e.GraveyardId, e.IncomeDate }, "IX_Income_Date");

            entity.HasIndex(e => new { e.GraveyardId, e.IncomeNo }, "UQ_Income_No").IsUnique();

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.IncomeCategoryCode).HasMaxLength(50);
            entity.Property(e => e.IncomeNo).HasMaxLength(50);
            entity.Property(e => e.ReferenceNo).HasMaxLength(150);
            entity.Property(e => e.StatusCode)
                .HasMaxLength(50)
                .HasDefaultValue("POSTED");

            entity.HasOne(d => d.Account).WithMany(p => p.FinIncomes)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK_Income_Account");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.FinIncomes)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_Income_CreatedBy");

            entity.HasOne(d => d.Fund).WithMany(p => p.FinIncomes)
                .HasForeignKey(d => d.FundId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Income_Fund");

            entity.HasOne(d => d.Graveyard).WithMany(p => p.FinIncomes)
                .HasForeignKey(d => d.GraveyardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Income_Graveyard");
        });

        modelBuilder.Entity<GmsAppUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK_AppUser");

            entity.ToTable("GMS_AppUser");

            entity.HasIndex(e => e.Email, "UQ_AppUser_Email").IsUnique();

            entity.HasIndex(e => e.UserCode, "UQ_AppUser_UserCode").IsUnique();

            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.FullName).HasMaxLength(200);
            entity.Property(e => e.FullNameBn).HasMaxLength(200);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LastLoginAt).HasPrecision(0);
            entity.Property(e => e.MobileNo).HasMaxLength(50);
            entity.Property(e => e.ModifiedAt).HasPrecision(0);
            entity.Property(e => e.PasswordHash).HasMaxLength(500);
            entity.Property(e => e.ProfilePhotoPath).HasMaxLength(500);
            entity.Property(e => e.UserCode).HasMaxLength(30);
            entity.Property(e => e.UserName).HasMaxLength(100);
        });

        modelBuilder.Entity<GmsBurial>(entity =>
        {
            entity.HasKey(e => e.BurialId).HasName("PK_Burial");

            entity.ToTable("GMS_Burial");

            entity.HasIndex(e => e.GraveId, "IX_Burial_Grave");

            entity.HasIndex(e => e.BurialReferenceNo, "UQ_Burial_Reference").IsUnique();

            entity.HasIndex(e => e.DeceasedId, "UX_Burial_PrimaryDeceased")
                .IsUnique()
                .HasFilter("([IsPrimary]=(1))");

            entity.Property(e => e.BurialPerformedBy).HasMaxLength(300);
            entity.Property(e => e.BurialReferenceNo).HasMaxLength(50);
            entity.Property(e => e.BurialTime).HasPrecision(0);
            entity.Property(e => e.BurialTypeCode).HasMaxLength(50);
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.IsPrimary).HasDefaultValue(true);
            entity.Property(e => e.ModifiedAt).HasPrecision(0);
            entity.Property(e => e.Notes).HasMaxLength(1000);
            entity.Property(e => e.ResponsiblePerson).HasMaxLength(300);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.GmsBurialCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_Burial_CreatedBy");

            entity.HasOne(d => d.Deceased).WithOne(p => p.GmsBurial)
                .HasForeignKey<GmsBurial>(d => d.DeceasedId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Burial_Deceased");

            entity.HasOne(d => d.Grave).WithMany(p => p.GmsBurials)
                .HasForeignKey(d => d.GraveId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Burial_Grave");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.GmsBurialModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK_Burial_ModifiedBy");
        });

        modelBuilder.Entity<GmsCommittee>(entity =>
        {
            entity.HasKey(e => e.CommitteeId).HasName("PK_Committee");

            entity.ToTable("GMS_Committee");

            entity.HasIndex(e => e.GraveyardId, "IX_Committee_Graveyard");

            entity.HasIndex(e => new { e.GraveyardId, e.CommitteeCode }, "UQ_Committee_Code").IsUnique();

            entity.Property(e => e.CommitteeCode).HasMaxLength(50);
            entity.Property(e => e.CommitteeName).HasMaxLength(200);
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.StatusCode)
                .HasMaxLength(50)
                .HasDefaultValue("ACTIVE");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.GmsCommittees)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_Committee_CreatedBy");

            entity.HasOne(d => d.Graveyard).WithMany(p => p.GmsCommittees)
                .HasForeignKey(d => d.GraveyardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Committee_Graveyard");
        });

        modelBuilder.Entity<GmsCommitteeMeeting>(entity =>
        {
            entity.HasKey(e => e.MeetingId).HasName("PK_CommitteeMeeting");

            entity.ToTable("GMS_CommitteeMeeting");

            entity.HasIndex(e => e.MeetingDate, "IX_CommitteeMeeting_Date");

            entity.HasIndex(e => new { e.CommitteeId, e.MeetingNo }, "UQ_CommitteeMeeting_No").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Location).HasMaxLength(300);
            entity.Property(e => e.MeetingNo).HasMaxLength(50);
            entity.Property(e => e.MeetingTime).HasPrecision(0);
            entity.Property(e => e.StatusCode)
                .HasMaxLength(50)
                .HasDefaultValue("SCHEDULED");

            entity.HasOne(d => d.Committee).WithMany(p => p.GmsCommitteeMeetings)
                .HasForeignKey(d => d.CommitteeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CommitteeMeeting_Committee");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.GmsCommitteeMeetings)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_CommitteeMeeting_CreatedBy");
        });

        modelBuilder.Entity<GmsCommitteeMember>(entity =>
        {
            entity.HasKey(e => e.CommitteeMemberId).HasName("PK_CommitteeMember");

            entity.ToTable("GMS_CommitteeMember");

            entity.Property(e => e.DesignationCode).HasMaxLength(50);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.MemberName).HasMaxLength(200);
            entity.Property(e => e.MobileNo).HasMaxLength(50);
            entity.Property(e => e.Responsibilities).HasMaxLength(1000);

            entity.HasOne(d => d.Committee).WithMany(p => p.GmsCommitteeMembers)
                .HasForeignKey(d => d.CommitteeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CommitteeMember_Committee");

            entity.HasOne(d => d.User).WithMany(p => p.GmsCommitteeMembers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_CommitteeMember_User");
        });

        modelBuilder.Entity<GmsCommitteeResolution>(entity =>
        {
            entity.HasKey(e => e.ResolutionId).HasName("PK_CommitteeResolution");

            entity.ToTable("GMS_CommitteeResolution");

            entity.HasIndex(e => new { e.MeetingId, e.ResolutionNo }, "UQ_Resolution_No").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.ResolutionNo).HasMaxLength(50);
            entity.Property(e => e.ResponsiblePerson).HasMaxLength(200);
            entity.Property(e => e.StatusCode)
                .HasMaxLength(50)
                .HasDefaultValue("OPEN");
            entity.Property(e => e.Subject).HasMaxLength(300);

            entity.HasOne(d => d.Meeting).WithMany(p => p.GmsCommitteeResolutions)
                .HasForeignKey(d => d.MeetingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Resolution_Meeting");
        });

        modelBuilder.Entity<GmsDeceasedPerson>(entity =>
        {
            entity.HasKey(e => e.DeceasedId).HasName("PK_DeceasedPerson");

            entity.ToTable("GMS_DeceasedPerson");

            entity.HasIndex(e => e.FatherName, "IX_Deceased_Father");

            entity.HasIndex(e => new { e.GraveyardId, e.DateOfDeath }, "IX_Deceased_Graveyard_DateOfDeath");

            entity.HasIndex(e => e.FullName, "IX_Deceased_Name");

            entity.HasIndex(e => e.FullNameBn, "IX_Deceased_NameBn");

            entity.HasIndex(e => new { e.RecordStatusCode, e.VisibilityCode }, "IX_Deceased_Status");

            entity.HasIndex(e => new { e.GraveyardId, e.DeceasedCode }, "UQ_Deceased_Code").IsUnique();

            entity.Property(e => e.AddressLine1).HasMaxLength(300);
            entity.Property(e => e.AddressLine2).HasMaxLength(300);
            entity.Property(e => e.AgeAtDeathYears).HasColumnType("decimal(6, 2)");
            entity.Property(e => e.ApprovedAt).HasPrecision(0);
            entity.Property(e => e.Area).HasMaxLength(150);
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.DeathCause).HasMaxLength(1000);
            entity.Property(e => e.DeathCertificateNo).HasMaxLength(100);
            entity.Property(e => e.DeathTime).HasPrecision(0);
            entity.Property(e => e.DeceasedCode).HasMaxLength(40);
            entity.Property(e => e.District).HasMaxLength(100);
            entity.Property(e => e.FatherName).HasMaxLength(300);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(300);
            entity.Property(e => e.FullNameBn).HasMaxLength(300);
            entity.Property(e => e.GenderCode).HasMaxLength(30);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.MaritalStatusCode).HasMaxLength(50);
            entity.Property(e => e.MiddleName).HasMaxLength(100);
            entity.Property(e => e.ModifiedAt).HasPrecision(0);
            entity.Property(e => e.MotherName).HasMaxLength(300);
            entity.Property(e => e.Nationality).HasMaxLength(100);
            entity.Property(e => e.Occupation).HasMaxLength(200);
            entity.Property(e => e.PlaceOfBirth).HasMaxLength(300);
            entity.Property(e => e.PlaceOfDeath).HasMaxLength(300);
            entity.Property(e => e.PrimaryPhotoPath).HasMaxLength(500);
            entity.Property(e => e.RecordStatusCode)
                .HasMaxLength(50)
                .HasDefaultValue("DRAFT");
            entity.Property(e => e.ReligionCode).HasMaxLength(50);
            entity.Property(e => e.SpouseName).HasMaxLength(300);
            entity.Property(e => e.VerifiedAt).HasPrecision(0);
            entity.Property(e => e.VisibilityCode)
                .HasMaxLength(50)
                .HasDefaultValue("PUBLIC");

            entity.HasOne(d => d.ApprovedByNavigation).WithMany(p => p.GmsDeceasedPersonApprovedByNavigations)
                .HasForeignKey(d => d.ApprovedBy)
                .HasConstraintName("FK_Deceased_ApprovedBy");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.GmsDeceasedPersonCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_Deceased_CreatedBy");

            entity.HasOne(d => d.Graveyard).WithMany(p => p.GmsDeceasedPeople)
                .HasForeignKey(d => d.GraveyardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Deceased_Graveyard");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.GmsDeceasedPersonModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK_Deceased_ModifiedBy");

            entity.HasOne(d => d.VerifiedByNavigation).WithMany(p => p.GmsDeceasedPersonVerifiedByNavigations)
                .HasForeignKey(d => d.VerifiedBy)
                .HasConstraintName("FK_Deceased_VerifiedBy");
        });

        modelBuilder.Entity<GmsEvent>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("PK_Event");

            entity.ToTable("GMS_Event");

            entity.HasIndex(e => new { e.GraveyardId, e.StartDateTime }, "IX_Event_Date");

            entity.HasIndex(e => new { e.GraveyardId, e.EventCode }, "UQ_Event_Code").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.EndDateTime).HasPrecision(0);
            entity.Property(e => e.EventCode).HasMaxLength(50);
            entity.Property(e => e.EventName).HasMaxLength(300);
            entity.Property(e => e.EventTypeCode).HasMaxLength(50);
            entity.Property(e => e.IsPublic).HasDefaultValue(true);
            entity.Property(e => e.Location).HasMaxLength(300);
            entity.Property(e => e.Organizer).HasMaxLength(200);
            entity.Property(e => e.StartDateTime).HasPrecision(0);
            entity.Property(e => e.StatusCode)
                .HasMaxLength(50)
                .HasDefaultValue("PLANNED");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.GmsEvents)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_Event_CreatedBy");

            entity.HasOne(d => d.Graveyard).WithMany(p => p.GmsEvents)
                .HasForeignKey(d => d.GraveyardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Event_Graveyard");
        });

        modelBuilder.Entity<GmsGrave>(entity =>
        {
            entity.HasKey(e => e.GraveId).HasName("PK_Grave");

            entity.ToTable("GMS_Grave");

            entity.HasIndex(e => new { e.GraveyardId, e.GraveStatusCode }, "IX_Grave_Graveyard_Status");

            entity.HasIndex(e => e.SectionId, "IX_Grave_Section");

            entity.HasIndex(e => new { e.GraveyardId, e.GraveCode }, "UQ_Grave_Code").IsUnique();

            entity.HasIndex(e => e.QrcodeValue, "UQ_Grave_QR").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.GraveCode).HasMaxLength(50);
            entity.Property(e => e.GraveNumber).HasMaxLength(50);
            entity.Property(e => e.GraveStatusCode)
                .HasMaxLength(50)
                .HasDefaultValue("AVAILABLE");
            entity.Property(e => e.GraveTypeCode).HasMaxLength(50);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Latitude).HasColumnType("decimal(10, 7)");
            entity.Property(e => e.Longitude).HasColumnType("decimal(10, 7)");
            entity.Property(e => e.MapReference).HasMaxLength(200);
            entity.Property(e => e.ModifiedAt).HasPrecision(0);
            entity.Property(e => e.PhotoFilePath).HasMaxLength(500);
            entity.Property(e => e.QrcodeValue)
                .HasMaxLength(200)
                .HasColumnName("QRCodeValue");

            entity.HasOne(d => d.Block).WithMany(p => p.GmsGraves)
                .HasForeignKey(d => d.BlockId)
                .HasConstraintName("FK_Grave_Block");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.GmsGraveCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_Grave_CreatedBy");

            entity.HasOne(d => d.Graveyard).WithMany(p => p.GmsGraves)
                .HasForeignKey(d => d.GraveyardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Grave_Graveyard");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.GmsGraveModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK_Grave_ModifiedBy");

            entity.HasOne(d => d.Row).WithMany(p => p.GmsGraves)
                .HasForeignKey(d => d.RowId)
                .HasConstraintName("FK_Grave_Row");

            entity.HasOne(d => d.Section).WithMany(p => p.GmsGraves)
                .HasForeignKey(d => d.SectionId)
                .HasConstraintName("FK_Grave_Section");
        });

        modelBuilder.Entity<GmsGraveBlock>(entity =>
        {
            entity.HasKey(e => e.BlockId).HasName("PK_GraveBlock");

            entity.ToTable("GMS_GraveBlock");

            entity.HasIndex(e => e.SectionId, "IX_Block_Section");

            entity.HasIndex(e => new { e.SectionId, e.BlockCode }, "UQ_Block_Code").IsUnique();

            entity.Property(e => e.BlockCode).HasMaxLength(30);
            entity.Property(e => e.BlockName).HasMaxLength(150);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Section).WithMany(p => p.GmsGraveBlocks)
                .HasForeignKey(d => d.SectionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Block_Section");
        });

        modelBuilder.Entity<GmsGraveMaintenance>(entity =>
        {
            entity.HasKey(e => e.MaintenanceId).HasName("PK_GraveMaintenance");

            entity.ToTable("GMS_GraveMaintenance");

            entity.HasIndex(e => new { e.GraveId, e.MaintenanceDate }, "IX_Maintenance_Grave_Date");

            entity.Property(e => e.AfterPhotoPath).HasMaxLength(500);
            entity.Property(e => e.BeforePhotoPath).HasMaxLength(500);
            entity.Property(e => e.CostAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.StatusCode)
                .HasMaxLength(50)
                .HasDefaultValue("COMPLETED");
            entity.Property(e => e.WorkTypeCode).HasMaxLength(50);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.GmsGraveMaintenances)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_GraveMaintenance_CreatedBy");

            entity.HasOne(d => d.Grave).WithMany(p => p.GmsGraveMaintenances)
                .HasForeignKey(d => d.GraveId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GraveMaintenance_Grave");

            entity.HasOne(d => d.Worker).WithMany(p => p.GmsGraveMaintenances)
                .HasForeignKey(d => d.WorkerId)
                .HasConstraintName("FK_GraveMaintenance_Worker");
        });

        modelBuilder.Entity<GmsGraveRow>(entity =>
        {
            entity.HasKey(e => e.RowId).HasName("PK_GraveRow");

            entity.ToTable("GMS_GraveRow");

            entity.HasIndex(e => e.BlockId, "IX_Row_Block");

            entity.HasIndex(e => new { e.BlockId, e.RowCode }, "UQ_GraveRow_Code").IsUnique();

            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.RowCode).HasMaxLength(30);
            entity.Property(e => e.RowName).HasMaxLength(150);

            entity.HasOne(d => d.Block).WithMany(p => p.GmsGraveRows)
                .HasForeignKey(d => d.BlockId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GraveRow_Block");
        });

        modelBuilder.Entity<GmsGraveyard>(entity =>
        {
            entity.HasKey(e => e.GraveyardId).HasName("PK_Graveyard");

            entity.ToTable("GMS_Graveyard");

            entity.HasIndex(e => e.OrganizationId, "IX_Graveyard_Organization");

            entity.HasIndex(e => new { e.OrganizationId, e.GraveyardCode }, "UQ_Graveyard_Code").IsUnique();

            entity.Property(e => e.AddressLine1).HasMaxLength(300);
            entity.Property(e => e.AddressLine2).HasMaxLength(300);
            entity.Property(e => e.Area).HasMaxLength(150);
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.ContactEmail).HasMaxLength(200);
            entity.Property(e => e.ContactPhone).HasMaxLength(50);
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.District).HasMaxLength(100);
            entity.Property(e => e.GraveyardCode).HasMaxLength(30);
            entity.Property(e => e.GraveyardName).HasMaxLength(200);
            entity.Property(e => e.GraveyardNameBn).HasMaxLength(200);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Latitude).HasColumnType("decimal(10, 7)");
            entity.Property(e => e.LogoFilePath).HasMaxLength(500);
            entity.Property(e => e.Longitude).HasColumnType("decimal(10, 7)");
            entity.Property(e => e.ModifiedAt).HasPrecision(0);
            entity.Property(e => e.PhotoFilePath).HasMaxLength(500);
            entity.Property(e => e.PostalCode).HasMaxLength(20);
            entity.Property(e => e.VisitingHours).HasMaxLength(500);
            entity.Property(e => e.Website).HasMaxLength(300);

            entity.HasOne(d => d.Organization).WithMany(p => p.GmsGraveyards)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Graveyard_Organization");
        });

        modelBuilder.Entity<GmsGraveyardSection>(entity =>
        {
            entity.HasKey(e => e.SectionId).HasName("PK_GraveyardSection");

            entity.ToTable("GMS_GraveyardSection");

            entity.HasIndex(e => e.GraveyardId, "IX_Section_Graveyard");

            entity.HasIndex(e => new { e.GraveyardId, e.SectionCode }, "UQ_Section_Code").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedAt).HasPrecision(0);
            entity.Property(e => e.SectionCode).HasMaxLength(30);
            entity.Property(e => e.SectionName).HasMaxLength(150);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.GmsGraveyardSectionCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_Section_CreatedBy");

            entity.HasOne(d => d.Graveyard).WithMany(p => p.GmsGraveyardSections)
                .HasForeignKey(d => d.GraveyardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Section_Graveyard");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.GmsGraveyardSectionModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK_Section_ModifiedBy");
        });

        modelBuilder.Entity<GmsMeetingParticipant>(entity =>
        {
            entity.HasKey(e => e.MeetingParticipantId).HasName("PK_MeetingParticipant");

            entity.ToTable("GMS_MeetingParticipant");

            entity.HasIndex(e => new { e.MeetingId, e.CommitteeMemberId }, "UQ_MeetingParticipant").IsUnique();

            entity.Property(e => e.AttendanceStatusCode)
                .HasMaxLength(30)
                .HasDefaultValue("PRESENT");
            entity.Property(e => e.Remarks).HasMaxLength(500);

            entity.HasOne(d => d.CommitteeMember).WithMany(p => p.GmsMeetingParticipants)
                .HasForeignKey(d => d.CommitteeMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MeetingParticipant_Member");

            entity.HasOne(d => d.Meeting).WithMany(p => p.GmsMeetingParticipants)
                .HasForeignKey(d => d.MeetingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MeetingParticipant_Meeting");
        });

        modelBuilder.Entity<GmsNotice>(entity =>
        {
            entity.HasKey(e => e.NoticeId).HasName("PK_Notice");

            entity.ToTable("GMS_Notice");

            entity.HasIndex(e => new { e.GraveyardId, e.IsPublic, e.PublishDate }, "IX_Notice_Public_Date");

            entity.HasIndex(e => new { e.GraveyardId, e.NoticeNo }, "UQ_Notice_No").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.IsPublic).HasDefaultValue(true);
            entity.Property(e => e.NoticeNo).HasMaxLength(50);
            entity.Property(e => e.PriorityCode).HasMaxLength(30);
            entity.Property(e => e.StatusCode)
                .HasMaxLength(50)
                .HasDefaultValue("PUBLISHED");
            entity.Property(e => e.Title).HasMaxLength(300);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.GmsNotices)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_Notice_CreatedBy");

            entity.HasOne(d => d.Graveyard).WithMany(p => p.GmsNotices)
                .HasForeignKey(d => d.GraveyardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Notice_Graveyard");
        });

        modelBuilder.Entity<GmsNotification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK_Notification");

            entity.ToTable("GMS_Notification");

            entity.HasIndex(e => new { e.UserId, e.IsRead, e.CreatedAt }, "IX_Notification_User_Read");

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.NotificationType).HasMaxLength(50);
            entity.Property(e => e.ReadAt).HasPrecision(0);
            entity.Property(e => e.ReferenceTable).HasMaxLength(100);
            entity.Property(e => e.Title).HasMaxLength(300);

            entity.HasOne(d => d.User).WithMany(p => p.GmsNotifications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Notification_User");
        });

        modelBuilder.Entity<GmsOrganization>(entity =>
        {
            entity.HasKey(e => e.OrganizationId).HasName("PK_Organization");

            entity.ToTable("GMS_Organization");

            entity.HasIndex(e => e.OrganizationCode, "UQ_Organization_Code").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedAt).HasPrecision(0);
            entity.Property(e => e.OrganizationCode).HasMaxLength(30);
            entity.Property(e => e.OrganizationName).HasMaxLength(200);
        });

        modelBuilder.Entity<GmsPermission>(entity =>
        {
            entity.HasKey(e => e.PermissionId).HasName("PK_Permission");

            entity.ToTable("GMS_Permission");

            entity.HasIndex(e => e.PermissionCode, "UQ_Permission_Code").IsUnique();

            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.ModuleName).HasMaxLength(100);
            entity.Property(e => e.PermissionCode).HasMaxLength(100);
            entity.Property(e => e.PermissionName).HasMaxLength(150);
        });

        modelBuilder.Entity<GmsQrcode>(entity =>
        {
            entity.HasKey(e => e.QrcodeId).HasName("PK_QRCode");

            entity.ToTable("GMS_QRCode");

            entity.HasIndex(e => e.QrcodeValue, "UQ_QRCode_Value").IsUnique();

            entity.Property(e => e.QrcodeId).HasColumnName("QRCodeId");
            entity.Property(e => e.FilePath).HasMaxLength(1000);
            entity.Property(e => e.GeneratedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.PublicUrl).HasMaxLength(1000);
            entity.Property(e => e.QrcodeValue)
                .HasMaxLength(200)
                .HasColumnName("QRCodeValue");

            entity.HasOne(d => d.GeneratedByNavigation).WithMany(p => p.GmsQrcodes)
                .HasForeignKey(d => d.GeneratedBy)
                .HasConstraintName("FK_QRCode_GeneratedBy");

            entity.HasOne(d => d.Grave).WithMany(p => p.GmsQrcodes)
                .HasForeignKey(d => d.GraveId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QRCode_Grave");
        });

        modelBuilder.Entity<GmsQrscanLog>(entity =>
        {
            entity.HasKey(e => e.QrscanLogId).HasName("PK_QRScanLog");

            entity.ToTable("GMS_QRScanLog");

            entity.HasIndex(e => new { e.QrcodeId, e.ScannedAt }, "IX_QRScanLog_QRCode_Date");

            entity.Property(e => e.QrscanLogId).HasColumnName("QRScanLogId");
            entity.Property(e => e.Ipaddress)
                .HasMaxLength(64)
                .HasColumnName("IPAddress");
            entity.Property(e => e.Latitude).HasColumnType("decimal(10, 7)");
            entity.Property(e => e.Longitude).HasColumnType("decimal(10, 7)");
            entity.Property(e => e.QrcodeId).HasColumnName("QRCodeId");
            entity.Property(e => e.ScannedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.UserAgent).HasMaxLength(1000);

            entity.HasOne(d => d.Qrcode).WithMany(p => p.GmsQrscanLogs)
                .HasForeignKey(d => d.QrcodeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QRScanLog_QRCode");

            entity.HasOne(d => d.User).WithMany(p => p.GmsQrscanLogs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_QRScanLog_User");
        });

        modelBuilder.Entity<GmsRole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK_Role");

            entity.ToTable("GMS_Role");

            entity.HasIndex(e => e.RoleCode, "UQ_Role_Code").IsUnique();

            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.RoleCode).HasMaxLength(50);
            entity.Property(e => e.RoleName).HasMaxLength(100);
        });

        modelBuilder.Entity<GmsRolePermission>(entity =>
        {
            entity.HasKey(e => e.RolePermissionId).HasName("PK_RolePermission");

            entity.ToTable("GMS_RolePermission");

            entity.HasIndex(e => new { e.RoleId, e.PermissionId }, "UQ_RolePermission").IsUnique();

            entity.Property(e => e.IsAllowed).HasDefaultValue(true);

            entity.HasOne(d => d.Permission).WithMany(p => p.GmsRolePermissions)
                .HasForeignKey(d => d.PermissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RolePermission_Permission");

            entity.HasOne(d => d.Role).WithMany(p => p.GmsRolePermissions)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RolePermission_Role");
        });

        modelBuilder.Entity<GmsUserRole>(entity =>
        {
            entity.HasKey(e => e.UserRoleId).HasName("PK_UserRole");

            entity.ToTable("GMS_UserRole");

            entity.HasIndex(e => e.GraveyardId, "IX_UserRole_Graveyard");

            entity.HasIndex(e => e.UserId, "IX_UserRole_User");

            entity.HasIndex(e => new { e.UserId, e.RoleId, e.GraveyardId }, "UQ_UserRole").IsUnique();

            entity.Property(e => e.AssignedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.AssignedByNavigation).WithMany(p => p.GmsUserRoleAssignedByNavigations)
                .HasForeignKey(d => d.AssignedBy)
                .HasConstraintName("FK_UserRole_AssignedBy");

            entity.HasOne(d => d.Graveyard).WithMany(p => p.GmsUserRoles)
                .HasForeignKey(d => d.GraveyardId)
                .HasConstraintName("FK_UserRole_Graveyard");

            entity.HasOne(d => d.Role).WithMany(p => p.GmsUserRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRole_Role");

            entity.HasOne(d => d.User).WithMany(p => p.GmsUserRoleUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRole_User");
        });

        modelBuilder.Entity<GmsWorker>(entity =>
        {
            entity.HasKey(e => e.WorkerId).HasName("PK_Worker");

            entity.ToTable("GMS_Worker");

            entity.HasIndex(e => new { e.GraveyardId, e.WorkerCode }, "UQ_Worker_Code").IsUnique();

            entity.Property(e => e.Designation).HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.MobileNo).HasMaxLength(50);
            entity.Property(e => e.SalaryAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.WorkerCode).HasMaxLength(50);
            entity.Property(e => e.WorkerName).HasMaxLength(200);

            entity.HasOne(d => d.Graveyard).WithMany(p => p.GmsWorkers)
                .HasForeignKey(d => d.GraveyardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Worker_Graveyard");
        });

        modelBuilder.Entity<MemDeceasedRelationship>(entity =>
        {
            entity.HasKey(e => e.DeceasedRelationshipId).HasName("PK_DeceasedRelationship");

            entity.ToTable("MEM_DeceasedRelationship");

            entity.HasIndex(e => e.DeceasedId, "IX_Relationship_Deceased");

            entity.HasIndex(e => e.RelatedDeceasedId, "IX_Relationship_RelatedDeceased");

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.IsPublic).HasDefaultValue(true);
            entity.Property(e => e.Notes).HasMaxLength(500);
            entity.Property(e => e.RelationshipName).HasMaxLength(100);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.MemDeceasedRelationshipCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_DeceasedRelationship_CreatedBy");

            entity.HasOne(d => d.Deceased).WithMany(p => p.MemDeceasedRelationshipDeceaseds)
                .HasForeignKey(d => d.DeceasedId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DeceasedRelationship_Deceased");

            entity.HasOne(d => d.RelatedDeceased).WithMany(p => p.MemDeceasedRelationshipRelatedDeceaseds)
                .HasForeignKey(d => d.RelatedDeceasedId)
                .HasConstraintName("FK_DeceasedRelationship_RelatedDeceased");

            entity.HasOne(d => d.RelatedUser).WithMany(p => p.MemDeceasedRelationshipRelatedUsers)
                .HasForeignKey(d => d.RelatedUserId)
                .HasConstraintName("FK_DeceasedRelationship_User");

            entity.HasOne(d => d.RelationshipType).WithMany(p => p.MemDeceasedRelationships)
                .HasForeignKey(d => d.RelationshipTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DeceasedRelationship_Type");
        });

        modelBuilder.Entity<MemDocument>(entity =>
        {
            entity.HasKey(e => e.DocumentId).HasName("PK_Document");

            entity.ToTable("MEM_Document");

            entity.HasIndex(e => e.DeceasedId, "IX_Document_Deceased");

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.DocumentNo).HasMaxLength(100);
            entity.Property(e => e.DocumentTitle).HasMaxLength(300);
            entity.Property(e => e.DocumentTypeCode).HasMaxLength(50);
            entity.Property(e => e.FileName).HasMaxLength(300);
            entity.Property(e => e.FilePath).HasMaxLength(1000);
            entity.Property(e => e.MimeType).HasMaxLength(100);
            entity.Property(e => e.StatusCode)
                .HasMaxLength(50)
                .HasDefaultValue("ACTIVE");

            entity.HasOne(d => d.Deceased).WithMany(p => p.MemDocuments)
                .HasForeignKey(d => d.DeceasedId)
                .HasConstraintName("FK_Document_Deceased");

            entity.HasOne(d => d.Grave).WithMany(p => p.MemDocuments)
                .HasForeignKey(d => d.GraveId)
                .HasConstraintName("FK_Document_Grave");

            entity.HasOne(d => d.UploadedByNavigation).WithMany(p => p.MemDocuments)
                .HasForeignKey(d => d.UploadedBy)
                .HasConstraintName("FK_Document_UploadedBy");
        });

        modelBuilder.Entity<MemFeedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK_Feedback");

            entity.ToTable("MEM_Feedback");

            entity.HasIndex(e => e.StatusCode, "IX_Feedback_Status");

            entity.Property(e => e.ContactEmail).HasMaxLength(200);
            entity.Property(e => e.ContactMobile).HasMaxLength(50);
            entity.Property(e => e.ContactName).HasMaxLength(200);
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.FeedbackTypeCode).HasMaxLength(50);
            entity.Property(e => e.ResolutionRemarks).HasMaxLength(1000);
            entity.Property(e => e.ResolvedAt).HasPrecision(0);
            entity.Property(e => e.StatusCode)
                .HasMaxLength(50)
                .HasDefaultValue("NEW");
            entity.Property(e => e.Subject).HasMaxLength(300);

            entity.HasOne(d => d.AssignedToNavigation).WithMany(p => p.MemFeedbackAssignedToNavigations)
                .HasForeignKey(d => d.AssignedTo)
                .HasConstraintName("FK_Feedback_AssignedTo");

            entity.HasOne(d => d.Deceased).WithMany(p => p.MemFeedbacks)
                .HasForeignKey(d => d.DeceasedId)
                .HasConstraintName("FK_Feedback_Deceased");

            entity.HasOne(d => d.User).WithMany(p => p.MemFeedbackUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Feedback_User");
        });

        modelBuilder.Entity<MemLifeLesson>(entity =>
        {
            entity.HasKey(e => e.LifeLessonId).HasName("PK_LifeLesson");

            entity.ToTable("MEM_LifeLesson");

            entity.Property(e => e.ApprovedAt).HasPrecision(0);
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.IsPublic).HasDefaultValue(true);
            entity.Property(e => e.LessonCategory).HasMaxLength(100);
            entity.Property(e => e.LessonTypeCode).HasMaxLength(50);
            entity.Property(e => e.ModifiedAt).HasPrecision(0);
            entity.Property(e => e.StatusCode)
                .HasMaxLength(50)
                .HasDefaultValue("DRAFT");
            entity.Property(e => e.Title).HasMaxLength(300);

            entity.HasOne(d => d.ApprovedByNavigation).WithMany(p => p.MemLifeLessonApprovedByNavigations)
                .HasForeignKey(d => d.ApprovedBy)
                .HasConstraintName("FK_LifeLesson_ApprovedBy");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.MemLifeLessonCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_LifeLesson_CreatedBy");

            entity.HasOne(d => d.Deceased).WithMany(p => p.MemLifeLessons)
                .HasForeignKey(d => d.DeceasedId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LifeLesson_Deceased");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.MemLifeLessonModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK_LifeLesson_ModifiedBy");
        });

        modelBuilder.Entity<MemLifeTimeline>(entity =>
        {
            entity.HasKey(e => e.TimelineId).HasName("PK_LifeTimeline");

            entity.ToTable("MEM_LifeTimeline");

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.EventTitle).HasMaxLength(300);
            entity.Property(e => e.IsPublic).HasDefaultValue(true);
            entity.Property(e => e.ModifiedAt).HasPrecision(0);
            entity.Property(e => e.StatusCode)
                .HasMaxLength(50)
                .HasDefaultValue("APPROVED");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.MemLifeTimelineCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_LifeTimeline_CreatedBy");

            entity.HasOne(d => d.Deceased).WithMany(p => p.MemLifeTimelines)
                .HasForeignKey(d => d.DeceasedId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LifeTimeline_Deceased");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.MemLifeTimelineModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK_LifeTimeline_ModifiedBy");
        });

        modelBuilder.Entity<MemMediaAlbum>(entity =>
        {
            entity.HasKey(e => e.AlbumId).HasName("PK_MediaAlbum");

            entity.ToTable("MEM_MediaAlbum");

            entity.Property(e => e.AlbumName).HasMaxLength(200);
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.IsPublic).HasDefaultValue(true);
            entity.Property(e => e.StatusCode)
                .HasMaxLength(50)
                .HasDefaultValue("DRAFT");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.MemMediaAlbums)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_MediaAlbum_CreatedBy");

            entity.HasOne(d => d.Deceased).WithMany(p => p.MemMediaAlbums)
                .HasForeignKey(d => d.DeceasedId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MediaAlbum_Deceased");
        });

        modelBuilder.Entity<MemMedium>(entity =>
        {
            entity.HasKey(e => e.MediaId).HasName("PK_Media");

            entity.ToTable("MEM_Media");

            entity.HasIndex(e => e.DeceasedId, "IX_Media_Deceased");

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.FileName).HasMaxLength(300);
            entity.Property(e => e.FilePath).HasMaxLength(1000);
            entity.Property(e => e.MediaTypeCode).HasMaxLength(30);
            entity.Property(e => e.MimeType).HasMaxLength(100);
            entity.Property(e => e.ReviewedAt).HasPrecision(0);
            entity.Property(e => e.StatusCode)
                .HasMaxLength(50)
                .HasDefaultValue("PENDING");
            entity.Property(e => e.Title).HasMaxLength(300);

            entity.HasOne(d => d.Album).WithMany(p => p.MemMedia)
                .HasForeignKey(d => d.AlbumId)
                .HasConstraintName("FK_Media_Album");

            entity.HasOne(d => d.Deceased).WithMany(p => p.MemMedia)
                .HasForeignKey(d => d.DeceasedId)
                .HasConstraintName("FK_Media_Deceased");

            entity.HasOne(d => d.ReviewedByNavigation).WithMany(p => p.MemMediumReviewedByNavigations)
                .HasForeignKey(d => d.ReviewedBy)
                .HasConstraintName("FK_Media_ReviewedBy");

            entity.HasOne(d => d.UploadedByNavigation).WithMany(p => p.MemMediumUploadedByNavigations)
                .HasForeignKey(d => d.UploadedBy)
                .HasConstraintName("FK_Media_UploadedBy");
        });

        modelBuilder.Entity<MemMemorialProfile>(entity =>
        {
            entity.HasKey(e => e.MemorialProfileId).HasName("PK_MemorialProfile");

            entity.ToTable("MEM_MemorialProfile");

            entity.HasIndex(e => e.DeceasedId, "UQ_Memorial_Deceased").IsUnique();

            entity.HasIndex(e => e.MemorialSlug, "UQ_Memorial_Slug").IsUnique();

            entity.Property(e => e.ApprovedAt).HasPrecision(0);
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.MemorialSlug).HasMaxLength(300);
            entity.Property(e => e.MemorialTitle).HasMaxLength(300);
            entity.Property(e => e.ModifiedAt).HasPrecision(0);
            entity.Property(e => e.StatusCode)
                .HasMaxLength(50)
                .HasDefaultValue("DRAFT");
            entity.Property(e => e.VisibilityCode)
                .HasMaxLength(50)
                .HasDefaultValue("PUBLIC");

            entity.HasOne(d => d.ApprovedByNavigation).WithMany(p => p.MemMemorialProfileApprovedByNavigations)
                .HasForeignKey(d => d.ApprovedBy)
                .HasConstraintName("FK_Memorial_ApprovedBy");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.MemMemorialProfileCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_Memorial_CreatedBy");

            entity.HasOne(d => d.Deceased).WithOne(p => p.MemMemorialProfile)
                .HasForeignKey<MemMemorialProfile>(d => d.DeceasedId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Memorial_Deceased");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.MemMemorialProfileModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK_Memorial_ModifiedBy");
        });

        modelBuilder.Entity<MemMemory>(entity =>
        {
            entity.HasKey(e => e.MemoryId).HasName("PK_Memory");

            entity.ToTable("MEM_Memory");

            entity.HasIndex(e => new { e.DeceasedId, e.StatusCode, e.IsPublic }, "IX_Memory_Deceased_Status");

            entity.Property(e => e.AuthorName).HasMaxLength(200);
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.ModifiedAt).HasPrecision(0);
            entity.Property(e => e.RelationshipToDeceased).HasMaxLength(150);
            entity.Property(e => e.ReviewRemarks).HasMaxLength(1000);
            entity.Property(e => e.ReviewedAt).HasPrecision(0);
            entity.Property(e => e.StatusCode)
                .HasMaxLength(50)
                .HasDefaultValue("SUBMITTED");
            entity.Property(e => e.Title).HasMaxLength(300);

            entity.HasOne(d => d.Deceased).WithMany(p => p.MemMemories)
                .HasForeignKey(d => d.DeceasedId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Memory_Deceased");

            entity.HasOne(d => d.ReviewedByNavigation).WithMany(p => p.MemMemoryReviewedByNavigations)
                .HasForeignKey(d => d.ReviewedBy)
                .HasConstraintName("FK_Memory_ReviewedBy");

            entity.HasOne(d => d.User).WithMany(p => p.MemMemoryUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Memory_User");
        });

        modelBuilder.Entity<MemTribute>(entity =>
        {
            entity.HasKey(e => e.TributeId).HasName("PK_Tribute");

            entity.ToTable("MEM_Tribute");

            entity.HasIndex(e => new { e.DeceasedId, e.StatusCode, e.IsPublic }, "IX_Tribute_Deceased_Status");

            entity.Property(e => e.AuthorName).HasMaxLength(200);
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.ReviewRemarks).HasMaxLength(1000);
            entity.Property(e => e.ReviewedAt).HasPrecision(0);
            entity.Property(e => e.StatusCode)
                .HasMaxLength(50)
                .HasDefaultValue("SUBMITTED");

            entity.HasOne(d => d.Deceased).WithMany(p => p.MemTributes)
                .HasForeignKey(d => d.DeceasedId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tribute_Deceased");

            entity.HasOne(d => d.ReviewedByNavigation).WithMany(p => p.MemTributeReviewedByNavigations)
                .HasForeignKey(d => d.ReviewedBy)
                .HasConstraintName("FK_Tribute_ReviewedBy");

            entity.HasOne(d => d.User).WithMany(p => p.MemTributeUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Tribute_User");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}




