# RSA ADI Stage 1 Exam Simulator

## 🎯 Project Goal

A full-stack web application to simulate the **RSA ADI Stage 1 theory exam** for private study, exam practice, and personal preparation.  
The system generates realistic exam conditions using weighted random question selection to closely match real RSA exam structure.

---

## 🔧 Technology Stack

| Layer | Technology |
| ----- | ----------- |
| Frontend | **Blazor WebAssembly (C#)** |
| Backend | **ASP.NET Core Web API (C#)** |
| Database (Dev) | **SQLite** |
| Database (Prod) | **PostgreSQL** (planned) |
| Deployment | **Render.com** |
| Auth | **JWT-based Authentication** |
| Payment | **Stripe Integration (Pre-Registration Payment)** |

---

## 🔐 Authentication & Payment Flow

- User pays via Stripe before registration.
- User can register with email/password after successful payment.
- Passwords stored securely using BCrypt hashing.
- JWT issued on successful login.
- JWT used to protect secured API endpoints.
- Admin users flagged via `IsAdmin` column.

---

## 🗄 Database Schema

### Users Table

| Column | Type | Description |
| ------ | ---- | ----------- |
| `UserID` | INTEGER (PK) | Primary key |
| `Email` | TEXT | User email (unique) |
| `PasswordHash` | TEXT | Hashed password |
| `IsActive` | BOOLEAN | Active after payment |
| `IsAdmin` | BOOLEAN | Admin flag |
| `CreatedAt` | DATETIME | Registration timestamp |

### Questions Table

| Column | Type | Description |
| ------ | ---- | ----------- |
| `ID` | INTEGER (PK) | Primary key |
| `QuestionText` | TEXT | Full question text |
| `OptionA` - `OptionD` | TEXT | Multiple choice options |
| `CorrectOption` | TEXT(1) | Correct answer ("A", "B", "C", or "D") |
| `Category` | TEXT | Question category |
| `Weighting` | REAL | Weighted value for random selection |
| `Explanation` | TEXT | Explanation for review mode |
| `IsEnabled` | BOOLEAN | Active/inactive flag |

### Results Table

| Column | Type | Description |
| ------ | ---- | ----------- |
| `ResultID` | INTEGER (PK) | Primary key |
| `UserID` | INTEGER (FK) | Linked user |
| `ExamDate` | DATETIME | Date of attempt |
| `TotalQuestions` | INTEGER | Total questions answered |
| `CorrectCount` | INTEGER | Number correct |
| `Score` | REAL | Percentage score |
| `BreakdownJSON` | TEXT | Per-question answers & details |

---

## 🏗 API Endpoints

| Endpoint | Description | Auth Required? |
| -------- | ----------- | -------------- |
| `POST /auth/register` | Register user after Stripe payment | ❌ |
| `POST /auth/login` | User login, returns JWT | ❌ |
| `POST /questions/add` | Admin bulk import questions via JSON | ✅ Admin |
| `POST /questions/update` | Admin update question | ✅ Admin |
| `POST /questions/enable-disable/{id}` | Enable/disable question | ✅ Admin |
| `DELETE /questions/delete/{id}` | Delete question | ✅ Admin |
| `GET /questions/random` | Generate weighted random exam | ✅ |
| `POST /results/submit` | Submit exam result | ✅ |
| `GET /results/history` | Retrieve exam history for user | ✅ |

---

## 🎯 Exam Generator Logic

- Full Exam Mode: **100 questions total**
  - 80 Generic questions
  - 20 Category-Specific questions (Category B)

### Category Weightings

| Category | Weighting |
| -------- | --------- |
| Rules of the Road | 25% |
| Driving Techniques | 15% |
| Teaching & Learning Theory | 20% |
| Driver Psychology | 10% |
| Legal / ADI Procedures | 15% |
| EDT / Test Standards | 15% |

- Weighted random selection across categories
- No duplicate questions per exam
- Only `IsEnabled == true` questions included

---

## 🖥 Frontend Pages (Blazor WebAssembly)

- **Home / Dashboard**
- **Login / Register**
- **Start New Exam**
- **Exam Interface (MCQ)**
- **Results Page (Score + Explanations)**
- **History Page (Past Results)**
- **Admin Panel (Upload / Manage Questions)**

---

## 🚫 Out of Scope (MVP v1.2)

- Social login
- Adaptive learning system
- Leaderboards
- Exam timer functionality

---

## ✅ Status

Spec fully locked ✅  
Core backend & frontend scaffolding complete ✅  
Authentication, authorization, admin panel functional ✅  
Exam generation engine operational ✅  
Currently entering: **Exam Flow UI Build Phase**

---

## 🚀 Deployment Target

Render.com  
- Server deployed as ASP.NET Core Web Service  
- Client deployed as Static Blazor WASM app

---

## 📂 Repository Structure

adi-simulator/
├── AdiExamSimulator.Server/ # ASP.NET Core Web API
├── AdiExamSimulator.Client/ # Blazor WebAssembly Frontend
├── adi-simulator.sln # Solution file
└── .gitignore

yaml
Copy
Edit

---

# 📝 Developer Notes

- Uses Entity Framework Core with SQLite for local dev
- Use `dotnet ef migrations` for DB schema updates
- Use GitHub for source control & CI/CD to Render

---

# 📅 Next Planned Build Stage

- Exam flow UI build:
  - Start Exam
  - Take Exam (MCQ)
  - Submit Results
  - View Result History

---