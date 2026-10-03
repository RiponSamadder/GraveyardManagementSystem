import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:intl/intl.dart';
import '../../core/theme/app_colors.dart';
import '../../providers/memorial_provider.dart';

class MemorialsScreen extends StatefulWidget {
  const MemorialsScreen({super.key});

  @override
  State<MemorialsScreen> createState() => _MemorialsScreenState();
}

class _MemorialsScreenState extends State<MemorialsScreen> {
  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((_) {
      context.read<MemorialProvider>().fetchMemorials();
    });
  }

  void _showMemorialDetailModal(BuildContext context, int memorialId) {
    context.read<MemorialProvider>().fetchMemorialDetail(memorialId);
    showModalBottomSheet(
      context: context,
      isScrollControlled: true,
      backgroundColor: Colors.transparent,
      builder: (ctx) => Consumer<MemorialProvider>(
        builder: (context, provider, _) {
          final detail = provider.selectedMemorial;
          final isDark = Theme.of(context).brightness == Brightness.dark;

          return Container(
            height: MediaQuery.of(context).size.height * 0.85,
            decoration: BoxDecoration(
              color: isDark ? AppColors.surfaceDark : AppColors.surfaceLight,
              borderRadius: const BorderRadius.vertical(top: Radius.circular(24)),
            ),
            padding: const EdgeInsets.all(24),
            child: provider.isLoading && detail == null
                ? const Center(child: CircularProgressIndicator())
                : detail == null
                    ? const Center(child: Text('Unable to load memorial profile'))
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

                            // Profile Header with Avatar
                            Center(
                              child: Column(
                                children: [
                                  CircleAvatar(
                                    radius: 38,
                                    backgroundColor: AppColors.accent.withAlpha(40),
                                    child: Text(
                                      detail.deceasedName.isNotEmpty
                                          ? detail.deceasedName[0].toUpperCase()
                                          : 'M',
                                      style: const TextStyle(
                                        fontSize: 32,
                                        fontWeight: FontWeight.bold,
                                        color: AppColors.accent,
                                      ),
                                    ),
                                  ),
                                  const SizedBox(height: 12),
                                  Text(
                                    detail.deceasedName,
                                    style: const TextStyle(
                                      fontSize: 22,
                                      fontWeight: FontWeight.bold,
                                    ),
                                  ),
                                  if (detail.title != null) ...[
                                    const SizedBox(height: 4),
                                    Text(
                                      detail.title!,
                                      style: TextStyle(
                                        fontStyle: FontStyle.italic,
                                        color: isDark ? AppColors.textDarkMuted : AppColors.textLightMuted,
                                        fontSize: 14,
                                      ),
                                    ),
                                  ],
                                  const SizedBox(height: 8),
                                  Text(
                                    '${detail.birthDate != null ? DateFormat('yyyy').format(detail.birthDate!) : '...'} — ${detail.deathDate != null ? DateFormat('yyyy').format(detail.deathDate!) : '...'}',
                                    style: const TextStyle(
                                      fontSize: 13,
                                      color: AppColors.primaryLight,
                                      fontWeight: FontWeight.w600,
                                    ),
                                  ),
                                ],
                              ),
                            ),
                            const SizedBox(height: 20),
                            const Divider(),
                            const SizedBox(height: 12),

                            // Biography
                            if (detail.shortBiography != null || detail.fullBiography != null) ...[
                              const Text(
                                'Biography & Life Legacy',
                                style: TextStyle(fontSize: 16, fontWeight: FontWeight.bold),
                              ),
                              const SizedBox(height: 8),
                              Text(
                                detail.fullBiography ?? detail.shortBiography ?? '',
                                style: TextStyle(
                                  height: 1.5,
                                  color: isDark ? AppColors.textDarkSecondary : AppColors.textLightSecondary,
                                  fontSize: 14,
                                ),
                              ),
                              const SizedBox(height: 20),
                            ],

                            // Timeline milestones
                            if (detail.timelineEvents.isNotEmpty) ...[
                              const Text(
                                'Life Milestones',
                                style: TextStyle(fontSize: 16, fontWeight: FontWeight.bold),
                              ),
                              const SizedBox(height: 12),
                              ...detail.timelineEvents.map((t) => Padding(
                                    padding: const EdgeInsets.only(bottom: 12),
                                    child: Row(
                                      crossAxisAlignment: CrossAxisAlignment.start,
                                      children: [
                                        Container(
                                          padding: const EdgeInsets.symmetric(horizontal: 8, vertical: 4),
                                          decoration: BoxDecoration(
                                            color: AppColors.primary.withAlpha(40),
                                            borderRadius: BorderRadius.circular(8),
                                          ),
                                          child: Text(
                                            '${t.eventYear}',
                                            style: const TextStyle(
                                              fontWeight: FontWeight.bold,
                                              fontSize: 12,
                                              color: AppColors.primaryLight,
                                            ),
                                          ),
                                        ),
                                        const SizedBox(width: 12),
                                        Expanded(
                                          child: Column(
                                            crossAxisAlignment: CrossAxisAlignment.start,
                                            children: [
                                              Text(t.title, style: const TextStyle(fontWeight: FontWeight.w600, fontSize: 13)),
                                              if (t.description != null)
                                                Text(
                                                  t.description!,
                                                  style: TextStyle(
                                                    fontSize: 12,
                                                    color: isDark ? AppColors.textDarkMuted : AppColors.textLightMuted,
                                                  ),
                                                ),
                                            ],
                                          ),
                                        ),
                                      ],
                                    ),
                                  )),
                              const SizedBox(height: 20),
                            ],

                            // Tributes
                            Row(
                              mainAxisAlignment: MainAxisAlignment.spaceBetween,
                              children: [
                                Text(
                                  'Tributes (${detail.tributes.length})',
                                  style: const TextStyle(fontSize: 16, fontWeight: FontWeight.bold),
                                ),
                                TextButton.icon(
                                  icon: const Icon(Icons.favorite, size: 16, color: AppColors.statusOccupied),
                                  label: const Text('Leave a Tribute'),
                                  onPressed: () => _showLeaveTributeDialog(context, detail.memorialId),
                                ),
                              ],
                            ),
                            const SizedBox(height: 8),
                            if (detail.tributes.isEmpty)
                              Padding(
                                padding: const EdgeInsets.symmetric(vertical: 16),
                                child: Center(
                                  child: Text(
                                    'No tributes posted yet. Be the first to share a warm memory.',
                                    style: TextStyle(
                                      color: isDark ? AppColors.textDarkMuted : AppColors.textLightMuted,
                                      fontSize: 13,
                                    ),
                                  ),
                                ),
                              )
                            else
                              ListView.separated(
                                shrinkWrap: true,
                                physics: const NeverScrollableScrollPhysics(),
                                itemCount: detail.tributes.length,
                                separatorBuilder: (_, __) => const SizedBox(height: 10),
                                itemBuilder: (context, idx) {
                                  final tribute = detail.tributes[idx];
                                  return Card(
                                    color: isDark ? AppColors.cardDark : AppColors.cardLight,
                                    child: Padding(
                                      padding: const EdgeInsets.all(14),
                                      child: Column(
                                        crossAxisAlignment: CrossAxisAlignment.start,
                                        children: [
                                          Row(
                                            mainAxisAlignment: MainAxisAlignment.spaceBetween,
                                            children: [
                                              Text(
                                                tribute.authorName,
                                                style: const TextStyle(
                                                  fontWeight: FontWeight.bold,
                                                  fontSize: 13,
                                                ),
                                              ),
                                              Text(
                                                DateFormat('MMM d, yyyy').format(tribute.createdAt),
                                                style: TextStyle(
                                                  fontSize: 11,
                                                  color: isDark ? AppColors.textDarkMuted : AppColors.textLightMuted,
                                                ),
                                              ),
                                            ],
                                          ),
                                          if (tribute.relationshipToDeceased != null)
                                            Text(
                                              tribute.relationshipToDeceased!,
                                              style: const TextStyle(
                                                fontSize: 11,
                                                color: AppColors.accent,
                                              ),
                                            ),
                                          if (tribute.message != null) ...[
                                            const SizedBox(height: 6),
                                            Text(
                                              tribute.message!,
                                              style: TextStyle(
                                                fontSize: 13,
                                                height: 1.4,
                                                color: isDark ? AppColors.textDarkSecondary : AppColors.textLightSecondary,
                                              ),
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

  void _showLeaveTributeDialog(BuildContext context, int memorialId) {
    final nameCtrl = TextEditingController();
    final relationCtrl = TextEditingController();
    final messageCtrl = TextEditingController();

    showDialog(
      context: context,
      builder: (ctx) => AlertDialog(
        title: const Text('Leave a Memorial Tribute'),
        content: Column(
          mainAxisSize: MainAxisSize.min,
          children: [
            TextField(
              controller: nameCtrl,
              decoration: const InputDecoration(labelText: 'Your Name *'),
            ),
            const SizedBox(height: 12),
            TextField(
              controller: relationCtrl,
              decoration: const InputDecoration(labelText: 'Relationship (e.g. Grandson, Friend)'),
            ),
            const SizedBox(height: 12),
            TextField(
              controller: messageCtrl,
              maxLines: 3,
              decoration: const InputDecoration(labelText: 'Your Tribute / Warm Message *'),
            ),
          ],
        ),
        actions: [
          TextButton(
            onPressed: () => Navigator.pop(ctx),
            child: const Text('Cancel'),
          ),
          ElevatedButton(
            onPressed: () async {
              if (nameCtrl.text.trim().isEmpty || messageCtrl.text.trim().isEmpty) return;
              await context.read<MemorialProvider>().addTribute(
                    memorialId,
                    authorName: nameCtrl.text.trim(),
                    relationshipToDeceased: relationCtrl.text.trim(),
                    tributeType: 'MESSAGE',
                    message: messageCtrl.text.trim(),
                  );
              if (ctx.mounted) Navigator.pop(ctx);
            },
            child: const Text('Submit Tribute'),
          ),
        ],
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    final provider = context.watch<MemorialProvider>();
    final isDark = Theme.of(context).brightness == Brightness.dark;

    return Scaffold(
      appBar: AppBar(
        title: const Text('Digital Memorials & Legacy'),
        actions: [
          IconButton(
            icon: const Icon(Icons.refresh),
            onPressed: () => context.read<MemorialProvider>().fetchMemorials(),
          ),
        ],
      ),
      body: provider.isLoading && provider.memorials.isEmpty
          ? const Center(child: CircularProgressIndicator())
          : provider.memorials.isEmpty
              ? Center(
                  child: Column(
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: [
                      Icon(
                        Icons.auto_stories_outlined,
                        size: 48,
                        color: isDark ? AppColors.textDarkMuted : AppColors.textLightMuted,
                      ),
                      const SizedBox(height: 12),
                      const Text('No memorial profiles published yet.'),
                    ],
                  ),
                )
              : RefreshIndicator(
                  onRefresh: () => context.read<MemorialProvider>().fetchMemorials(),
                  child: LayoutBuilder(
                    builder: (context, constraints) {
                      int crossAxisCount = constraints.maxWidth > 900
                          ? 3
                          : (constraints.maxWidth > 600 ? 2 : 1);
                      return GridView.builder(
                        padding: const EdgeInsets.all(20),
                        gridDelegate: SliverGridDelegateWithFixedCrossAxisCount(
                          crossAxisCount: crossAxisCount,
                          crossAxisSpacing: 16,
                          mainAxisSpacing: 16,
                          childAspectRatio: 1.35,
                        ),
                        itemCount: provider.memorials.length,
                        itemBuilder: (context, index) {
                          final item = provider.memorials[index];
                          return Card(
                            child: InkWell(
                              onTap: () => _showMemorialDetailModal(context, item.memorialId),
                              borderRadius: BorderRadius.circular(16),
                              child: Padding(
                                padding: const EdgeInsets.all(16),
                                child: Column(
                                  crossAxisAlignment: CrossAxisAlignment.start,
                                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                                  children: [
                                    Row(
                                      children: [
                                        CircleAvatar(
                                          radius: 24,
                                          backgroundColor: AppColors.accent.withAlpha(40),
                                          child: Text(
                                            item.deceasedName.isNotEmpty
                                                ? item.deceasedName[0].toUpperCase()
                                                : 'M',
                                            style: const TextStyle(
                                              fontWeight: FontWeight.bold,
                                              color: AppColors.accent,
                                              fontSize: 18,
                                            ),
                                          ),
                                        ),
                                        const SizedBox(width: 12),
                                        Expanded(
                                          child: Column(
                                            crossAxisAlignment: CrossAxisAlignment.start,
                                            children: [
                                              Text(
                                                item.deceasedName,
                                                maxLines: 1,
                                                overflow: TextOverflow.ellipsis,
                                                style: const TextStyle(
                                                  fontWeight: FontWeight.bold,
                                                  fontSize: 15,
                                                ),
                                              ),
                                              if (item.title != null)
                                                Text(
                                                  item.title!,
                                                  maxLines: 1,
                                                  overflow: TextOverflow.ellipsis,
                                                  style: TextStyle(
                                                    fontSize: 12,
                                                    color: isDark ? AppColors.textDarkMuted : AppColors.textLightMuted,
                                                  ),
                                                ),
                                            ],
                                          ),
                                        ),
                                      ],
                                    ),
                                    Text(
                                      '${item.birthDate != null ? DateFormat('yyyy').format(item.birthDate!) : '...'} — ${item.deathDate != null ? DateFormat('yyyy').format(item.deathDate!) : '...'}',
                                      style: const TextStyle(
                                        fontSize: 12,
                                        color: AppColors.primaryLight,
                                        fontWeight: FontWeight.w600,
                                      ),
                                    ),
                                    Row(
                                      mainAxisAlignment: MainAxisAlignment.spaceBetween,
                                      children: [
                                        Row(
                                          children: [
                                            const Icon(
                                              Icons.favorite,
                                              size: 14,
                                              color: AppColors.statusOccupied,
                                            ),
                                            const SizedBox(width: 4),
                                            Text(
                                              '${item.tributeCount} tributes',
                                              style: TextStyle(
                                                fontSize: 11,
                                                color: isDark ? AppColors.textDarkMuted : AppColors.textLightMuted,
                                              ),
                                            ),
                                          ],
                                        ),
                                        const Text(
                                          'View Memorial →',
                                          style: TextStyle(
                                            fontSize: 11,
                                            fontWeight: FontWeight.w600,
                                            color: AppColors.primaryLight,
                                          ),
                                        ),
                                      ],
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
    );
  }
}
