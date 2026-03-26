# 🚀 Recruitment Management System

A premium, full-stack recruitment management platform engineered with **.NET 10.0** (Clean Architecture) and **Angular 19** (Standalone Components).

---

## 🌟 Key Highlights
- **Framework**: Powered by **.NET 10.0** for cutting-edge performance and scalability.
- **Frontend**: Modern **Angular 19** with standalone components and zoneless-ready architecture.
- **Security**: Robust **JWT Authentication** and **Role-Based Access Control (RBAC)**.
- **Architecture**: Strict adherence to **Clean Architecture** patterns (Domain, Application, Infrastructure, API).

---

## 🛠️ Tech Stack
- **Backend**: ASP.NET Core Web API (.NET 10.0), Entity Framework Core, SQL Server (SSMS), JWT Auth.
- **Frontend**: Angular 19, Angular Material, RxJS Signals.
- **Database**: SQL Server (LocalDB or SSMS instance).

---

## ⚙️ Quick Start Guide

### 1. Database Initialization
1.  Open **SQL Server Management Studio (SSMS)**.
2.  Connect to your instance: **Recruitment**.
3.  Ensure the database **registration** exists (or create it and run `RecruitmentSystem/Schema.sql`).
4.  Verify the connection string in `RecruitmentSystem.API/appsettings.json` is set to `Server=Recruitment;Database=registration`.

### 2. Backend Launch (Port 5123)
```bash
cd RecruitmentSystem
dotnet restore
dotnet build
dotnet run --project RecruitmentSystem.API\RecruitmentSystem.API.csproj --urls "http://localhost:5123"
```
> [!NOTE]
> The Swagger UI is globally enabled and available at [http://localhost:5123/swagger](http://localhost:5123/swagger) for API testing.

### 3. Frontend Launch (Port 4200)
```bash
cd recruitment-frontend
npm install
npm start
```
Open [http://localhost:4200](http://localhost:4200) to access the Recruitment Portal.

---

## 🔗 Core API Endpoints
- `POST /api/auth/register` - Create a new user (Admin, Recruiter, Candidate)
- `POST /api/auth/login` - Authenticate and retrieve JWT token
- `GET /api/jobs` - Browse available job listings
- `POST /api/applications/apply/{jobId}` - Submit a candidacy (Candidate role)
- `PATCH /api/applications/{id}/status` - Manage application lifecycle (Admin/Recruiter)

---

## ✨ Features
- **Identity Management**: Secure login/registration with `BCrypt` password hashing.
- **Job Lifecycle**: End-to-end recruitment flow from job posting to application status tracking.
- **Clean Architecture**: Decoupled layers ensuring easy maintenance and testability.
- **Premium UI**: Responsive design with Angular Material, optimized for both desktop and mobile viewports.
- **Auto-Mapping**: Type-safe DTO mappings using AutoMapper.

