class DonationItem {
  final int donationId;
  final String receiptNumber;
  final String donorName;
  final String? donorPhone;
  final double amount;
  final String paymentMethod;
  final DateTime donationDate;
  final String? fundName;
  final String? remarks;

  DonationItem({
    required this.donationId,
    required this.receiptNumber,
    required this.donorName,
    this.donorPhone,
    required this.amount,
    required this.paymentMethod,
    required this.donationDate,
    this.fundName,
    this.remarks,
  });

  factory DonationItem.fromJson(Map<String, dynamic> json) {
    return DonationItem(
      donationId: json['donationId'] ?? 0,
      receiptNumber: json['receiptNumber'] ?? '',
      donorName: json['donorName'] ?? '',
      donorPhone: json['donorPhone'],
      amount: (json['amount'] as num?)?.toDouble() ?? 0.0,
      paymentMethod: json['paymentMethod'] ?? 'CASH',
      donationDate: DateTime.tryParse(json['donationDate'] ?? '') ?? DateTime.now(),
      fundName: json['fundName'],
      remarks: json['remarks'],
    );
  }
}

class ExpenseItem {
  final int expenseId;
  final String voucherNumber;
  final String expenseCategory;
  final double amount;
  final String paymentMethod;
  final DateTime expenseDate;
  final String? approvedByName;
  final String? description;

  ExpenseItem({
    required this.expenseId,
    required this.voucherNumber,
    required this.expenseCategory,
    required this.amount,
    required this.paymentMethod,
    required this.expenseDate,
    this.approvedByName,
    this.description,
  });

  factory ExpenseItem.fromJson(Map<String, dynamic> json) {
    return ExpenseItem(
      expenseId: json['expenseId'] ?? 0,
      voucherNumber: json['voucherNumber'] ?? '',
      expenseCategory: json['expenseCategory'] ?? 'GENERAL',
      amount: (json['amount'] as num?)?.toDouble() ?? 0.0,
      paymentMethod: json['paymentMethod'] ?? 'CASH',
      expenseDate: DateTime.tryParse(json['expenseDate'] ?? '') ?? DateTime.now(),
      approvedByName: json['approvedByName'],
      description: json['description'],
    );
  }
}

class FundItem {
  final int fundId;
  final String fundCode;
  final String fundName;
  final double currentBalance;
  final bool isActive;

  FundItem({
    required this.fundId,
    required this.fundCode,
    required this.fundName,
    required this.currentBalance,
    required this.isActive,
  });

  factory FundItem.fromJson(Map<String, dynamic> json) {
    return FundItem(
      fundId: json['fundId'] ?? 0,
      fundCode: json['fundCode'] ?? '',
      fundName: json['fundName'] ?? '',
      currentBalance: (json['currentBalance'] as num?)?.toDouble() ?? 0.0,
      isActive: json['isActive'] ?? true,
    );
  }
}
