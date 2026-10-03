import 'package:flutter/material.dart';
import 'package:shared_preferences/shared_preferences.dart';
import '../core/api/api_client.dart';
import '../core/api/api_endpoints.dart';
import '../models/auth_model.dart';

class AuthProvider with ChangeNotifier {
  final ApiClient _api = ApiClient();
  LoginResponse? _currentUser;
  bool _isLoading = false;
  String? _errorMessage;

  LoginResponse? get currentUser => _currentUser;
  bool get isAuthenticated => _currentUser != null;
  bool get isLoading => _isLoading;
  String? get errorMessage => _errorMessage;

  Future<bool> login(String username, String password) async {
    _isLoading = true;
    _errorMessage = null;
    notifyListeners();

    final response = await _api.post<LoginResponse>(
      ApiEndpoints.login,
      body: {'username': username, 'password': password},
      fromJson: (data) => LoginResponse.fromJson(data as Map<String, dynamic>),
    );

    _isLoading = false;
    if (response.success && response.data != null) {
      _currentUser = response.data;
      _api.setToken(response.data!.token);

      final prefs = await SharedPreferences.getInstance();
      await prefs.setString('auth_token', response.data!.token);
      await prefs.setString('user_fullname', response.data!.fullName);
      await prefs.setString('user_code', response.data!.userCode);
      if (response.data!.email != null) {
        await prefs.setString('user_email', response.data!.email!);
      }

      notifyListeners();
      return true;
    } else {
      _errorMessage = response.message ?? 'Invalid username or password';
      notifyListeners();
      return false;
    }
  }

  Future<bool> register({
    required String userCode,
    required String fullName,
    required String username,
    required String password,
    String? email,
    String? mobileNo,
  }) async {
    _isLoading = true;
    _errorMessage = null;
    notifyListeners();

    final response = await _api.post(
      ApiEndpoints.register,
      body: {
        'userCode': userCode,
        'fullName': fullName,
        'username': username,
        'password': password,
        'email': email,
        'mobileNo': mobileNo,
      },
    );

    _isLoading = false;
    if (response.success) {
      notifyListeners();
      return true;
    } else {
      _errorMessage = response.message ?? 'Registration failed';
      notifyListeners();
      return false;
    }
  }

  Future<void> tryAutoLogin() async {
    final prefs = await SharedPreferences.getInstance();
    final token = prefs.getString('auth_token');
    if (token == null || token.isEmpty) return;

    _api.setToken(token);
    _isLoading = true;
    notifyListeners();

    final response = await _api.get<UserProfile>(
      ApiEndpoints.me,
      fromJson: (data) => UserProfile.fromJson(data as Map<String, dynamic>),
    );

    _isLoading = false;
    if (response.success && response.data != null) {
      final profile = response.data!;
      _currentUser = LoginResponse(
        userId: profile.userId,
        userCode: profile.userCode,
        fullName: profile.fullName,
        email: profile.email,
        mobileNo: profile.mobileNo,
        token: token,
        expiresAt: DateTime.now().add(const Duration(days: 1)),
        roles: profile.roles,
      );
    } else {
      await logout();
    }
    notifyListeners();
  }

  Future<void> logout() async {
    _currentUser = null;
    _api.setToken(null);
    final prefs = await SharedPreferences.getInstance();
    await prefs.remove('auth_token');
    await prefs.remove('user_fullname');
    await prefs.remove('user_code');
    await prefs.remove('user_email');
    notifyListeners();
  }
}
