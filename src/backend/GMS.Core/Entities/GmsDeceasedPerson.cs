using System;
using System.Collections.Generic;

namespace GMS.Core.Entities;

public partial class GmsDeceasedPerson
{
    public long DeceasedId { get; set; }

    public long GraveyardId { get; set; }

    public string DeceasedCode { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? MiddleName { get; set; }

    public string? LastName { get; set; }

    public string FullName { get; set; } = null!;

    public string? FullNameBn { get; set; }

    public string? GenderCode { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public short? ApproximateBirthYear { get; set; }

    public DateOnly DateOfDeath { get; set; }

    public TimeOnly? DeathTime { get; set; }

    public string? PlaceOfBirth { get; set; }

    public string? PlaceOfDeath { get; set; }

    public string? Nationality { get; set; }

    public string? Occupation { get; set; }

    public string? ReligionCode { get; set; }

    public string? MaritalStatusCode { get; set; }

    public string? FatherName { get; set; }

    public string? MotherName { get; set; }

    public string? SpouseName { get; set; }

    public string? AddressLine1 { get; set; }

    public string? AddressLine2 { get; set; }

    public string? Area { get; set; }

    public string? City { get; set; }

    public string? District { get; set; }

    public string? Country { get; set; }

    public decimal? AgeAtDeathYears { get; set; }

    public string? DeathCause { get; set; }

    public string? DeathCertificateNo { get; set; }

    public string? PrimaryPhotoPath { get; set; }

    public string? ShortIntroduction { get; set; }

    public string RecordStatusCode { get; set; } = null!;

    public string VisibilityCode { get; set; } = null!;

    public bool IsVerified { get; set; }

    public long? VerifiedBy { get; set; }

    public DateTime? VerifiedAt { get; set; }

    public long? ApprovedBy { get; set; }

    public DateTime? ApprovedAt { get; set; }

    public bool IsActive { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public long? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual GmsAppUser? ApprovedByNavigation { get; set; }

    public virtual GmsAppUser? CreatedByNavigation { get; set; }

    public virtual GmsBurial? GmsBurial { get; set; }

    public virtual GmsGraveyard Graveyard { get; set; } = null!;

    public virtual ICollection<MemDeceasedRelationship> MemDeceasedRelationshipDeceaseds { get; set; } = new List<MemDeceasedRelationship>();

    public virtual ICollection<MemDeceasedRelationship> MemDeceasedRelationshipRelatedDeceaseds { get; set; } = new List<MemDeceasedRelationship>();

    public virtual ICollection<MemDocument> MemDocuments { get; set; } = new List<MemDocument>();

    public virtual ICollection<MemFeedback> MemFeedbacks { get; set; } = new List<MemFeedback>();

    public virtual ICollection<MemLifeLesson> MemLifeLessons { get; set; } = new List<MemLifeLesson>();

    public virtual ICollection<MemLifeTimeline> MemLifeTimelines { get; set; } = new List<MemLifeTimeline>();

    public virtual ICollection<MemMedium> MemMedia { get; set; } = new List<MemMedium>();

    public virtual ICollection<MemMediaAlbum> MemMediaAlbums { get; set; } = new List<MemMediaAlbum>();

    public virtual MemMemorialProfile? MemMemorialProfile { get; set; }

    public virtual ICollection<MemMemory> MemMemories { get; set; } = new List<MemMemory>();

    public virtual ICollection<MemTribute> MemTributes { get; set; } = new List<MemTribute>();

    public virtual GmsAppUser? ModifiedByNavigation { get; set; }

    public virtual GmsAppUser? VerifiedByNavigation { get; set; }
}
