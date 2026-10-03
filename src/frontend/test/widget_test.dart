import 'package:flutter_test/flutter_test.dart';
import 'package:provider/provider.dart';
import 'package:gms_client/main.dart';
import 'package:gms_client/providers/auth_provider.dart';
import 'package:gms_client/providers/dashboard_provider.dart';
import 'package:gms_client/providers/graveyard_provider.dart';
import 'package:gms_client/providers/deceased_provider.dart';
import 'package:gms_client/providers/memorial_provider.dart';
import 'package:gms_client/providers/finance_provider.dart';

void main() {
  testWidgets('App initialization smoke test', (WidgetTester tester) async {
    final authProvider = AuthProvider();

    await tester.pumpWidget(
      MultiProvider(
        providers: [
          ChangeNotifierProvider.value(value: authProvider),
          ChangeNotifierProvider(create: (_) => DashboardProvider()),
          ChangeNotifierProvider(create: (_) => GraveyardProvider()),
          ChangeNotifierProvider(create: (_) => DeceasedProvider()),
          ChangeNotifierProvider(create: (_) => MemorialProvider()),
          ChangeNotifierProvider(create: (_) => FinanceProvider()),
        ],
        child: const DgmmsApp(),
      ),
    );

    // Verify DGMMS Portal or Login Screen renders
    expect(find.text('DGMMS Portal'), findsOneWidget);
  });
}
