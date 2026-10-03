import 'package:flutter/material.dart';
import '../core/api/api_client.dart';
import '../core/api/api_endpoints.dart';
import '../models/finance_model.dart';

class FinanceProvider with ChangeNotifier {
  final ApiClient _api = ApiClient();

  List<DonationItem> _donations = [];
  List<ExpenseItem> _expenses = [];
  List<FundItem> _funds = [];
  bool _isLoading = false;
  String? _errorMessage;

  List<DonationItem> get donations => _donations;
  List<ExpenseItem> get expenses => _expenses;
  List<FundItem> get funds => _funds;
  bool get isLoading => _isLoading;
  String? get errorMessage => _errorMessage;

  Future<void> fetchFinanceData() async {
    _isLoading = true;
    _errorMessage = null;
    notifyListeners();

    final donationsRes = await _api.get<PagedData<DonationItem>>(
      ApiEndpoints.donations,
      fromJson: (data) => PagedData.fromJson(
        data as Map<String, dynamic>,
        (item) => DonationItem.fromJson(item),
      ),
    );

    final expensesRes = await _api.get<PagedData<ExpenseItem>>(
      ApiEndpoints.expenses,
      fromJson: (data) => PagedData.fromJson(
        data as Map<String, dynamic>,
        (item) => ExpenseItem.fromJson(item),
      ),
    );

    final fundsRes = await _api.get<List<FundItem>>(
      ApiEndpoints.funds,
      fromJson: (data) => (data as List<dynamic>)
          .map((e) => FundItem.fromJson(e as Map<String, dynamic>))
          .toList(),
    );

    _isLoading = false;
    if (donationsRes.success && donationsRes.data != null) {
      _donations = donationsRes.data!.items;
    }
    if (expensesRes.success && expensesRes.data != null) {
      _expenses = expensesRes.data!.items;
    }
    if (fundsRes.success && fundsRes.data != null) {
      _funds = fundsRes.data!;
    }

    notifyListeners();
  }
}
