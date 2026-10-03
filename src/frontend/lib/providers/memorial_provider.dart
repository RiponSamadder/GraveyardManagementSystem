import 'package:flutter/material.dart';
import '../core/api/api_client.dart';
import '../core/api/api_endpoints.dart';
import '../models/memorial_model.dart';

class MemorialProvider with ChangeNotifier {
  final ApiClient _api = ApiClient();

  List<MemorialProfileItem> _memorials = [];
  MemorialProfileDetail? _selectedMemorial;
  bool _isLoading = false;
  String? _errorMessage;

  List<MemorialProfileItem> get memorials => _memorials;
  MemorialProfileDetail? get selectedMemorial => _selectedMemorial;
  bool get isLoading => _isLoading;
  String? get errorMessage => _errorMessage;

  Future<void> fetchMemorials() async {
    _isLoading = true;
    _errorMessage = null;
    notifyListeners();

    final response = await _api.get<PagedData<MemorialProfileItem>>(
      ApiEndpoints.memorials,
      fromJson: (data) => PagedData.fromJson(
        data as Map<String, dynamic>,
        (item) => MemorialProfileItem.fromJson(item),
      ),
    );

    _isLoading = false;
    if (response.success && response.data != null) {
      _memorials = response.data!.items;
    } else {
      _errorMessage = response.message ?? 'Failed to load memorial profiles';
    }
    notifyListeners();
  }

  Future<void> fetchMemorialDetail(int id) async {
    _isLoading = true;
    _errorMessage = null;
    notifyListeners();

    final response = await _api.get<MemorialProfileDetail>(
      ApiEndpoints.memorialDetail(id),
      fromJson: (data) => MemorialProfileDetail.fromJson(data as Map<String, dynamic>),
    );

    _isLoading = false;
    if (response.success && response.data != null) {
      _selectedMemorial = response.data;
    } else {
      _errorMessage = response.message ?? 'Failed to load memorial details';
    }
    notifyListeners();
  }

  Future<bool> addTribute(int memorialId, {
    required String authorName,
    String? authorEmail,
    String? relationshipToDeceased,
    required String tributeType,
    required String message,
  }) async {
    final response = await _api.post(
      ApiEndpoints.memorialTributes(memorialId),
      body: {
        'memorialId': memorialId,
        'authorName': authorName,
        'authorEmail': authorEmail,
        'relationshipToDeceased': relationshipToDeceased,
        'tributeType': tributeType,
        'message': message,
      },
    );

    if (response.success) {
      await fetchMemorialDetail(memorialId);
      return true;
    }
    return false;
  }
}
