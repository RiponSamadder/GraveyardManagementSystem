import 'package:flutter/material.dart';
import '../core/api/api_client.dart';
import '../core/api/api_endpoints.dart';
import '../models/deceased_model.dart';

class DeceasedProvider with ChangeNotifier {
  final ApiClient _api = ApiClient();

  List<DeceasedItem> _deceasedList = [];
  DeceasedDetail? _selectedDeceased;
  bool _isLoading = false;
  String? _errorMessage;
  String _searchQuery = '';

  List<DeceasedItem> get deceasedList => _deceasedList;
  DeceasedDetail? get selectedDeceased => _selectedDeceased;
  bool get isLoading => _isLoading;
  String? get errorMessage => _errorMessage;
  String get searchQuery => _searchQuery;

  Future<void> fetchDeceased({String? query}) async {
    _isLoading = true;
    _errorMessage = null;
    if (query != null) _searchQuery = query;
    notifyListeners();

    final url = _searchQuery.isNotEmpty
        ? ApiEndpoints.deceasedSearch
        : ApiEndpoints.deceased;

    final queryParams = _searchQuery.isNotEmpty ? {'q': _searchQuery} : null;

    final response = await _api.get<PagedData<DeceasedItem>>(
      url,
      queryParams: queryParams,
      fromJson: (data) => PagedData.fromJson(
        data as Map<String, dynamic>,
        (item) => DeceasedItem.fromJson(item),
      ),
    );

    _isLoading = false;
    if (response.success && response.data != null) {
      _deceasedList = response.data!.items;
    } else {
      _errorMessage = response.message ?? 'Failed to load deceased records';
    }
    notifyListeners();
  }

  Future<void> fetchDeceasedDetail(int id) async {
    _isLoading = true;
    _errorMessage = null;
    notifyListeners();

    final response = await _api.get<DeceasedDetail>(
      ApiEndpoints.deceasedDetail(id),
      fromJson: (data) => DeceasedDetail.fromJson(data as Map<String, dynamic>),
    );

    _isLoading = false;
    if (response.success && response.data != null) {
      _selectedDeceased = response.data;
    } else {
      _errorMessage = response.message ?? 'Failed to load details';
    }
    notifyListeners();
  }

  Future<bool> verifyDeceased(int id, String remarks) async {
    final response = await _api.post(
      ApiEndpoints.deceasedVerify(id),
      body: {'remarks': remarks},
    );
    if (response.success) {
      await fetchDeceased();
      return true;
    }
    return false;
  }

  Future<bool> approveDeceased(int id, String remarks) async {
    final response = await _api.post(
      ApiEndpoints.deceasedApprove(id),
      body: {'remarks': remarks},
    );
    if (response.success) {
      await fetchDeceased();
      return true;
    }
    return false;
  }
}
