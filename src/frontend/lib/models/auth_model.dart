class LoginResponse {
  final int userId;
  final String userCode;
  final String fullName;
  final String? email;
  final String? mobileNo;
  final String token;
  final DateTime expiresAt;
  final List<String> roles;

  LoginResponse({
    required this.userId,
    required this.userCode,
    required this.fullName,
    this.email,
    this.mobileNo,
    required this.token,
    required this.expiresAt,
    required this.roles,
  });

  factory LoginResponse.fromJson(Map<String, dynamic> json) {
    return LoginResponse(
      userId: json['userId'] ?? 0,
      userCode: json['userCode'] ?? '',
      fullName: json['fullName'] ?? '',
      email: json['email'],
      mobileNo: json['mobileNo'],
      token: json['token'] ?? '',
      expiresAt: DateTime.tryParse(json['expiresAt'] ?? '') ?? DateTime.now().add(const Duration(days: 1)),
      roles: (json['roles'] as List<dynamic>?)?.map((e) => e.toString()).toList() ?? [],
    );
  }

  bool get isAdmin => roles.any((r) => r.toLowerCase().contains('admin'));
}

class UserProfile {
  final int userId;
  final String userCode;
  final String fullName;
  final String? fullNameBn;
  final String? email;
  final String? mobileNo;
  final String? address;
  final bool isActive;
  final DateTime createdAt;
  final List<String> roles;

  UserProfile({
    required this.userId,
    required this.userCode,
    required this.fullName,
    this.fullNameBn,
    this.email,
    this.mobileNo,
    this.address,
    required this.isActive,
    required this.createdAt,
    required this.roles,
  });

  factory UserProfile.fromJson(Map<String, dynamic> json) {
    return UserProfile(
      userId: json['userId'] ?? 0,
      userCode: json['userCode'] ?? '',
      fullName: json['fullName'] ?? '',
      fullNameBn: json['fullNameBn'],
      email: json['email'],
      mobileNo: json['mobileNo'],
      address: json['address'],
      isActive: json['isActive'] ?? true,
      createdAt: DateTime.tryParse(json['createdAt'] ?? '') ?? DateTime.now(),
      roles: (json['roles'] as List<dynamic>?)?.map((e) => e.toString()).toList() ?? [],
    );
  }
}
