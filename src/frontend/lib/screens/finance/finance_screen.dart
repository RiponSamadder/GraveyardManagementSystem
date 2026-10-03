import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:intl/intl.dart';
import '../../core/theme/app_colors.dart';
import '../../providers/finance_provider.dart';

class FinanceScreen extends StatefulWidget {
  const FinanceScreen({super.key});

  @override
  State<FinanceScreen> createState() => _FinanceScreenState();
}

class _FinanceScreenState extends State<FinanceScreen> with SingleTickerProviderStateMixin {
  late TabController _tabController;

  @override
  void initState() {
    super.initState();
    _tabController = TabController(length: 2, vsync: this);
    WidgetsBinding.instance.addPostFrameCallback((_) {
      context.read<FinanceProvider>().fetchFinanceData();
    });
  }

  @override
  void dispose() {
    _tabController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    final provider = context.watch<FinanceProvider>();
    final isDark = Theme.of(context).brightness == Brightness.dark;
    final currencyFormat = NumberFormat.currency(symbol: '৳', decimalDigits: 2);

    return Scaffold(
      appBar: AppBar(
        title: const Text('Financial Management & Donations'),
        actions: [
          IconButton(
            icon: const Icon(Icons.refresh),
            onPressed: () => context.read<FinanceProvider>().fetchFinanceData(),
          ),
        ],
      ),
      body: provider.isLoading && provider.funds.isEmpty
          ? const Center(child: CircularProgressIndicator())
          : RefreshIndicator(
              onRefresh: () => context.read<FinanceProvider>().fetchFinanceData(),
              child: SingleChildScrollView(
                physics: const AlwaysScrollableScrollPhysics(),
                padding: const EdgeInsets.all(20),
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    // Funds Overview Section
                    const Text(
                      'Dedicated Fund Accounts',
                      style: TextStyle(fontSize: 16, fontWeight: FontWeight.bold),
                    ),
                    const SizedBox(height: 12),
                    if (provider.funds.isEmpty)
                      Card(
                        child: Padding(
                          padding: const EdgeInsets.all(20),
                          child: Center(
                            child: Text(
                              'No specific funds registered yet.',
                              style: TextStyle(
                                color: isDark ? AppColors.textDarkMuted : AppColors.textLightMuted,
                              ),
                            ),
                          ),
                        ),
                      )
                    else
                      LayoutBuilder(
                        builder: (context, constraints) {
                          int crossAxisCount = constraints.maxWidth > 900
                              ? 3
                              : (constraints.maxWidth > 600 ? 2 : 1);
                          return GridView.builder(
                            shrinkWrap: true,
                            physics: const NeverScrollableScrollPhysics(),
                            gridDelegate: SliverGridDelegateWithFixedCrossAxisCount(
                              crossAxisCount: crossAxisCount,
                              crossAxisSpacing: 14,
                              mainAxisSpacing: 14,
                              childAspectRatio: 2.2,
                            ),
                            itemCount: provider.funds.length,
                            itemBuilder: (context, index) {
                              final fund = provider.funds[index];
                              return Card(
                                child: Padding(
                                  padding: const EdgeInsets.all(16),
                                  child: Column(
                                    crossAxisAlignment: CrossAxisAlignment.start,
                                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                                    children: [
                                      Row(
                                        mainAxisAlignment: MainAxisAlignment.spaceBetween,
                                        children: [
                                          Expanded(
                                            child: Text(
                                              fund.fundName,
                                              style: const TextStyle(
                                                fontWeight: FontWeight.bold,
                                                fontSize: 14,
                                              ),
                                            ),
                                          ),
                                          Container(
                                            padding: const EdgeInsets.symmetric(horizontal: 6, vertical: 2),
                                            decoration: BoxDecoration(
                                              color: AppColors.primary.withAlpha(40),
                                              borderRadius: BorderRadius.circular(6),
                                            ),
                                            child: Text(
                                              fund.fundCode,
                                              style: const TextStyle(
                                                fontSize: 10,
                                                color: AppColors.primaryLight,
                                                fontWeight: FontWeight.bold,
                                              ),
                                            ),
                                          ),
                                        ],
                                      ),
                                      Text(
                                        currencyFormat.format(fund.currentBalance),
                                        style: const TextStyle(
                                          fontSize: 18,
                                          fontWeight: FontWeight.bold,
                                          color: AppColors.statusAvailable,
                                        ),
                                      ),
                                    ],
                                  ),
                                ),
                              );
                            },
                          );
                        },
                      ),
                    const SizedBox(height: 24),

                    // Tab bar for Donations vs Expenses
                    TabBar(
                      controller: _tabController,
                      labelColor: AppColors.primaryLight,
                      unselectedLabelColor: isDark ? AppColors.textDarkMuted : AppColors.textLightMuted,
                      indicatorColor: AppColors.primaryLight,
                      tabs: [
                        Tab(
                          icon: const Icon(Icons.volunteer_activism_outlined),
                          text: 'Donations (${provider.donations.length})',
                        ),
                        Tab(
                          icon: const Icon(Icons.receipt_long_outlined),
                          text: 'Expenses (${provider.expenses.length})',
                        ),
                      ],
                    ),
                    const SizedBox(height: 16),

                    // Tab contents
                    SizedBox(
                      height: 480,
                      child: TabBarView(
                        controller: _tabController,
                        children: [
                          // Donations Tab
                          provider.donations.isEmpty
                              ? Center(
                                  child: Text(
                                    'No donation records yet.',
                                    style: TextStyle(
                                      color: isDark ? AppColors.textDarkMuted : AppColors.textLightMuted,
                                    ),
                                  ),
                                )
                              : ListView.separated(
                                  itemCount: provider.donations.length,
                                  separatorBuilder: (_, __) => const SizedBox(height: 10),
                                  itemBuilder: (context, index) {
                                    final d = provider.donations[index];
                                    return Card(
                                      child: ListTile(
                                        leading: CircleAvatar(
                                          backgroundColor: AppColors.statusAvailable.withAlpha(30),
                                          child: const Icon(
                                            Icons.arrow_downward,
                                            color: AppColors.statusAvailable,
                                            size: 20,
                                          ),
                                        ),
                                        title: Text(
                                          d.donorName,
                                          style: const TextStyle(fontWeight: FontWeight.bold, fontSize: 14),
                                        ),
                                        subtitle: Text(
                                          'Receipt: ${d.receiptNumber} • ${DateFormat('MMM dd, yyyy').format(d.donationDate)} • ${d.paymentMethod}',
                                          style: TextStyle(
                                            fontSize: 12,
                                            color: isDark ? AppColors.textDarkMuted : AppColors.textLightMuted,
                                          ),
                                        ),
                                        trailing: Text(
                                          '+${currencyFormat.format(d.amount)}',
                                          style: const TextStyle(
                                            fontWeight: FontWeight.bold,
                                            fontSize: 15,
                                            color: AppColors.statusAvailable,
                                          ),
                                        ),
                                      ),
                                    );
                                  },
                                ),

                          // Expenses Tab
                          provider.expenses.isEmpty
                              ? Center(
                                  child: Text(
                                    'No expense records yet.',
                                    style: TextStyle(
                                      color: isDark ? AppColors.textDarkMuted : AppColors.textLightMuted,
                                    ),
                                  ),
                                )
                              : ListView.separated(
                                  itemCount: provider.expenses.length,
                                  separatorBuilder: (_, __) => const SizedBox(height: 10),
                                  itemBuilder: (context, index) {
                                    final e = provider.expenses[index];
                                    return Card(
                                      child: ListTile(
                                        leading: CircleAvatar(
                                          backgroundColor: AppColors.statusOccupied.withAlpha(30),
                                          child: const Icon(
                                            Icons.arrow_upward,
                                            color: AppColors.statusOccupied,
                                            size: 20,
                                          ),
                                        ),
                                        title: Text(
                                          e.expenseCategory,
                                          style: const TextStyle(fontWeight: FontWeight.bold, fontSize: 14),
                                        ),
                                        subtitle: Text(
                                          'Voucher: ${e.voucherNumber} • ${DateFormat('MMM dd, yyyy').format(e.expenseDate)} • ${e.paymentMethod}',
                                          style: TextStyle(
                                            fontSize: 12,
                                            color: isDark ? AppColors.textDarkMuted : AppColors.textLightMuted,
                                          ),
                                        ),
                                        trailing: Text(
                                          '-${currencyFormat.format(e.amount)}',
                                          style: const TextStyle(
                                            fontWeight: FontWeight.bold,
                                            fontSize: 15,
                                            color: AppColors.statusOccupied,
                                          ),
                                        ),
                                      ),
                                    );
                                  },
                                ),
                        ],
                      ),
                    ),
                  ],
                ),
              ),
            ),
    );
  }
}
