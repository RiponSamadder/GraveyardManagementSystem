class GraveItem {
  final int graveId;
  final String graveNumber;
  final String? graveyardName;
  final String? sectionName;
  final String? blockName;
  final String graveStatus;
  final String graveType;
  final bool isPerpetual;
  final DateTime? burialDate;
  final String? deceasedName;

  GraveItem({
    required this.graveId,
    required this.graveNumber,
    this.graveyardName,
    this.sectionName,
    this.blockName,
    required this.graveStatus,
    required this.graveType,
    required this.isPerpetual,
    this.burialDate,
    this.deceasedName,
  });

  factory GraveItem.fromJson(Map<String, dynamic> json) {
    return GraveItem(
      graveId: json['graveId'] ?? 0,
      graveNumber: json['graveNumber'] ?? '',
      graveyardName: json['graveyardName'],
      sectionName: json['sectionName'],
      blockName: json['blockName'],
      graveStatus: json['graveStatus'] ?? 'AVAILABLE',
      graveType: json['graveType'] ?? 'STANDARD',
      isPerpetual: json['isPerpetual'] ?? false,
      burialDate: json['burialDate'] != null ? DateTime.tryParse(json['burialDate']) : null,
      deceasedName: json['deceasedName'],
    );
  }

  bool get isAvailable => graveStatus.toUpperCase() == 'AVAILABLE';
  bool get isOccupied => graveStatus.toUpperCase() == 'OCCUPIED';
  bool get isReserved => graveStatus.toUpperCase() == 'RESERVED';
}

class BurialItem {
  final int burialId;
  final String burialCode;
  final int graveId;
  final String graveNumber;
  final int deceasedId;
  final String deceasedName;
  final DateTime burialDate;
  final String? prayerLeaderName;
  final String? burialType;
  final String? graveCondition;

  BurialItem({
    required this.burialId,
    required this.burialCode,
    required this.graveId,
    required this.graveNumber,
    required this.deceasedId,
    required this.deceasedName,
    required this.burialDate,
    this.prayerLeaderName,
    this.burialType,
    this.graveCondition,
  });

  factory BurialItem.fromJson(Map<String, dynamic> json) {
    return BurialItem(
      burialId: json['burialId'] ?? 0,
      burialCode: json['burialCode'] ?? '',
      graveId: json['graveId'] ?? 0,
      graveNumber: json['graveNumber'] ?? '',
      deceasedId: json['deceasedId'] ?? 0,
      deceasedName: json['deceasedName'] ?? '',
      burialDate: DateTime.tryParse(json['burialDate'] ?? '') ?? DateTime.now(),
      prayerLeaderName: json['prayerLeaderName'],
      burialType: json['burialType'],
      graveCondition: json['graveCondition'],
    );
  }
}
