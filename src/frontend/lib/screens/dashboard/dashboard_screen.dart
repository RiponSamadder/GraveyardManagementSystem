import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:intl/intl.dart';
import '../../core/theme/app_colors.dart';
import '../../providers/auth_provider.dart';
import '../../providers/dashboard_provider.dart';

class DashboardScreen extends StatefulWidget {
  final Function(int)? onNavigateTab;
  const DashboardScreen({super.key, this.onNavigateTab});

  @override
  State<DashboardScreen> createState() => _DashboardScreenState();
}

class _DashboardScreenState extends State<DashboardScreen> {
  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((_) {
      context.read<DashboardProvider>().fetchSummary();
    });
  }

  @override
  Widget build(BuildContext context) {
    final auth = context.watch<AuthProvider>();
    final dashboard = context.watch<DashboardProvider>();
    final summary = dashboard.summary;
    final isDark = Theme.of(context).brightness == Brightness.dark;
    final currencyFormat = NumberFormat.currency(symbol: '৳', decimalDigits: 0);

    return Scaffold(
      body: dashboard.isLoading && summary == null
          ? const Center(child: CircularProgressIndicator())
          : RefreshIndicator(
              onRefresh: () => context.read<DashboardProvider>().fetchSummary(),
              child: SingleChildScrollView(
                physics: const AlwaysScrollableScrollPhysics(),
                padding: const EdgeInsets.all(24),
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    // Welcome Header
                    Row(
                      mainAxisAlignment: MainAxisAlignment.spaceBetween,
                      crossAxisAlignment: CrossAxisAlignment.center,
                      children: [
                        Column(
                          crossAxisAlignment: CrossAxisAlignment.start,
                          children: [
                            Text(
                              'Welcome back, ${auth.currentUser?.fullName ?? 'Administrator'}',
                              style: Theme.of(context).textTheme.headlineSmall?.copyWith(
                                    fontWeight: FontWeight.bold,
                                  ),
                            ),
                            const SizedBox(height: 4),
                            Text(
                              'Overview and real-time operations of cemetery grounds & memorials',
                              style: TextStyle(
                                color: isDark ? AppColors.textDarkMuted : AppColors.textLightMuted,
                                fontSize: 14,
                              ),
                            ),
                          ],
                        ),
                        IconButton(
                          icon: const Icon(Icons.refresh),
                          tooltip: 'Refresh Dashboard',
                          onPressed: () => context.read<DashboardProvider>().fetchSummary(),
                        ),
                      ],
                    ),
                    const SizedBox(height: 24),

                    // Metric Cards Grid
                    LayoutBuilder(
                      builder: (context, constraints) {
                        int crossAxisCount = constraints.maxWidth > 1100
                            ? 4
                            : (constraints.maxWidth > 700 ? 2 : 1);
                        return GridView.count(
                          crossAxisCount: crossAxisCount,
                          shrinkWrap: true,
                          physics: const NeverScrollableScrollPhysics(),
                          crossAxisSpacing: 16,
                          mainAxisSpacing: 16,
                          childAspectRatio: 1.8,
                          children: [
                            _buildStatCard(
                              context,
                              title: 'Total Graves',
                              value: '${summary?.totalGraves ?? 0}',
                              subtitle: '${summary?.totalGraveyards ?? 0} Graveyards Managed',
                              icon: Icons.grid_view,
                              color: AppColors.primaryLight,
                              onTap: () => widget.onNavigateTab?.call(2),
                            ),
                            _buildStatCard(
                              context,
                              title: 'Available Graves',
                              value: '${summary?.availableGraves ?? 0}',
                              subtitle: '${summary?.occupiedGraves ?? 0} Occupied Graves',
                              icon: Icons.check_circle_outline,
                              color: AppColors.statusAvailable,
                              onTap: () => widget.onNavigateTab?.call(2),
                            ),
                            _buildStatCard(
                              context,
                              title: 'Deceased & Burials',
                              value: '${summary?.totalDeceased ?? 0}',
                              subtitle: '${summary?.totalBurials ?? 0} Recorded Burials',
                              icon: Icons.person_outline,
                              color: AppColors.statusReserved,
                              onTap: () => widget.onNavigateTab?.call(3),
                            ),
                            _buildStatCard(
                              context,
                              title: 'Fund Balance',
                              value: currencyFormat.format(summary?.currentFundBalance ?? 0),
                              subtitle: 'Total Donations: ${currencyFormat.format(summary?.totalDonations ?? 0)}',
                              icon: Icons.account_balance_wallet_outlined,
                              color: AppColors.accent,
                              onTap: () => widget.onNavigateTab?.call(5),
                            ),
                          ],
                        );
                      },
                    ),
                    const SizedBox(height: 24),

                    // Capacity & Occupancy Overview Card
                    Card(
                      child: Padding(
                        padding: const EdgeInsets.all(20),
                        child: Column(
                          crossAxisAlignment: CrossAxisAlignment.start,
                          children: [
                            Row(
                              mainAxisAlignment: MainAxisAlignment.spaceBetween,
                              children: [
                                const Text(
                                  'Grave Occupancy & Space Utilization',
                                  style: TextStyle(
                                    fontSize: 16,
                                    fontWeight: FontWeight.bold,
                                  ),
                                ),
                                Container(
                                  padding: const EdgeInsets.symmetric(horizontal: 10, vertical: 4),
                                  decoration: BoxDecoration(
                                    color: AppColors.primary.withAlpha(40),
                                    borderRadius: BorderRadius.circular(20),
                                  ),
                                  child: Text(
                                    '${(summary?.occupancyRate ?? 0).toStringAsFixed(1)}% Occupied',
                                    style: const TextStyle(
                                      color: AppColors.primaryLight,
                                      fontWeight: FontWeight.w600,
                                      fontSize: 12,
                                    ),
                                  ),
                                ),
                              ],
                            ),
                            const SizedBox(height: 16),
                            ClipRRect(
                              borderRadius: BorderRadius.circular(8),
                              child: LinearProgressIndicator(
                                value: (summary?.totalGraves ?? 0) > 0
                                    ? (summary!.occupiedGraves / summary.totalGraves)
                                    : 0.0,
                                minHeight: 12,
                                backgroundColor: isDark ? AppColors.surfaceDark : AppColors.borderLight,
                                valueColor: const AlwaysStoppedAnimation<Color>(AppColors.statusOccupied),
                              ),
                            ),
                            const SizedBox(height: 12),
                            Row(
                              mainAxisAlignment: MainAxisAlignment.spaceBetween,
                              children: [
                                _buildLegendItem(
                                  label: 'Occupied (${summary?.occupiedGraves ?? 0})',
                                  color: AppColors.statusOccupied,
                                ),
                                _buildLegendItem(
                                  label: 'Available (${summary?.availableGraves ?? 0})',
                                  color: AppColors.statusAvailable,
                                ),
                                _buildLegendItem(
                                  label: 'Total Capacity (${summary?.totalGraves ?? 0})',
                                  color: AppColors.primaryLight,
                                ),
                              ],
                            ),
                          ],
                        ),
                      ),
                    ),
                    const SizedBox(height: 24),

                    // Quick Shortcuts Row
                    const Text(
                      'Quick Operations',
                      style: TextStyle(fontSize: 16, fontWeight: FontWeight.bold),
                    ),
                    const SizedBox(height: 12),
                    Wrap(
                      spacing: 12,
                      runSpacing: 12,
                      children: [
                        _buildQuickActionButton(
                          icon: Icons.location_on_outlined,
                          label: 'Graveyard Map',
                          color: AppColors.primary,
                          onPressed: () => widget.onNavigateTab?.call(1),
                        ),
                        _buildQuickActionButton(
                          icon: Icons.grid_on_outlined,
                          label: 'Grave Locator',
                          color: AppColors.statusAvailable,
                          onPressed: () => widget.onNavigateTab?.call(2),
                        ),
                        _buildQuickActionButton(
                          icon: Icons.search,
                          label: 'Find Deceased',
                          color: AppColors.statusReserved,
                          onPressed: () => widget.onNavigateTab?.call(3),
                        ),
                        _buildQuickActionButton(
                          icon: Icons.favorite_border,
                          label: 'Memorial Profiles',
                          color: AppColors.accent,
                          onPressed: () => widget.onNavigateTab?.call(4),
                        ),
                        _buildQuickActionButton(
                          icon: Icons.volunteer_activism_outlined,
                          label: 'Donations & Finance',
                          color: AppColors.statusMaintenance,
                          onPressed: () => widget.onNavigateTab?.call(5),
                        ),
                      ],
                    ),
                    const SizedBox(height: 24),

                    // Recent Activities Section
                    Card(
                      child: Padding(
                        padding: const EdgeInsets.all(20),
                        child: Column(
                          crossAxisAlignment: CrossAxisAlignment.start,
                          children: [
                            const Text(
                              'Recent System Activity & Burials',
                              style: TextStyle(
                                fontSize: 16,
                                fontWeight: FontWeight.bold,
                              ),
                            ),
                            const SizedBox(height: 16),
                            if (summary?.recentActivities.isEmpty ?? true)
                              Padding(
                                padding: const EdgeInsets.symmetric(vertical: 24),
                                child: Center(
                                  child: Column(
                                    children: [
                                      Icon(
                                        Icons.history_outlined,
                                        size: 40,
                                        color: isDark ? AppColors.textDarkMuted : AppColors.textLightMuted,
                                      ),
                                      const SizedBox(height: 8),
                                      Text(
                                        'No recent activity recorded yet in this period.',
                                        style: TextStyle(
                                          color: isDark ? AppColors.textDarkMuted : AppColors.textLightMuted,
                                          fontSize: 13,
                                        ),
                                      ),
                                    ],
                                  ),
                                ),
                              )
                            else
                              ListView.separated(
                                shrinkWrap: true,
                                physics: const NeverScrollableScrollPhysics(),
                                itemCount: summary!.recentActivities.length,
                                separatorBuilder: (_, __) => const Divider(height: 16),
                                itemBuilder: (context, index) {
                                  final activity = summary.recentActivities[index];
                                  return ListTile(
                                    contentPadding: EdgeInsets.zero,
                                    leading: CircleAvatar(
                                      backgroundColor: AppColors.primary.withAlpha(40),
                                      child: const Icon(
                                        Icons.event_note,
                                        color: AppColors.primaryLight,
                                        size: 18,
                                      ),
                                    ),
                                    title: Text(
                                      activity.title,
                                      style: const TextStyle(fontWeight: FontWeight.w600, fontSize: 14),
                                    ),
                                    subtitle: Text(
                                      activity.description,
                                      style: TextStyle(
                                        color: isDark ? AppColors.textDarkMuted : AppColors.textLightMuted,
                                        fontSize: 12,
                                      ),
                                    ),
                                    trailing: Text(
                                      DateFormat('MMM d, h:mm a').format(activity.timestamp),
                                      style: TextStyle(
                                        color: isDark ? AppColors.textDarkMuted : AppColors.textLightMuted,
                                        fontSize: 11,
                                      ),
                                    ),
                                  );
                                },
                              ),
                          ],
                        ),
                      ),
                    ),
                  ],
                ),
              ),
            ),
    );
  }

  Widget _buildStatCard(
    BuildContext context, {
    required String title,
    required String value,
    required String subtitle,
    required IconData icon,
    required Color color,
    VoidCallback? onTap,
  }) {
    final isDark = Theme.of(context).brightness == Brightness.dark;
    return Card(
      child: InkWell(
        onTap: onTap,
        borderRadius: BorderRadius.circular(16),
        child: Padding(
          padding: const EdgeInsets.all(16),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            children: [
              Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: [
                  Text(
                    title,
                    style: TextStyle(
                      color: isDark ? AppColors.textDarkSecondary : AppColors.textLightSecondary,
                      fontSize: 13,
                      fontWeight: FontWeight.w500,
                    ),
                  ),
                  Container(
                    padding: const EdgeInsets.all(8),
                    decoration: BoxDecoration(
                      color: color.withAlpha(35),
                      borderRadius: BorderRadius.circular(10),
                    ),
                    child: Icon(icon, color: color, size: 20),
                  ),
                ],
              ),
              Text(
                value,
                style: const TextStyle(
                  fontSize: 22,
                  fontWeight: FontWeight.bold,
                  letterSpacing: -0.5,
                ),
              ),
              Text(
                subtitle,
                style: TextStyle(
                  color: isDark ? AppColors.textDarkMuted : AppColors.textLightMuted,
                  fontSize: 11,
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }

  Widget _buildLegendItem({required String label, required Color color}) {
    return Row(
      children: [
        Container(
          width: 10,
          height: 10,
          decoration: BoxDecoration(
            color: color,
            shape: BoxShape.circle,
          ),
        ),
        const SizedBox(width: 6),
        Text(label, style: const TextStyle(fontSize: 12)),
      ],
    );
  }

  Widget _buildQuickActionButton({
    required IconData icon,
    required String label,
    required Color color,
    required VoidCallback onPressed,
  }) {
    return ElevatedButton.icon(
      onPressed: onPressed,
      icon: Icon(icon, size: 18),
      label: Text(label),
      style: ElevatedButton.styleFrom(
        backgroundColor: color,
        padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 12),
      ),
    );
  }
}
