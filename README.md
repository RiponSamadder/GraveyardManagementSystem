# Digital Graveyard & Memorial Management System (DGMMS)
> *স্মৃতির আঙিনা — A comprehensive digital ecosystem for graveyard administration and permanent digital memorials.*

[![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![Flutter](https://img.shields.io/badge/Flutter-3.32-02569B?logo=flutter)](https://flutter.dev/)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-Enterprise-CC292B?logo=microsoftsqlserver)](https://www.microsoft.com/sql-server)

---

## 📖 Overview

The **Digital Graveyard & Memorial Management System (DGMMS)** bridges official cemetery administration with enduring human remembrance. Built according to enterprise business requirements, the platform maintains two synchronized record streams:

1. **Physical Record (Graveyard Operations)**: Grave mapping, sections/blocks/rows, burial register, committee administration, maintenance scheduling, and financial accounting.
2. **Human Record (Digital Memorial)**: Respectful deceased biography, life milestones, personal reflections/lessons, family lineage, moderated community tributes, photo archives, and grave-side QR navigation.

---

## 🏛 Architecture

The solution uses a **Clean Architecture** mono-repo:

```
├── docs/                      # Business Requirement Documents (BRD) & Specifications
├── src/
│   ├── backend/               # ASP.NET Core 8 Web API
│   │   ├── GMS.Core/          # Domain Entities, Enums & Interfaces
│   │   ├── GMS.Application/   # Business Logic, Services, DTOs & Validators
│   │   ├── GMS.Infrastructure/# EF Core, SQL Server, Auth, QR Code & File Storage
│   │   └── GMS.Api/           # REST API, JWT Auth & OpenAPI / Swagger
│   └── frontend/              # Flutter Cross-Platform Application (Web, Android, iOS, Windows)
│       └── lib/               # Clean Flutter UI, Bloc/Provider State, Localization (EN/BN)
```

---

## 🛠 Tech Stack

- **Backend**: ASP.NET Core 8 Web API, Entity Framework Core 8, Microsoft SQL Server
- **Frontend**: Flutter 3.32 (Dart 3.8), Responsive Web & Cross-platform Mobile
- **Security**: JWT Bearer Tokens, Role-Based Access Control (RBAC), Audit Trails (`AUD_AuditLog`)
- **Database**: Microsoft SQL Server (48 Domain Tables across `GMS`, `MEM`, `FIN`, `CFG`, `AUD`)

---

## 👥 Roles & Workflows

- **Super Administrator**: System configuration, organization settings, role/permission grants.
- **Graveyard Administrator**: Burials, grave occupancy, workers, and physical maintenance.
- **Committee Chairman & Members**: Governance, meeting resolutions, expenditure approvals.
- **Treasurer**: Double-entry financial vouchers, donor receipts, fund balancing.
- **Community & Families**: Profile curation, story submissions, tributes (moderation queue), QR scanning.

---

## 📜 License
Developed by [Ripon Samadder](https://github.com/RiponSamadder).
