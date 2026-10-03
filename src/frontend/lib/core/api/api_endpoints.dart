class ApiEndpoints {
  // Default base URL for API (can be overridden via environment or settings)
  static const String baseUrl = 'http://localhost:5189/api';

  // Auth
  static const String login = '$baseUrl/auth/login';
  static const String register = '$baseUrl/auth/register';
  static const String me = '$baseUrl/auth/me';
  static const String changePassword = '$baseUrl/auth/change-password';
  static const String users = '$baseUrl/auth/users';

  // Graveyards
  static const String graveyards = '$baseUrl/graveyards';
  static String graveyardDetail(int id) => '$baseUrl/graveyards/$id';
  static String graveyardSections(int id) => '$baseUrl/graveyards/$id/sections';
  static String sectionBlocks(int sectionId) => '$baseUrl/graveyards/sections/$sectionId/blocks';
  static const String blocks = '$baseUrl/graveyards/blocks';

  // Graves & Burials
  static const String graves = '$baseUrl/graves';
  static String graveDetail(int id) => '$baseUrl/graves/$id';
  static String gravesByStatus(String status) => '$baseUrl/graves/status/$status';
  static const String burials = '$baseUrl/burials';
  static String burialDetail(int id) => '$baseUrl/burials/$id';
  static String burialsByGrave(int graveId) => '$baseUrl/burials/grave/$graveId';

  // Deceased Persons
  static const String deceased = '$baseUrl/deceased';
  static String deceasedDetail(int id) => '$baseUrl/deceased/$id';
  static const String deceasedSearch = '$baseUrl/deceased/search';
  static String deceasedVerify(int id) => '$baseUrl/deceased/$id/verify';
  static String deceasedApprove(int id) => '$baseUrl/deceased/$id/approve';

  // Memorials
  static const String memorials = '$baseUrl/memorials';
  static String memorialDetail(int id) => '$baseUrl/memorials/$id';
  static String memorialBySlug(String slug) => '$baseUrl/memorials/slug/$slug';
  static String memorialTributes(int id) => '$baseUrl/memorials/$id/tributes';
  static String memorialTimeline(int id) => '$baseUrl/memorials/$id/timeline';
  static String memorialMemories(int id) => '$baseUrl/memorials/$id/memories';

  // Finance & Dashboard
  static const String donations = '$baseUrl/finance/donations';
  static const String expenses = '$baseUrl/finance/expenses';
  static const String funds = '$baseUrl/finance/funds';
  static const String dashboard = '$baseUrl/dashboard';
}
