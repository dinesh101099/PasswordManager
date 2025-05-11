───────────────────────────────────────────── PERSONAL PASSWORD MANAGER – README ─────────────────────────────────────────────

OVERVIEW: This is a secure personal password manager application developed as two separate projects:

Backend API using C# .NET 8

Frontend Web Application using Angular and TypeScript

The backend uses SQL Server (running in Docker) to store encrypted passwords, while the frontend interacts with the API for password operations.

───────────────────────────────────────────── TECHNOLOGY STACK: ───────────────────────────────────────────── Backend:

.NET 8 (ASP.NET Core Web API)

Entity Framework Core (Database-First)

SQL Server (in Docker container)

Swagger for API documentation

Frontend:

Angular

TypeScript

HTML/CSS for responsive design

───────────────────────────────────────────── BACKEND FEATURES: ───────────────────────────────────────────── API Endpoints:

Passwords: GET /api/Passwords

POST /api/Passwords

GET /api/Passwords/{id}

PUT /api/Passwords/{id}

DELETE /api/Passwords/{id}

GET /api/Passwords/by-username/{userName}

Password Format (Sample): { "id": 10, "category": "Social Media", "app": "Instagram", "userName": "jane_doe_87", "encryptedPassword": "UEBzc3cwcmQyMDI1MTIxMjMxMjM=" }

Security:

Passwords stored using Base64 encoding (ASCII ⇄ Base64)

Database:

SQL Server hosted in Docker

Connected using Entity Framework Core (DB-First approach)

───────────────────────────────────────────── FRONTEND FEATURES: ─────────────────────────────────────────────

List all passwords

View specific password (with option to decrypt)

Add new password

Edit and update existing password (shown decrypted)

Delete password from list

Responsive UI with form validations

Communicates with backend API for all data operations

───────────────────────────────────────────── SETUP INSTRUCTIONS: ─────────────────────────────────────────────

1)START SQL SERVER IN DOCKER:

Make sure you have Docker Desktop installed and running properly. Then, run the following command in the Command Prompt (CMD) to create an instance of SQL Server in Docker

docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=dinesh@1010" -p 1433:1433 --name sqlserver-container -d mcr.microsoft.com/mssql/server:2022-latest

"ConnectionStrings": { "DefaultConnection": "Server=localhost,1433;Database=PasswordManager;User Id=sa;Password=dinesh@1010;" }

Open SQL Server and connect the database instance which we created in the docker, then run the below query.

CREATE DATABASE PasswordManager; CREATE TABLE Passwords ( Id INT PRIMARY KEY IDENTITY, Category NVARCHAR(100), App NVARCHAR(100), UserName NVARCHAR(255), EncryptedPassword NVARCHAR(MAX) );

2)BACKEND SETUP:

Navigate to the backend folder

dotnet restore dotnet run Press F5 to host.

Open your browser and visit: http://localhost:5066/swagger/index.html

3)FRONTEND SETUP:

Navigate to the frontend folder

Run the following commands:

npm install (or try) npm install --legacy-peer-deps

ng serve

Open your browser and visit: http://localhost:4200

───────────────────────────────────────────── END OF README ─────────────────────────────────────────────