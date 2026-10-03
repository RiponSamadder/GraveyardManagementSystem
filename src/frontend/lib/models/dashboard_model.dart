class DashboardSummary {
  final int totalGraveyards;
  final int totalGraves;
  final int occupiedGraves;
  final int availableGraves;
  final int totalDeceased;
  final int totalBurials;
  final double totalDonations;
  final double totalExpenses;
  final double currentFundBalance;
  final int recentBurials;
  final List<RecentActivity> recentActivities;

  DashboardSummary({
    required this.totalGraveyards,
    required this.totalGraves,
    required this.occupiedGraves,
    required this.availableGraves,
    required this.totalDeceased,
    required this.totalBurials,
    required this.totalDonations,
    required this.totalExpenses,
    required this.currentFundBalance,
    required this.recentBurials,
    required this.recentActivities,
  });

  factory DashboardSummary.fromJson(Map<String, dynamic> json) {
    return DashboardSummary(
      totalGraveyards: json['totalGraveyards'] ?? 0,
      totalGraves: json['totalGraves'] ?? 0,
      occupiedGraves: json['occupiedGraves'] ?? 0,
      availableGraves: json['availableGraves'] ?? 0,
      totalDeceased: json['totalDeceased'] ?? 0,
      totalBurials: json['totalBurials'] ?? 0,
      totalDonations: (json['totalDonations'] as num?)?.toDouble() ?? 0.0,
      totalExpenses: (json['totalExpenses'] as num?)?.toDouble() ?? 0.0,
      currentFundBalance: (json['currentFundBalance'] as num?)?.toDouble() ?? 0.0,
      recentBurials: json['recentBurials'] ?? 0,
      recentActivities: (json['recentActivities'] as List<dynamic>?)
              ?.map((e) => RecentActivity.fromJson(e as Map<String, dynamic>))
              .toList() ??
          [],
    );
  }

  double get occupancyRate =>
      totalGraves > 0 ? (occupiedGraves / totalGraves) * 100 : 0.0;
}

class RecentActivity {
  final String title;
  final String description;
  final DateTime timestamp;
  final String category;

  RecentActivity({
    required this.title,
    required this.description,
    required this.timestamp,
    required this.category,
  });

  factory RecentActivity.fromJson(Map<String, dynamic> json) {
    return RecentActivity(
      title: json['title'] ?? '',
      description: json['description'] ?? '',
      timestamp: DateTime.tryParse(json['timestamp'] ?? '') ?? DateTime.now(),
      category: json['category'] ?? 'General',
    );
  }
}
