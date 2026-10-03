class DeceasedItem {
  final int deceasedId;
  final String deceasedCode;
  final String fullName;
  final String? fullNameBn;
  final String? gender;
  final DateTime? dateOfBirth;
  final DateTime dateOfDeath;
  final String? causeOfDeath;
  final String verificationStatus;
  final String? nationalId;
  final String? graveNumber;
  final String? graveyardName;

  DeceasedItem({
    required this.deceasedId,
    required this.deceasedCode,
    required this.fullName,
    this.fullNameBn,
    this.gender,
    this.dateOfBirth,
    required this.dateOfDeath,
    this.causeOfDeath,
    required this.verificationStatus,
    this.nationalId,
    this.graveNumber,
    this.graveyardName,
  });

  factory DeceasedItem.fromJson(Map<String, dynamic> json) {
    return DeceasedItem(
      deceasedId: json['deceasedId'] ?? 0,
      deceasedCode: json['deceasedCode'] ?? '',
      fullName: json['fullName'] ?? '',
      fullNameBn: json['fullNameBn'],
      gender: json['gender'],
      dateOfBirth: json['dateOfBirth'] != null ? DateTime.tryParse(json['dateOfBirth']) : null,
      dateOfDeath: DateTime.tryParse(json['dateOfDeath'] ?? '') ?? DateTime.now(),
      causeOfDeath: json['causeOfDeath'],
      verificationStatus: json['verificationStatus'] ?? 'PENDING',
      nationalId: json['nationalId'],
      graveNumber: json['graveNumber'],
      graveyardName: json['graveyardName'],
    );
  }

  bool get isApproved => verificationStatus.toUpperCase() == 'APPROVED';
  bool get isVerified => verificationStatus.toUpperCase() == 'VERIFIED';
  bool get isPending => verificationStatus.toUpperCase() == 'PENDING';
}

class DeceasedDetail {
  final int deceasedId;
  final String deceasedCode;
  final String fullName;
  final String? fullNameBn;
  final String? gender;
  final DateTime? dateOfBirth;
  final DateTime dateOfDeath;
  final int? ageYears;
  final String? placeOfDeath;
  final String? causeOfDeath;
  final String? nationalId;
  final String? birthCertificateNo;
  final String? fatherName;
  final String? motherName;
  final String? spouseName;
  final String? permanentAddress;
  final String verificationStatus;
  final String? photoUrl;
  final int? graveId;
  final String? graveNumber;
  final String? graveyardName;

  DeceasedDetail({
    required this.deceasedId,
    required this.deceasedCode,
    required this.fullName,
    this.fullNameBn,
    this.gender,
    this.dateOfBirth,
    required this.dateOfDeath,
    this.ageYears,
    this.placeOfDeath,
    this.causeOfDeath,
    this.nationalId,
    this.birthCertificateNo,
    this.fatherName,
    this.motherName,
    this.spouseName,
    this.permanentAddress,
    required this.verificationStatus,
    this.photoUrl,
    this.graveId,
    this.graveNumber,
    this.graveyardName,
  });

  factory DeceasedDetail.fromJson(Map<String, dynamic> json) {
    return DeceasedDetail(
      deceasedId: json['deceasedId'] ?? 0,
      deceasedCode: json['deceasedCode'] ?? '',
      fullName: json['fullName'] ?? '',
      fullNameBn: json['fullNameBn'],
      gender: json['gender'],
      dateOfBirth: json['dateOfBirth'] != null ? DateTime.tryParse(json['dateOfBirth']) : null,
      dateOfDeath: DateTime.tryParse(json['dateOfDeath'] ?? '') ?? DateTime.now(),
      ageYears: json['ageYears'],
      placeOfDeath: json['placeOfDeath'],
      causeOfDeath: json['causeOfDeath'],
      nationalId: json['nationalId'],
      birthCertificateNo: json['birthCertificateNo'],
      fatherName: json['fatherName'],
      motherName: json['motherName'],
      spouseName: json['spouseName'],
      permanentAddress: json['permanentAddress'],
      verificationStatus: json['verificationStatus'] ?? 'PENDING',
      photoUrl: json['photoUrl'],
      graveId: json['graveId'],
      graveNumber: json['graveNumber'],
      graveyardName: json['graveyardName'],
    );
  }
}
