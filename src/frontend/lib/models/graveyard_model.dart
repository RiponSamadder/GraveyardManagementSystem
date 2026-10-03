class GraveyardItem {
  final int graveyardId;
  final String graveyardCode;
  final String graveyardName;
  final String? graveyardNameBn;
  final String? city;
  final String? district;
  final bool isActive;
  final int totalGraves;

  GraveyardItem({
    required this.graveyardId,
    required this.graveyardCode,
    required this.graveyardName,
    this.graveyardNameBn,
    this.city,
    this.district,
    required this.isActive,
    required this.totalGraves,
  });

  factory GraveyardItem.fromJson(Map<String, dynamic> json) {
    return GraveyardItem(
      graveyardId: json['graveyardId'] ?? 0,
      graveyardCode: json['graveyardCode'] ?? '',
      graveyardName: json['graveyardName'] ?? '',
      graveyardNameBn: json['graveyardNameBn'],
      city: json['city'],
      district: json['district'],
      isActive: json['isActive'] ?? true,
      totalGraves: json['totalGraves'] ?? 0,
    );
  }
}

class GraveyardDetail {
  final int graveyardId;
  final String graveyardCode;
  final String graveyardName;
  final String? graveyardNameBn;
  final String? address;
  final String? city;
  final String? district;
  final double? totalAreaAcres;
  final int? totalCapacity;
  final double? latitude;
  final double? longitude;
  final bool isActive;
  final DateTime createdAt;
  final List<SectionItem> sections;

  GraveyardDetail({
    required this.graveyardId,
    required this.graveyardCode,
    required this.graveyardName,
    this.graveyardNameBn,
    this.address,
    this.city,
    this.district,
    this.totalAreaAcres,
    this.totalCapacity,
    this.latitude,
    this.longitude,
    required this.isActive,
    required this.createdAt,
    required this.sections,
  });

  factory GraveyardDetail.fromJson(Map<String, dynamic> json) {
    return GraveyardDetail(
      graveyardId: json['graveyardId'] ?? 0,
      graveyardCode: json['graveyardCode'] ?? '',
      graveyardName: json['graveyardName'] ?? '',
      graveyardNameBn: json['graveyardNameBn'],
      address: json['address'],
      city: json['city'],
      district: json['district'],
      totalAreaAcres: (json['totalAreaAcres'] as num?)?.toDouble(),
      totalCapacity: json['totalCapacity'],
      latitude: (json['latitude'] as num?)?.toDouble(),
      longitude: (json['longitude'] as num?)?.toDouble(),
      isActive: json['isActive'] ?? true,
      createdAt: DateTime.tryParse(json['createdAt'] ?? '') ?? DateTime.now(),
      sections: (json['sections'] as List<dynamic>?)
              ?.map((e) => SectionItem.fromJson(e as Map<String, dynamic>))
              .toList() ??
          [],
    );
  }
}

class SectionItem {
  final int sectionId;
  final int graveyardId;
  final String sectionCode;
  final String sectionName;
  final String? sectionNameBn;
  final String? sectionType;
  final int? capacity;
  final bool isActive;
  final List<BlockItem> blocks;

  SectionItem({
    required this.sectionId,
    required this.graveyardId,
    required this.sectionCode,
    required this.sectionName,
    this.sectionNameBn,
    this.sectionType,
    this.capacity,
    required this.isActive,
    required this.blocks,
  });

  factory SectionItem.fromJson(Map<String, dynamic> json) {
    return SectionItem(
      sectionId: json['sectionId'] ?? 0,
      graveyardId: json['graveyardId'] ?? 0,
      sectionCode: json['sectionCode'] ?? '',
      sectionName: json['sectionName'] ?? '',
      sectionNameBn: json['sectionNameBn'],
      sectionType: json['sectionType'],
      capacity: json['capacity'],
      isActive: json['isActive'] ?? true,
      blocks: (json['blocks'] as List<dynamic>?)
              ?.map((e) => BlockItem.fromJson(e as Map<String, dynamic>))
              .toList() ??
          [],
    );
  }
}

class BlockItem {
  final int blockId;
  final int sectionId;
  final String blockCode;
  final String blockName;
  final int? capacity;
  final bool isActive;

  BlockItem({
    required this.blockId,
    required this.sectionId,
    required this.blockCode,
    required this.blockName,
    this.capacity,
    required this.isActive,
  });

  factory BlockItem.fromJson(Map<String, dynamic> json) {
    return BlockItem(
      blockId: json['blockId'] ?? 0,
      sectionId: json['sectionId'] ?? 0,
      blockCode: json['blockCode'] ?? '',
      blockName: json['blockName'] ?? '',
      capacity: json['capacity'],
      isActive: json['isActive'] ?? true,
    );
  }
}
