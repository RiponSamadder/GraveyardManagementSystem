import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:intl/intl.dart';
import '../../core/theme/app_colors.dart';
import '../../providers/auth_provider.dart';
import '../../providers/deceased_provider.dart';

class DeceasedScreen extends StatefulWidget {
  const DeceasedScreen({super.key});

  @override
  State<DeceasedScreen> createState() => _DeceasedScreenState();
}

class _DeceasedScreenState extends State<DeceasedScreen> {
  final _searchController = TextEditingController();

  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((_) {
      context.read<DeceasedProvider>().fetchDeceased();
    });
  }

  @override
  void dispose() {
    _searchController.dispose();
    super.dispose();
  }

  Color _getStatusColor(String status) {
    switch (status.toUpperCase()) {
      case 'APPROVED':
        return AppColors.statusAvailable;
      case 'VERIFIED':
        return AppColors.statusReserved;
      case 'PENDING':
        return AppColors.accent;
      default:
        return AppColors.statusOccupied;
    }
  }

  void _showDeceasedDetailModal(BuildContext context, int deceasedId) {
    context.read<DeceasedProvider>().fetchDeceasedDetail(deceasedId);
    final auth = context.read<AuthProvider>();

    showModalBottomSheet(
      context: context,
      isScrollControlled: true,
      backgroundColor: Colors.transparent,
      builder: (ctx) => Consumer<DeceasedProvider>(
        builder: (context, provider, _) {
          final detail = provider.selectedDeceased;
          final isDark = Theme.of(context).brightness == Brightness.dark;

          return Container(
            height: MediaQuery.of(context).size.height * 0.8,
            decoration: BoxDecoration(
              color: isDark ? AppColors.surfaceDark : AppColors.surfaceLight,
              borderRadius: const BorderRadius.vertical(top: Radius.circular(24)),
            ),
            padding: const EdgeInsets.all(24),
            child: provider.isLoading && detail == null
                ? const Center(child: CircularProgressIndicator())
                : detail == null
                    ? const Center(child: Text('Unable to load details'))
                    : SingleChildScrollView(
                        child: Column(
                          crossAxisAlignment: CrossAxisAlignment.start,
                          children: [
                            Center(
                              child: Container(
                                width: 40,
                                height: 4,
                                margin: const EdgeInsets.only(bottom: 20),
                                decoration: BoxDecoration(
                                  color: Colors.grey.withAlpha(80),
                                  borderRadius: BorderRadius.circular(2),
                                ),
                              ),
                            ),
                            Row(
                              mainAxisAlignment: MainAxisAlignment.spaceBetween,
                              children: [
                                Expanded(
                                  child: Column(
                                    crossAxisAlignment: CrossAxisAlignment.start,
                                    children: [
                                      Text(
                                        detail.fullName,
                                        style: const TextStyle(
                                          fontSize: 20,
                                          fontWeight: FontWeight.bold,
                                        ),
                                      ),
                                      if (detail.fullNameBn != null)
                                        Text(
                                          detail.fullNameBn!,
                                          style: const TextStyle(
                                            color: AppColors.textDarkMuted,
                                            fontSize: 14,
                                          ),
                                        ),
                                    ],
                                  ),
                                ),
                                Container(
                                  padding: const EdgeInsets.symmetric(horizontal: 10, vertical: 4),
                                  decoration: BoxDecoration(
                                    color: _getStatusColor(detail.verificationStatus).withAlpha(40),
                                    borderRadius: BorderRadius.circular(12),
                                  ),
                                  child: Text(
                                    detail.verificationStatus,
                                    style: TextStyle(
                                      color: _getStatusColor(detail.verificationStatus),
                                      fontWeight: FontWeight.bold,
                                      fontSize: 11,
                                    ),
                                  ),
                                ),
                              ],
                            ),
                            const SizedBox(height: 16),
                            const Divider(),
                            const SizedBox(height: 12),
                            _buildInfoRow('Deceased Code', detail.deceasedCode),
                            if (detail.gender != null) _buildInfoRow('Gender', detail.gender!),
                            if (detail.dateOfBirth != null)
                              _buildInfoRow('Date of Birth', DateFormat('MMMM dd, yyyy').format(detail.dateOfBirth!)),
                            _buildInfoRow('Date of Death', DateFormat('MMMM dd, yyyy').format(detail.dateOfDeath)),
                            if (detail.ageYears != null) _buildInfoRow('Age', '${detail.ageYears} years'),
                            if (detail.causeOfDeath != null) _buildInfoRow('Cause of Death', detail.causeOfDeath!),
                            if (detail.placeOfDeath != null) _buildInfoRow('Place of Death', detail.placeOfDeath!),
                            if (detail.nationalId != null) _buildInfoRow('National ID (NID)', detail.nationalId!),
                            if (detail.fatherName != null) _buildInfoRow('Father\'s Name', detail.fatherName!),
                            if (detail.motherName != null) _buildInfoRow('Mother\'s Name', detail.motherName!),
                            if (detail.spouseName != null) _buildInfoRow('Spouse\'s Name', detail.spouseName!),
                            if (detail.permanentAddress != null)
                              _buildInfoRow('Permanent Address', detail.permanentAddress!),
                            const SizedBox(height: 16),
                            const Divider(),
                            const SizedBox(height: 12),
                            _buildInfoRow('Grave Location',
                                'Grave #${detail.graveNumber ?? 'N/A'} (${detail.graveyardName ?? 'Main'})'),
                            const SizedBox(height: 24),

                            // Workflow verification buttons
                            if (auth.currentUser?.isAdmin ?? true) ...[
                              Row(
                                children: [
                                  if (detail.verificationStatus.toUpperCase() == 'PENDING')
                                    Expanded(
                                      child: ElevatedButton(
                                        style: ElevatedButton.styleFrom(
                                          backgroundColor: AppColors.statusReserved,
                                        ),
                                        onPressed: () async {
                                          final ok = await provider.verifyDeceased(
                                              detail.deceasedId, 'Verified by ${auth.currentUser?.fullName}');
                                          if (ok && context.mounted) {
                                            Navigator.pop(context);
                                          }
                                        },
                                        child: const Text('Verify Record'),
                                      ),
                                    ),
                                  if (detail.verificationStatus.toUpperCase() != 'APPROVED') ...[
                                    const SizedBox(width: 12),
                                    Expanded(
                                      child: ElevatedButton(
                                        style: ElevatedButton.styleFrom(
                                          backgroundColor: AppColors.statusAvailable,
                                        ),
                                        onPressed: () async {
                                          final ok = await provider.approveDeceased(
                                              detail.deceasedId, 'Approved by ${auth.currentUser?.fullName}');
                                          if (ok && context.mounted) {
                                            Navigator.pop(context);
                                          }
                                        },
                                        child: const Text('Approve Record'),
                                      ),
                                    ),
                                  ],
                                ],
                              ),
                              const SizedBox(height: 16),
                            ],
                          ],
                        ),
                      ),
          );
        },
      ),
    );
  }

  Widget _buildInfoRow(String label, String value) {
    return Padding(
      padding: const EdgeInsets.symmetric(vertical: 4),
      child: Row(
        crossAxisAlignment: CrossAxisAlignment.start,
        mainAxisAlignment: MainAxisAlignment.spaceBetween,
        children: [
          Text(label, style: const TextStyle(fontSize: 13, color: AppColors.textDarkMuted)),
          const SizedBox(width: 12),
          Flexible(
            child: Text(
              value,
              textAlign: TextAlign.end,
              style: const TextStyle(fontSize: 13, fontWeight: FontWeight.w600),
            ),
          ),
        ],
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    final provider = context.watch<DeceasedProvider>();
    final isDark = Theme.of(context).brightness == Brightness.dark;

    return Scaffold(
      appBar: AppBar(
        title: const Text('Deceased Records & Registry'),
        actions: [
          IconButton(
            icon: const Icon(Icons.refresh),
            onPressed: () => context.read<DeceasedProvider>().fetchDeceased(),
          ),
        ],
      ),
      body: Column(
        children: [
          // Search Bar
          Padding(
            padding: const EdgeInsets.all(20),
            child: TextField(
              controller: _searchController,
              decoration: InputDecoration(
                hintText: 'Search deceased by name, code, or NID...',
                prefixIcon: const Icon(Icons.search),
                suffixIcon: _searchController.text.isNotEmpty
                    ? IconButton(
                        icon: const Icon(Icons.clear),
                        onPressed: () {
                          _searchController.clear();
                          provider.fetchDeceased(query: '');
                        },
                      )
                    : null,
              ),
              onSubmitted: (query) => provider.fetchDeceased(query: query),
            ),
          ),

          // Deceased List
          Expanded(
            child: provider.isLoading && provider.deceasedList.isEmpty
                ? const Center(child: CircularProgressIndicator())
                : provider.deceasedList.isEmpty
                    ? Center(
                        child: Column(
                          mainAxisAlignment: MainAxisAlignment.center,
                          children: [
                            Icon(
                              Icons.person_search_outlined,
                              size: 48,
                              color: isDark ? AppColors.textDarkMuted : AppColors.textLightMuted,
                            ),
                            const SizedBox(height: 12),
                            Text('No records found for "${_searchController.text}"'),
                          ],
                        ),
                      )
                    : RefreshIndicator(
                        onRefresh: () => context.read<DeceasedProvider>().fetchDeceased(),
                        child: ListView.separated(
                          padding: const EdgeInsets.symmetric(horizontal: 20, vertical: 8),
                          itemCount: provider.deceasedList.length,
                          separatorBuilder: (_, __) => const SizedBox(height: 12),
                          itemBuilder: (context, index) {
                            final item = provider.deceasedList[index];
                            final statusColor = _getStatusColor(item.verificationStatus);

                            return Card(
                              child: InkWell(
                                onTap: () => _showDeceasedDetailModal(context, item.deceasedId),
                                borderRadius: BorderRadius.circular(16),
                                child: Padding(
                                  padding: const EdgeInsets.all(16),
                                  child: Row(
                                    children: [
                                      CircleAvatar(
                                        radius: 24,
                                        backgroundColor: AppColors.primary.withAlpha(40),
                                        child: Text(
                                          item.fullName.isNotEmpty ? item.fullName[0].toUpperCase() : '?',
                                          style: const TextStyle(
                                            color: AppColors.primaryLight,
                                            fontWeight: FontWeight.bold,
                                            fontSize: 18,
                                          ),
                                        ),
                                      ),
                                      const SizedBox(width: 16),
                                      Expanded(
                                        child: Column(
                                          crossAxisAlignment: CrossAxisAlignment.start,
                                          children: [
                                            Row(
                                              children: [
                                                Expanded(
                                                  child: Text(
                                                    item.fullName,
                                                    style: const TextStyle(
                                                      fontWeight: FontWeight.bold,
                                                      fontSize: 15,
                                                    ),
                                                  ),
                                                ),
                                                Container(
                                                  padding: const EdgeInsets.symmetric(
                                                    horizontal: 8,
                                                    vertical: 2,
                                                  ),
                                                  decoration: BoxDecoration(
                                                    color: statusColor.withAlpha(30),
                                                    borderRadius: BorderRadius.circular(8),
                                                  ),
                                                  child: Text(
                                                    item.verificationStatus,
                                                    style: TextStyle(
                                                      color: statusColor,
                                                      fontSize: 10,
                                                      fontWeight: FontWeight.bold,
                                                    ),
                                                  ),
                                                ),
                                              ],
                                            ),
                                            if (item.fullNameBn != null) ...[
                                              const SizedBox(height: 2),
                                              Text(
                                                item.fullNameBn!,
                                                style: TextStyle(
                                                  color: isDark
                                                      ? AppColors.textDarkMuted
                                                      : AppColors.textLightMuted,
                                                  fontSize: 12,
                                                ),
                                              ),
                                            ],
                                            const SizedBox(height: 6),
                                            Row(
                                              children: [
                                                Icon(
                                                  Icons.calendar_today_outlined,
                                                  size: 13,
                                                  color: isDark
                                                      ? AppColors.textDarkMuted
                                                      : AppColors.textLightMuted,
                                                ),
                                                const SizedBox(width: 4),
                                                Text(
                                                  'Died: ${DateFormat('MMM dd, yyyy').format(item.dateOfDeath)}',
                                                  style: TextStyle(
                                                    fontSize: 12,
                                                    color: isDark
                                                        ? AppColors.textDarkMuted
                                                        : AppColors.textLightMuted,
                                                  ),
                                                ),
                                                if (item.graveNumber != null) ...[
                                                  const SizedBox(width: 14),
                                                  const Icon(
                                                    Icons.grid_3x3,
                                                    size: 13,
                                                    color: AppColors.primaryLight,
                                                  ),
                                                  const SizedBox(width: 4),
                                                  Text(
                                                    'Grave #${item.graveNumber}',
                                                    style: const TextStyle(
                                                      fontSize: 12,
                                                      fontWeight: FontWeight.w600,
                                                    ),
                                                  ),
                                                ],
                                              ],
                                            ),
                                          ],
                                        ),
                                      ),
                                    ],
                                  ),
                                ),
                              ),
                            );
                          },
                        ),
                      ),
          ),
        ],
      ),
    );
  }
}
