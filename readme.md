# RSA ADI Stage 1 Exam Simulator

## 🎯 Project Goal

A full-stack C# web application to simulate the **RSA ADI Stage 1 theory exam**, designed for private study, exam preparation, and realistic practice.  
The system generates exams with weighted random question selection reflecting the official RSA exam structure and conditions.

---

## 🔧 Technology Stack

| Layer           | Technology                       |
|-----------------|--------------------------------|
| Frontend        | Blazor WebAssembly (C#)          |
| Backend         | ASP.NET Core Web API (C#)        |
| Database (Dev)  | SQLite                          |
| Database (Prod) | PostgreSQL (planned)             |
| Deployment      | Render.com                      |
| Authentication  | JWT-based (email + password)    |
| Payment         | Stripe Integration (pre-registration) |

---

## 🔐 Authentication & Payment Flow

- Payment processed via Stripe **before** user registration.
- Registration enabled **only after** successful payment.
- Passwords stored securely with **BCrypt hashing**.
- JWT tokens issued on successful login, used for API authorization.
- Admin users flagged with `IsAdmin` column for elevated access.

---

## 🗄 Database Schema Overview

### Users Table

| Column        | Type          | Description                |
|---------------|---------------|----------------------------|
| `UserID`      | INTEGER (PK)  | Primary key                |
| `Email`       | TEXT          | Unique email address       |
| `PasswordHash`| TEXT          | Hashed password            |
| `IsActive`    | BOOLEAN       | Active status after payment|
| `IsAdmin`     | BOOLEAN       | Admin flag                 |
| `CreatedAt`   | DATETIME      | Registration timestamp     |

### Questions Table

| Column        | Type          | Description                |
|---------------|---------------|----------------------------|
| `ID`          | INTEGER (PK)  | Primary key                |
| `QuestionText`| TEXT          | Full question text         |
| `OptionA-D`   | TEXT          | Four multiple-choice options|
| `CorrectOption`| TEXT(1)      | Correct option ("A"-"D")   |
| `Category`    | TEXT          | Question category          |
| `Weighting`   | REAL          | Weight for exam generation |
| `Explanation` | TEXT          | Optional explanation text  |
| `IsEnabled`   | BOOLEAN       | Enabled/disabled flag      |

### Results Table

| Column        | Type          | Description                |
|---------------|---------------|----------------------------|
| `ResultID`    | INTEGER (PK)  | Primary key                |
| `UserID`      | INTEGER (FK)  | Linked user                |
| `ExamDate`    | DATETIME      | Timestamp of exam attempt  |
| `TotalQuestions`| INTEGER     | Total questions answered   |
| `CorrectCount`| INTEGER       | Number correct             |
| `Score`       | REAL          | Percentage score           |
| `BreakdownJSON`| TEXT         | Detailed per-question data |

---

## 🏗 API Endpoints

| Endpoint                      | Description                            | Auth Required? |
|-------------------------------|------------------------------------|---------------|
| `POST /auth/register`         | Register user after Stripe payment   | No            |
| `POST /auth/login`            | Login, receive JWT token             | No            |
| `POST /questions/add`         | Admin: Bulk import questions (JSON) | Yes (Admin)   |
| `POST /questions/update`      | Admin: Update question details       | Yes (Admin)   |
| `POST /questions/enable-disable/{id}` | Enable/disable a question      | Yes (Admin)   |
| `DELETE /questions/delete/{id}`| Delete a question                   | Yes (Admin)   |
| `GET /questions/random`       | Generate weighted random exam        | Yes           |
| `POST /results/submit`        | Submit exam results                  | Yes           |
| `GET /results/history`        | Retrieve user exam history           | Yes           |

---

## 🎯 Exam Generator Logic

- **Full Exam Mode:** 100 questions total
  - 80 Generic questions
  - 20 Category-specific (Category B)

### Category Weightings

| Category                 | Weighting |
|--------------------------|-----------|
| Rules of the Road        | 25%       |
| Driving Techniques       | 15%       |
| Teaching & Learning Theory| 20%       |
| Driver Psychology        | 10%       |
| Legal / ADI Procedures   | 15%       |
| EDT / Test Standards     | 15%       |

- Weighted random selection without question duplication.
- Only `IsEnabled` questions included.

---

## 🖥 Frontend Pages (Blazor WebAssembly)

- Home / Dashboard
- Login / Register
- Start New Exam (Full or Quick)
- Exam Interface (Multiple Choice Questions)
- Results Page (Score, Review, Explanations)
- History Page (Past Exam Results)
- Admin Panel (Upload, Enable/Disable, Delete Questions)

---

## 🚫 Out of Scope for MVP v1.2

- Social login
- Adaptive learning systems
- Leaderboards
- Exam timer

---

## ✅ Current Status

- Core backend and frontend scaffolding complete
- Authentication & authorization functional
- Admin panel implemented
- Exam generation engine operational
- Currently building: full exam flow UI

---

## 🚀 Deployment Target

- Render.com hosting
- Server: ASP.NET Core Web API
- Client: Blazor WebAssembly static site
- Dev database: SQLite; Prod database: PostgreSQL (planned)

---

## 📂 Repository Structure

adi-simulator/
├── AdiExamSimulator.Server/ # ASP.NET Core Web API backend
├── AdiExamSimulator.Client/ # Blazor WebAssembly frontend
├── adi-simulator.sln # Solution file
└── .gitignore

yaml
Copy

---

## 🛠 Development & Build Instructions

### Prerequisites

- .NET 9 SDK
- Optional: Node.js / npm (if using frontend tools)

### Build and Run

```bash
git clone https://github.com/SimplyAI-IE/adi-simulator.git
cd adi-simulator

dotnet build AdiExamSimulator.Server
dotnet build AdiExamSimulator.Client

# Run server
dotnet run --project AdiExamSimulator.Server

# Run client (Blazor WASM)
dotnet run --project AdiExamSimulator.Client
💡 Developer Notes
Use Entity Framework Core migrations for DB schema changes

Follow RSA official phrasing style for questions

Expand question pool progressively via admin panel or imports

Ensure exam conditions are realistic, avoid “training wheels”

📄 License
MIT License

Thank you for using the RSA ADI Stage 1 Exam Simulator!
Please contribute improvements or report issues on GitHub.

This README is kept up-to-date with project progress.