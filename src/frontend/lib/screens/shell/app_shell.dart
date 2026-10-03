import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import '../../core/theme/app_colors.dart';
import '../../providers/auth_provider.dart';
import '../auth/login_screen.dart';
import '../dashboard/dashboard_screen.dart';
import '../graveyards/graveyards_screen.dart';
import '../graves/graves_screen.dart';
import '../deceased/deceased_screen.dart';
import '../memorials/memorials_screen.dart';
import '../finance/finance_screen.dart';

class AppShell extends StatefulWidget {
  const AppShell({super.key});

  @override
  State<AppShell> createState() => _AppShellState();
}

class _AppShellState extends State<AppShell> {
  int _currentIndex = 0;

  void _onSelectTab(int index) {
    setState(() {
      _currentIndex = index;
    });
  }

  @override
  Widget build(BuildContext context) {
    final auth = context.watch<AuthProvider>();
    final isDark = Theme.of(context).brightness == Brightness.dark;
    final isDesktop = MediaQuery.of(context).size.width >= 800;

    final screens = [
      DashboardScreen(onNavigateTab: _onSelectTab),
      const GraveyardsScreen(),
      const GravesScreen(),
      const DeceasedScreen(),
      const MemorialsScreen(),
      const FinanceScreen(),
    ];

    final navItems = [
      _NavItem(icon: Icons.dashboard_outlined, activeIcon: Icons.dashboard, label: 'Overview'),
      _NavItem(icon: Icons.account_balance_outlined, activeIcon: Icons.account_balance, label: 'Graveyards'),
      _NavItem(icon: Icons.grid_view_outlined, activeIcon: Icons.grid_view, label: 'Graves & Locator'),
      _NavItem(icon: Icons.people_outline, activeIcon: Icons.people, label: 'Deceased Registry'),
      _NavItem(icon: Icons.auto_stories_outlined, activeIcon: Icons.auto_stories, label: 'Memorials'),
      _NavItem(icon: Icons.account_balance_wallet_outlined, activeIcon: Icons.account_balance_wallet, label: 'Finance'),
    ];

    if (!isDesktop) {
      // Mobile / Tablet layout with BottomNavigationBar
      return Scaffold(
        body: IndexedStack(
          index: _currentIndex,
          children: screens,
        ),
        bottomNavigationBar: NavigationBar(
          selectedIndex: _currentIndex,
          onDestinationSelected: _onSelectTab,
          destinations: navItems
              .map((item) => NavigationDestination(
                    icon: Icon(item.icon),
                    selectedIcon: Icon(item.activeIcon, color: AppColors.primaryLight),
                    label: item.label,
                  ))
              .toList(),
        ),
      );
    }

    // Desktop / Web layout with Sidebar
    return Scaffold(
      body: Row(
        children: [
          // Sidebar
          Container(
            width: 250,
            decoration: BoxDecoration(
              color: isDark ? AppColors.surfaceDark : Colors.white,
              border: Border(
                right: BorderSide(
                  color: isDark ? AppColors.borderDark : AppColors.borderLight,
                ),
              ),
            ),
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                // Brand Header
                Padding(
                  padding: const EdgeInsets.all(24),
                  child: Row(
                    children: [
                      Container(
                        width: 40,
                        height: 40,
                        decoration: BoxDecoration(
                          color: AppColors.primary.withAlpha(40),
                          borderRadius: BorderRadius.circular(10),
                          border: Border.all(color: AppColors.primaryLight, width: 1.5),
                        ),
                        child: const Icon(
                          Icons.account_balance,
                          color: AppColors.primaryLight,
                          size: 22,
                        ),
                      ),
                      const SizedBox(width: 12),
                      Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          const Text(
                            'DGMMS',
                            style: TextStyle(
                              fontWeight: FontWeight.bold,
                              fontSize: 16,
                              letterSpacing: 0.5,
                            ),
                          ),
                          Text(
                            'Memorial Portal',
                            style: TextStyle(
                              color: isDark ? AppColors.textDarkMuted : AppColors.textLightMuted,
                              fontSize: 11,
                            ),
                          ),
                        ],
                      ),
                    ],
                  ),
                ),
                const Divider(height: 1),
                const SizedBox(height: 12),

                // Navigation list
                Expanded(
                  child: ListView.builder(
                    padding: const EdgeInsets.symmetric(horizontal: 12),
                    itemCount: navItems.length,
                    itemBuilder: (context, index) {
                      final item = navItems[index];
                      final isSelected = _currentIndex == index;
                      return Padding(
                        padding: const EdgeInsets.only(bottom: 4),
                        child: ListTile(
                          dense: true,
                          shape: RoundedRectangleBorder(
                            borderRadius: BorderRadius.circular(10),
                          ),
                          tileColor: isSelected
                              ? AppColors.primary.withAlpha(isDark ? 50 : 30)
                              : Colors.transparent,
                          leading: Icon(
                            isSelected ? item.activeIcon : item.icon,
                            color: isSelected
                                ? AppColors.primaryLight
                                : (isDark ? AppColors.textDarkMuted : AppColors.textLightMuted),
                            size: 20,
                          ),
                          title: Text(
                            item.label,
                            style: TextStyle(
                              fontSize: 13,
                              fontWeight: isSelected ? FontWeight.w600 : FontWeight.normal,
                              color: isSelected
                                  ? (isDark ? Colors.white : AppColors.primaryDark)
                                  : (isDark ? AppColors.textDarkSecondary : AppColors.textLightSecondary),
                            ),
                          ),
                          onTap: () => _onSelectTab(index),
                        ),
                      );
                    },
                  ),
                ),

                const Divider(height: 1),

                // User Profile & Logout at bottom
                Padding(
                  padding: const EdgeInsets.all(16),
                  child: Container(
                    padding: const EdgeInsets.all(12),
                    decoration: BoxDecoration(
                      color: isDark ? AppColors.cardDark : AppColors.bgLight,
                      borderRadius: BorderRadius.circular(12),
                      border: Border.all(
                        color: isDark ? AppColors.borderDark : AppColors.borderLight,
                      ),
                    ),
                    child: Row(
                      children: [
                        CircleAvatar(
                          radius: 18,
                          backgroundColor: AppColors.primary.withAlpha(40),
                          child: Text(
                            (auth.currentUser?.fullName.isNotEmpty ?? false)
                                ? auth.currentUser!.fullName[0].toUpperCase()
                                : 'U',
                            style: const TextStyle(
                              color: AppColors.primaryLight,
                              fontWeight: FontWeight.bold,
                              fontSize: 14,
                            ),
                          ),
                        ),
                        const SizedBox(width: 10),
                        Expanded(
                          child: Column(
                            crossAxisAlignment: CrossAxisAlignment.start,
                            mainAxisSize: MainAxisSize.min,
                            children: [
                              Text(
                                auth.currentUser?.fullName ?? 'User',
                                maxLines: 1,
                                overflow: TextOverflow.ellipsis,
                                style: const TextStyle(fontWeight: FontWeight.bold, fontSize: 12),
                              ),
                              Text(
                                auth.currentUser?.userCode ?? 'DGMMS',
                                style: TextStyle(
                                  color: isDark ? AppColors.textDarkMuted : AppColors.textLightMuted,
                                  fontSize: 10,
                                ),
                              ),
                            ],
                          ),
                        ),
                        IconButton(
                          icon: const Icon(Icons.logout, size: 18),
                          tooltip: 'Sign Out',
                          onPressed: () async {
                            await auth.logout();
                            if (context.mounted) {
                              Navigator.of(context).pushReplacement(
                                MaterialPageRoute(builder: (_) => const LoginScreen()),
                              );
                            }
                          },
                        ),
                      ],
                    ),
                  ),
                ),
              ],
            ),
          ),

          // Main Screen Area
          Expanded(
            child: IndexedStack(
              index: _currentIndex,
              children: screens,
            ),
          ),
        ],
      ),
    );
  }
}

class _NavItem {
  final IconData icon;
  final IconData activeIcon;
  final String label;

  _NavItem({
    required this.icon,
    required this.activeIcon,
    required this.label,
  });
}
