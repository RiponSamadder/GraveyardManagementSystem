import 'package:flutter/material.dart';
import '../core/api/api_client.dart';
import '../core/api/api_endpoints.dart';
import '../models/graveyard_model.dart';
import '../models/grave_model.dart';

class GraveyardProvider with ChangeNotifier {
  final ApiClient _api = ApiClient();

  List<GraveyardItem> _graveyards = [];
  GraveyardDetail? _selectedGraveyard;
  List<GraveItem> _graves = [];
  String _selectedGraveStatusFilter = 'ALL';
  bool _isLoading = false;
  String? _errorMessage;

  List<GraveyardItem> get graveyards => _graveyards;
  GraveyardDetail? get selectedGraveyard => _selectedGraveyard;
  List<GraveItem> get graves => _graves;
  String get selectedGraveStatusFilter => _selectedGraveStatusFilter;
  bool get isLoading => _isLoading;
  String? get errorMessage => _errorMessage;

  Future<void> fetchGraveyards() async {
    _isLoading = true;
    _errorMessage = null;
    notifyListeners();

    final response = await _api.get<PagedData<GraveyardItem>>(
      ApiEndpoints.graveyards,
      fromJson: (data) => PagedData.fromJson(
        data as Map<String, dynamic>,
        (item) => GraveyardItem.fromJson(item),
      ),
    );

    _isLoading = false;
    if (response.success && response.data != null) {
      _graveyards = response.data!.items;
    } else {
      _errorMessage = response.message ?? 'Failed to load graveyards';
    }
    notifyListeners();
  }

  Future<void> fetchGraveyardDetail(int id) async {
    _isLoading = true;
    _errorMessage = null;
    notifyListeners();

    final response = await _api.get<GraveyardDetail>(
      ApiEndpoints.graveyardDetail(id),
      fromJson: (data) => GraveyardDetail.fromJson(data as Map<String, dynamic>),
    );

    _isLoading = false;
    if (response.success && response.data != null) {
      _selectedGraveyard = response.data;
    } else {
      _errorMessage = response.message ?? 'Failed to load graveyard details';
    }
    notifyListeners();
  }

  Future<void> fetchGraves({String? status}) async {
    _isLoading = true;
    _errorMessage = null;
    if (status != null) _selectedGraveStatusFilter = status;
    notifyListeners();

    final url = (_selectedGraveStatusFilter == 'ALL' || _selectedGraveStatusFilter.isEmpty)
        ? ApiEndpoints.graves
        : ApiEndpoints.gravesByStatus(_selectedGraveStatusFilter);

    final response = await _api.get<PagedData<GraveItem>>(
      url,
      fromJson: (data) => PagedData.fromJson(
        data as Map<String, dynamic>,
        (item) => GraveItem.fromJson(item),
      ),
    );

    _isLoading = false;
    if (response.success && response.data != null) {
      _graves = response.data!.items;
    } else {
      _errorMessage = response.message ?? 'Failed to load graves';
    }
    notifyListeners();
  }

  void filterGravesByStatus(String status) {
    _selectedGraveStatusFilter = status;
    fetchGraves(status: status);
  }
}
