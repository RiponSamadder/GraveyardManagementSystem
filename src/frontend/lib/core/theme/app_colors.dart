import 'package:flutter/material.dart';

class AppColors {
  // Brand Colors
  static const Color primary = Color(0xFF0F766E); // Teal / Deep Pine
  static const Color primaryLight = Color(0xFF14B8A6);
  static const Color primaryDark = Color(0xFF134E4A);
  
  static const Color accent = Color(0xFFD97706); // Warm Amber
  static const Color accentLight = Color(0xFFFDE68A);

  // Status Colors
  static const Color statusAvailable = Color(0xFF10B981); // Emerald
  static const Color statusOccupied = Color(0xFFEF4444);  // Coral Red
  static const Color statusReserved = Color(0xFFF59E0B);  // Amber
  static const Color statusMaintenance = Color(0xFF6366F1); // Indigo

  // Neutral Colors (Dark Mode Default for Executive / Solemn aesthetic)
  static const Color bgDark = Color(0xFF0F172A);      // Slate 900
  static const Color surfaceDark = Color(0xFF1E293B); // Slate 800
  static const Color cardDark = Color(0xFF243044);    // Custom elevated card
  static const Color borderDark = Color(0xFF334155);  // Slate 700

  // Light Mode Colors
  static const Color bgLight = Color(0xFFF8FAFC);     // Slate 50
  static const Color surfaceLight = Color(0xFFFFFFFF);
  static const Color cardLight = Color(0xFFFFFFFF);
  static const Color borderLight = Color(0xFFE2E8F0);

  // Text Colors
  static const Color textDarkPrimary = Color(0xFFF1F5F9);
  static const Color textDarkSecondary = Color(0xFF94A3B8);
  static const Color textDarkMuted = Color(0xFF64748B);

  static const Color textLightPrimary = Color(0xFF0F172A);
  static const Color textLightSecondary = Color(0xFF475569);
  static const Color textLightMuted = Color(0xFF94A3B8);
}
