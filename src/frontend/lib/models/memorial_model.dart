class MemorialProfileItem {
  final int memorialId;
  final int deceasedId;
  final String deceasedName;
  final String slug;
  final String? title;
  final String? profilePhotoUrl;
  final String? coverPhotoUrl;
  final DateTime? birthDate;
  final DateTime? deathDate;
  final bool isPublic;
  final int tributeCount;
  final int viewCount;

  MemorialProfileItem({
    required this.memorialId,
    required this.deceasedId,
    required this.deceasedName,
    required this.slug,
    this.title,
    this.profilePhotoUrl,
    this.coverPhotoUrl,
    this.birthDate,
    this.deathDate,
    required this.isPublic,
    required this.tributeCount,
    required this.viewCount,
  });

  factory MemorialProfileItem.fromJson(Map<String, dynamic> json) {
    return MemorialProfileItem(
      memorialId: json['memorialId'] ?? 0,
      deceasedId: json['deceasedId'] ?? 0,
      deceasedName: json['deceasedName'] ?? '',
      slug: json['slug'] ?? '',
      title: json['title'],
      profilePhotoUrl: json['profilePhotoUrl'],
      coverPhotoUrl: json['coverPhotoUrl'],
      birthDate: json['birthDate'] != null ? DateTime.tryParse(json['birthDate']) : null,
      deathDate: json['deathDate'] != null ? DateTime.tryParse(json['deathDate']) : null,
      isPublic: json['isPublic'] ?? true,
      tributeCount: json['tributeCount'] ?? 0,
      viewCount: json['viewCount'] ?? 0,
    );
  }
}

class MemorialProfileDetail {
  final int memorialId;
  final int deceasedId;
  final String deceasedName;
  final String slug;
  final String? title;
  final String? shortBiography;
  final String? fullBiography;
  final String? profilePhotoUrl;
  final String? coverPhotoUrl;
  final DateTime? birthDate;
  final DateTime? deathDate;
  final bool isPublic;
  final bool allowTributes;
  final int tributeCount;
  final int viewCount;
  final List<TributeItem> tributes;
  final List<TimelineItem> timelineEvents;
  final List<MemoryItem> memories;

  MemorialProfileDetail({
    required this.memorialId,
    required this.deceasedId,
    required this.deceasedName,
    required this.slug,
    this.title,
    this.shortBiography,
    this.fullBiography,
    this.profilePhotoUrl,
    this.coverPhotoUrl,
    this.birthDate,
    this.deathDate,
    required this.isPublic,
    required this.allowTributes,
    required this.tributeCount,
    required this.viewCount,
    required this.tributes,
    required this.timelineEvents,
    required this.memories,
  });

  factory MemorialProfileDetail.fromJson(Map<String, dynamic> json) {
    return MemorialProfileDetail(
      memorialId: json['memorialId'] ?? 0,
      deceasedId: json['deceasedId'] ?? 0,
      deceasedName: json['deceasedName'] ?? '',
      slug: json['slug'] ?? '',
      title: json['title'],
      shortBiography: json['shortBiography'],
      fullBiography: json['fullBiography'],
      profilePhotoUrl: json['profilePhotoUrl'],
      coverPhotoUrl: json['coverPhotoUrl'],
      birthDate: json['birthDate'] != null ? DateTime.tryParse(json['birthDate']) : null,
      deathDate: json['deathDate'] != null ? DateTime.tryParse(json['deathDate']) : null,
      isPublic: json['isPublic'] ?? true,
      allowTributes: json['allowTributes'] ?? true,
      tributeCount: json['tributeCount'] ?? 0,
      viewCount: json['viewCount'] ?? 0,
      tributes: (json['tributes'] as List<dynamic>?)
              ?.map((e) => TributeItem.fromJson(e as Map<String, dynamic>))
              .toList() ??
          [],
      timelineEvents: (json['timelineEvents'] as List<dynamic>?)
              ?.map((e) => TimelineItem.fromJson(e as Map<String, dynamic>))
              .toList() ??
          [],
      memories: (json['memories'] as List<dynamic>?)
              ?.map((e) => MemoryItem.fromJson(e as Map<String, dynamic>))
              .toList() ??
          [],
    );
  }
}

class TributeItem {
  final int tributeId;
  final int memorialId;
  final String authorName;
  final String? authorEmail;
  final String? relationshipToDeceased;
  final String tributeType;
  final String? message;
  final bool isApproved;
  final DateTime createdAt;

  TributeItem({
    required this.tributeId,
    required this.memorialId,
    required this.authorName,
    this.authorEmail,
    this.relationshipToDeceased,
    required this.tributeType,
    this.message,
    required this.isApproved,
    required this.createdAt,
  });

  factory TributeItem.fromJson(Map<String, dynamic> json) {
    return TributeItem(
      tributeId: json['tributeId'] ?? 0,
      memorialId: json['memorialId'] ?? 0,
      authorName: json['authorName'] ?? '',
      authorEmail: json['authorEmail'],
      relationshipToDeceased: json['relationshipToDeceased'],
      tributeType: json['tributeType'] ?? 'MESSAGE',
      message: json['message'],
      isApproved: json['isApproved'] ?? true,
      createdAt: DateTime.tryParse(json['createdAt'] ?? '') ?? DateTime.now(),
    );
  }
}

class TimelineItem {
  final int timelineId;
  final int memorialId;
  final int eventYear;
  final String title;
  final String? description;
  final String? photoUrl;

  TimelineItem({
    required this.timelineId,
    required this.memorialId,
    required this.eventYear,
    required this.title,
    this.description,
    this.photoUrl,
  });

  factory TimelineItem.fromJson(Map<String, dynamic> json) {
    return TimelineItem(
      timelineId: json['timelineId'] ?? 0,
      memorialId: json['memorialId'] ?? 0,
      eventYear: json['eventYear'] ?? 0,
      title: json['title'] ?? '',
      description: json['description'],
      photoUrl: json['photoUrl'],
    );
  }
}

class MemoryItem {
  final int memoryId;
  final int memorialId;
  final String sharedByName;
  final String title;
  final String story;
  final String? photoUrl;
  final DateTime createdAt;

  MemoryItem({
    required this.memoryId,
    required this.memorialId,
    required this.sharedByName,
    required this.title,
    required this.story,
    this.photoUrl,
    required this.createdAt,
  });

  factory MemoryItem.fromJson(Map<String, dynamic> json) {
    return MemoryItem(
      memoryId: json['memoryId'] ?? 0,
      memorialId: json['memorialId'] ?? 0,
      sharedByName: json['sharedByName'] ?? '',
      title: json['title'] ?? '',
      story: json['story'] ?? '',
      photoUrl: json['photoUrl'],
      createdAt: DateTime.tryParse(json['createdAt'] ?? '') ?? DateTime.now(),
    );
  }
}
