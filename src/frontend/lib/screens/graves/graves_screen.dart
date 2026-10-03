import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:intl/intl.dart';
import '../../core/theme/app_colors.dart';
import '../../models/grave_model.dart';
import '../../providers/graveyard_provider.dart';

class GravesScreen extends StatefulWidget {
  const GravesScreen({super.key});

  @override
  State<GravesScreen> createState() => _GravesScreenState();
}

class _GravesScreenState extends State<GravesScreen> {
  final List<String> _filters = ['ALL', 'AVAILABLE', 'OCCUPIED', 'RESERVED'];

  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((_) {
      context.read<GraveyardProvider>().fetchGraves();
    });
  }

  Color _getStatusColor(String status) {
    switch (status.toUpperCase()) {
      case 'AVAILABLE':
        return AppColors.statusAvailable;
      case 'OCCUPIED':
        return AppColors.statusOccupied;
      case 'RESERVED':
        return AppColors.statusReserved;
      default:
        return AppColors.statusMaintenance;
    }
  }

  void _showGraveDetailModal(BuildContext context, GraveItem grave) {
    final isDark = Theme.of(context).brightness == Brightness.dark;
    final color = _getStatusColor(grave.graveStatus);

    showModalBottomSheet(
      context: context,
      backgroundColor: Colors.transparent,
      builder: (ctx) => Container(
        decoration: BoxDecoration(
          color: isDark ? AppColors.surfaceDark : AppColors.surfaceLight,
          borderRadius: const BorderRadius.vertical(top: Radius.circular(24)),
        ),
        padding: const EdgeInsets.all(24),
        child: Column(
          mainAxisSize: MainAxisSize.min,
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
                Row(
                  children: [
                    Container(
                      padding: const EdgeInsets.all(10),
                      decoration: BoxDecoration(
                        color: color.withAlpha(40),
                        borderRadius: BorderRadius.circular(12),
                      ),
                      child: Icon(Icons.grid_3x3, color: color, size: 24),
                    ),
                    const SizedBox(width: 14),
                    Column(
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        Text(
                          'Grave #${grave.graveNumber}',
                          style: const TextStyle(
                            fontSize: 20,
                            fontWeight: FontWeight.bold,
                          ),
                        ),
                        Text(
                          '${grave.graveyardName ?? 'Main'} • ${grave.sectionName ?? 'Sec'} • ${grave.blockName ?? 'Blk'}',
                          style: TextStyle(
                            color: isDark ? AppColors.textDarkMuted : AppColors.textLightMuted,
                            fontSize: 13,
                          ),
                        ),
                      ],
                    ),
                  ],
                ),
                Container(
                  padding: const EdgeInsets.symmetric(horizontal: 10, vertical: 4),
                  decoration: BoxDecoration(
                    color: color.withAlpha(40),
                    borderRadius: BorderRadius.circular(12),
                  ),
                  child: Text(
                    grave.graveStatus,
                    style: TextStyle(
                      color: color,
                      fontWeight: FontWeight.bold,
                      fontSize: 11,
                    ),
                  ),
                ),
              ],
            ),
            const SizedBox(height: 20),
            const Divider(),
            const SizedBox(height: 12),
            _buildInfoRow('Grave Type', grave.graveType),
            _buildInfoRow('Tenure', grave.isPerpetual ? 'Perpetual (Permanent)' : 'Standard Term'),
            if (grave.isOccupied) ...[
              const SizedBox(height: 8),
              _buildInfoRow('Buried Person', grave.deceasedName ?? 'Recorded Deceased'),
              if (grave.burialDate != null)
                _buildInfoRow(
                  'Burial Date',
                  DateFormat('MMMM dd, yyyy').format(grave.burialDate!),
                ),
            ],
            const SizedBox(height: 24),
          ],
        ),
      ),
    );
  }

  Widget _buildInfoRow(String label, String value) {
    return Padding(
      padding: const EdgeInsets.symmetric(vertical: 4),
      child: Row(
        mainAxisAlignment: MainAxisAlignment.spaceBetween,
        children: [
          Text(label, style: const TextStyle(fontSize: 13, color: AppColors.textDarkMuted)),
          Text(value, style: const TextStyle(fontSize: 13, fontWeight: FontWeight.w600)),
        ],
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    final provider = context.watch<GraveyardProvider>();
    final isDark = Theme.of(context).brightness == Brightness.dark;

    return Scaffold(
      appBar: AppBar(
        title: const Text('Grave Locator & Space Map'),
        actions: [
          IconButton(
            icon: const Icon(Icons.refresh),
            onPressed: () => context.read<GraveyardProvider>().fetchGraves(),
          ),
        ],
      ),
      body: Column(
        children: [
          // Filter Chips Row
          Container(
            padding: const EdgeInsets.symmetric(horizontal: 20, vertical: 12),
            color: isDark ? AppColors.surfaceDark.withAlpha(120) : Colors.white,
            child: SingleChildScrollView(
              scrollDirection: Axis.horizontal,
              child: Row(
                children: _filters.map((filter) {
                  final isSelected = provider.selectedGraveStatusFilter == filter;
                  return Padding(
                    padding: const EdgeInsets.only(right: 8),
                    child: ChoiceChip(
                      label: Text(filter),
                      selected: isSelected,
                      onSelected: (selected) {
                        if (selected) {
                          provider.filterGravesByStatus(filter);
                        }
                      },
                      selectedColor: AppColors.primary,
                      labelStyle: TextStyle(
                        color: isSelected ? Colors.white : (isDark ? AppColors.textDarkPrimary : AppColors.textLightPrimary),
                        fontWeight: isSelected ? FontWeight.w600 : FontWeight.normal,
                        fontSize: 12,
                      ),
                    ),
                  );
                }).toList(),
              ),
            ),
          ),

          // Graves Grid
          Expanded(
            child: provider.isLoading && provider.graves.isEmpty
                ? const Center(child: CircularProgressIndicator())
                : provider.graves.isEmpty
                    ? Center(
                        child: Column(
                          mainAxisAlignment: MainAxisAlignment.center,
                          children: [
                            Icon(
                              Icons.grid_off_outlined,
                              size: 48,
                              color: isDark ? AppColors.textDarkMuted : AppColors.textLightMuted,
                            ),
                            const SizedBox(height: 12),
                            Text('No graves found for filter "${provider.selectedGraveStatusFilter}"'),
                          ],
                        ),
                      )
                    : RefreshIndicator(
                        onRefresh: () => context.read<GraveyardProvider>().fetchGraves(),
                        child: LayoutBuilder(
                          builder: (context, constraints) {
                            int crossAxisCount = constraints.maxWidth > 1200
                                ? 6
                                : (constraints.maxWidth > 900
                                    ? 4
                                    : (constraints.maxWidth > 600 ? 3 : 2));
                            return GridView.builder(
                              padding: const EdgeInsets.all(20),
                              gridDelegate: SliverGridDelegateWithFixedCrossAxisCount(
                                crossAxisCount: crossAxisCount,
                                crossAxisSpacing: 14,
                                mainAxisSpacing: 14,
                                childAspectRatio: 1.25,
                              ),
                              itemCount: provider.graves.length,
                              itemBuilder: (context, index) {
                                final grave = provider.graves[index];
                                final statusColor = _getStatusColor(grave.graveStatus);

                                return Card(
                                  child: InkWell(
                                    onTap: () => _showGraveDetailModal(context, grave),
                                    borderRadius: BorderRadius.circular(16),
                                    child: Padding(
                                      padding: const EdgeInsets.all(12),
                                      child: Column(
                                        crossAxisAlignment: CrossAxisAlignment.start,
                                        mainAxisAlignment: MainAxisAlignment.spaceBetween,
                                        children: [
                                          Row(
                                            mainAxisAlignment: MainAxisAlignment.spaceBetween,
                                            children: [
                                              Text(
                                                '#${grave.graveNumber}',
                                                style: const TextStyle(
                                                  fontWeight: FontWeight.bold,
                                                  fontSize: 15,
                                                ),
                                              ),
                                              Container(
                                                width: 10,
                                                height: 10,
                                                decoration: BoxDecoration(
                                                  color: statusColor,
                                                  shape: BoxShape.circle,
                                                ),
                                              ),
                                            ],
                                          ),
                                          Column(
                                            crossAxisAlignment: CrossAxisAlignment.start,
                                            children: [
                                              if (grave.isOccupied && grave.deceasedName != null)
                                                Text(
                                                  grave.deceasedName!,
                                                  maxLines: 1,
                                                  overflow: TextOverflow.ellipsis,
                                                  style: const TextStyle(
                                                    fontSize: 12,
                                                    fontWeight: FontWeight.w600,
                                                  ),
                                                ),
                                              Text(
                                                '${grave.sectionName ?? ''} • ${grave.blockName ?? ''}',
                                                style: TextStyle(
                                                  fontSize: 11,
                                                  color: isDark ? AppColors.textDarkMuted : AppColors.textLightMuted,
                                                ),
                                              ),
                                            ],
                                          ),
                                          Container(
                                            padding: const EdgeInsets.symmetric(horizontal: 6, vertical: 2),
                                            decoration: BoxDecoration(
                                              color: statusColor.withAlpha(30),
                                              borderRadius: BorderRadius.circular(6),
                                            ),
                                            child: Text(
                                              grave.graveStatus,
                                              style: TextStyle(
                                                color: statusColor,
                                                fontSize: 10,
                                                fontWeight: FontWeight.bold,
                                              ),
                                            ),
                                          ),
                                        ],
                                      ),
                                    ),
                                  ),
                                );
                              },
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
