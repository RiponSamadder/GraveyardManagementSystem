import 'package:flutter/material.dart';
import '../core/api/api_client.dart';
import '../core/api/api_endpoints.dart';
import '../models/dashboard_model.dart';

class DashboardProvider with ChangeNotifier {
  final ApiClient _api = ApiClient();
  DashboardSummary? _summary;
  bool _isLoading = false;
  String? _errorMessage;

  DashboardSummary? get summary => _summary;
  bool get isLoading => _isLoading;
  String? get errorMessage => _errorMessage;

  Future<void> fetchSummary() async {
    _isLoading = true;
    _errorMessage = null;
    notifyListeners();

    final response = await _api.get<DashboardSummary>(
      ApiEndpoints.dashboard,
      fromJson: (data) => DashboardSummary.fromJson(data as Map<String, dynamic>),
    );

    _isLoading = false;
    if (response.success && response.data != null) {
      _summary = response.data;
    } else {
      _errorMessage = response.message ?? 'Failed to load dashboard metrics';
    }
    notifyListeners();
  }
}
