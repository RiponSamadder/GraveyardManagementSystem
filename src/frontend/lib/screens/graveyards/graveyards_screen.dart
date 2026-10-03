import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import '../../core/theme/app_colors.dart';
import '../../providers/graveyard_provider.dart';

class GraveyardsScreen extends StatefulWidget {
  const GraveyardsScreen({super.key});

  @override
  State<GraveyardsScreen> createState() => _GraveyardsScreenState();
}

class _GraveyardsScreenState extends State<GraveyardsScreen> {
  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((_) {
      context.read<GraveyardProvider>().fetchGraveyards();
    });
  }

  void _showGraveyardDetailModal(BuildContext context, int graveyardId) {
    context.read<GraveyardProvider>().fetchGraveyardDetail(graveyardId);
    showModalBottomSheet(
      context: context,
      isScrollControlled: true,
      backgroundColor: Colors.transparent,
      builder: (ctx) => Consumer<GraveyardProvider>(
        builder: (context, provider, _) {
          final detail = provider.selectedGraveyard;
          final isDark = Theme.of(context).brightness == Brightness.dark;

          return Container(
            height: MediaQuery.of(context).size.height * 0.75,
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
                                        detail.graveyardName,
                                        style: const TextStyle(
                                          fontSize: 20,
                                          fontWeight: FontWeight.bold,
                                        ),
                                      ),
                                      if (detail.graveyardNameBn != null)
                                        Text(
                                          detail.graveyardNameBn!,
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
                                    color: detail.isActive
                                        ? AppColors.statusAvailable.withAlpha(35)
                                        : AppColors.statusOccupied.withAlpha(35),
                                    borderRadius: BorderRadius.circular(12),
                                  ),
                                  child: Text(
                                    detail.isActive ? 'ACTIVE' : 'INACTIVE',
                                    style: TextStyle(
                                      color: detail.isActive
                                          ? AppColors.statusAvailable
                                          : AppColors.statusOccupied,
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
                            _buildDetailRow(Icons.location_on_outlined, 'Location',
                                '${detail.address ?? ''}, ${detail.city ?? ''}, ${detail.district ?? ''}'),
                            if (detail.totalAreaAcres != null)
                              _buildDetailRow(Icons.landscape_outlined, 'Total Area',
                                  '${detail.totalAreaAcres} Acres'),
                            if (detail.totalCapacity != null)
                              _buildDetailRow(Icons.groups_outlined, 'Total Capacity',
                                  '${detail.totalCapacity} graves'),
                            const SizedBox(height: 20),
                            const Text(
                              'Sections & Blocks',
                              style: TextStyle(
                                fontSize: 16,
                                fontWeight: FontWeight.bold,
                              ),
                            ),
                            const SizedBox(height: 12),
                            if (detail.sections.isEmpty)
                              const Padding(
                                padding: EdgeInsets.all(12),
                                child: Text('No sections configured yet.'),
                              )
                            else
                              ListView.separated(
                                shrinkWrap: true,
                                physics: const NeverScrollableScrollPhysics(),
                                itemCount: detail.sections.length,
                                separatorBuilder: (_, __) => const SizedBox(height: 12),
                                itemBuilder: (context, sIdx) {
                                  final section = detail.sections[sIdx];
                                  return Card(
                                    color: isDark ? AppColors.cardDark : AppColors.cardLight,
                                    child: Padding(
                                      padding: const EdgeInsets.all(16),
                                      child: Column(
                                        crossAxisAlignment: CrossAxisAlignment.start,
                                        children: [
                                          Row(
                                            mainAxisAlignment: MainAxisAlignment.spaceBetween,
                                            children: [
                                              Text(
                                                'Section ${section.sectionCode}: ${section.sectionName}',
                                                style: const TextStyle(
                                                  fontWeight: FontWeight.w600,
                                                  fontSize: 14,
                                                ),
                                              ),
                                              if (section.capacity != null)
                                                Text(
                                                  'Cap: ${section.capacity}',
                                                  style: const TextStyle(
                                                    fontSize: 12,
                                                    color: AppColors.primaryLight,
                                                  ),
                                                ),
                                            ],
                                          ),
                                          if (section.blocks.isNotEmpty) ...[
                                            const SizedBox(height: 10),
                                            Wrap(
                                              spacing: 8,
                                              runSpacing: 8,
                                              children: section.blocks.map((block) {
                                                return Chip(
                                                  label: Text(
                                                    'Block ${block.blockCode} (${block.blockName})',
                                                    style: const TextStyle(fontSize: 11),
                                                  ),
                                                  padding: EdgeInsets.zero,
                                                  visualDensity: VisualDensity.compact,
                                                );
                                              }).toList(),
                                            ),
                                          ],
                                        ],
                                      ),
                                    ),
                                  );
                                },
                              ),
                          ],
                        ),
                      ),
          );
        },
      ),
    );
  }

  Widget _buildDetailRow(IconData icon, String title, String value) {
    return Padding(
      padding: const EdgeInsets.only(bottom: 10),
      child: Row(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Icon(icon, size: 18, color: AppColors.primaryLight),
          const SizedBox(width: 10),
          Text('$title: ', style: const TextStyle(fontWeight: FontWeight.w500, fontSize: 13)),
          Expanded(child: Text(value, style: const TextStyle(fontSize: 13))),
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
        title: const Text('Graveyards & Grounds Management'),
        actions: [
          IconButton(
            icon: const Icon(Icons.refresh),
            onPressed: () => context.read<GraveyardProvider>().fetchGraveyards(),
          ),
        ],
      ),
      body: provider.isLoading && provider.graveyards.isEmpty
          ? const Center(child: CircularProgressIndicator())
          : provider.graveyards.isEmpty
              ? Center(
                  child: Column(
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: [
                      Icon(
                        Icons.account_balance,
                        size: 48,
                        color: isDark ? AppColors.textDarkMuted : AppColors.textLightMuted,
                      ),
                      const SizedBox(height: 12),
                      const Text('No graveyards found'),
                    ],
                  ),
                )
              : RefreshIndicator(
                  onRefresh: () => context.read<GraveyardProvider>().fetchGraveyards(),
                  child: ListView.separated(
                    padding: const EdgeInsets.all(20),
                    itemCount: provider.graveyards.length,
                    separatorBuilder: (_, __) => const SizedBox(height: 16),
                    itemBuilder: (context, index) {
                      final item = provider.graveyards[index];
                      return Card(
                        child: InkWell(
                          onTap: () => _showGraveyardDetailModal(context, item.graveyardId),
                          borderRadius: BorderRadius.circular(16),
                          child: Padding(
                            padding: const EdgeInsets.all(20),
                            child: Row(
                              children: [
                                Container(
                                  width: 52,
                                  height: 52,
                                  decoration: BoxDecoration(
                                    color: AppColors.primary.withAlpha(40),
                                    borderRadius: BorderRadius.circular(12),
                                  ),
                                  child: const Icon(
                                    Icons.account_balance,
                                    color: AppColors.primaryLight,
                                  ),
                                ),
                                const SizedBox(width: 16),
                                Expanded(
                                  child: Column(
                                    crossAxisAlignment: CrossAxisAlignment.start,
                                    children: [
                                      Row(
                                        children: [
                                          Text(
                                            item.graveyardName,
                                            style: const TextStyle(
                                              fontWeight: FontWeight.bold,
                                              fontSize: 16,
                                            ),
                                          ),
                                          const SizedBox(width: 8),
                                          Container(
                                            padding: const EdgeInsets.symmetric(
                                              horizontal: 8,
                                              vertical: 2,
                                            ),
                                            decoration: BoxDecoration(
                                              color: item.isActive
                                                  ? AppColors.statusAvailable.withAlpha(35)
                                                  : AppColors.statusOccupied.withAlpha(35),
                                              borderRadius: BorderRadius.circular(8),
                                            ),
                                            child: Text(
                                              item.isActive ? 'Active' : 'Inactive',
                                              style: TextStyle(
                                                color: item.isActive
                                                    ? AppColors.statusAvailable
                                                    : AppColors.statusOccupied,
                                                fontSize: 11,
                                                fontWeight: FontWeight.w600,
                                              ),
                                            ),
                                          ),
                                        ],
                                      ),
                                      if (item.graveyardNameBn != null) ...[
                                        const SizedBox(height: 2),
                                        Text(
                                          item.graveyardNameBn!,
                                          style: TextStyle(
                                            color: isDark
                                                ? AppColors.textDarkMuted
                                                : AppColors.textLightMuted,
                                            fontSize: 13,
                                          ),
                                        ),
                                      ],
                                      const SizedBox(height: 6),
                                      Row(
                                        children: [
                                          Icon(
                                            Icons.location_on_outlined,
                                            size: 14,
                                            color: isDark
                                                ? AppColors.textDarkMuted
                                                : AppColors.textLightMuted,
                                          ),
                                          const SizedBox(width: 4),
                                          Text(
                                            '${item.city ?? ''}, ${item.district ?? ''}',
                                            style: TextStyle(
                                              fontSize: 12,
                                              color: isDark
                                                  ? AppColors.textDarkMuted
                                                  : AppColors.textLightMuted,
                                            ),
                                          ),
                                          const SizedBox(width: 16),
                                          const Icon(
                                            Icons.grid_3x3,
                                            size: 14,
                                            color: AppColors.primaryLight,
                                          ),
                                          const SizedBox(width: 4),
                                          Text(
                                            '${item.totalGraves} Graves',
                                            style: const TextStyle(
                                              fontSize: 12,
                                              fontWeight: FontWeight.w600,
                                            ),
                                          ),
                                        ],
                                      ),
                                    ],
                                  ),
                                ),
                                const Icon(Icons.arrow_forward_ios, size: 16),
                              ],
                            ),
                          ),
                        ),
                      );
                    },
                  ),
                ),
    );
  }
}
