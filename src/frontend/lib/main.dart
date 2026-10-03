import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'core/theme/app_theme.dart';
import 'providers/auth_provider.dart';
import 'providers/dashboard_provider.dart';
import 'providers/graveyard_provider.dart';
import 'providers/deceased_provider.dart';
import 'providers/memorial_provider.dart';
import 'providers/finance_provider.dart';
import 'screens/auth/login_screen.dart';
import 'screens/shell/app_shell.dart';

void main() async {
  WidgetsFlutterBinding.ensureInitialized();

  final authProvider = AuthProvider();
  await authProvider.tryAutoLogin();

  runApp(
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
}

class DgmmsApp extends StatelessWidget {
  const DgmmsApp({super.key});

  @override
  Widget build(BuildContext context) {
    final auth = context.watch<AuthProvider>();

    return MaterialApp(
      title: 'Digital Graveyard & Memorial Management System',
      debugShowCheckedModeBanner: false,
      theme: AppTheme.lightTheme,
      darkTheme: AppTheme.darkTheme,
      themeMode: ThemeMode.dark, // Executive dark mode default
      home: auth.isAuthenticated ? const AppShell() : const LoginScreen(),
    );
  }
}
