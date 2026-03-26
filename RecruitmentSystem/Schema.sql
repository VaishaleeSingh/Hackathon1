-- CREATE DATABASE RecruitmentDb;
-- GO

-- USE RecruitmentDb;
-- GO

CREATE TABLE Roles (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL
);

CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(100) NOT NULL,
    Email NVARCHAR(255) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(MAX) NOT NULL,
    RoleId INT NOT NULL,
    FOREIGN KEY (RoleId) REFERENCES Roles(Id)
);

CREATE TABLE Candidates (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    ResumeUrl NVARCHAR(MAX),
    Skills NVARCHAR(MAX),
    Experience NVARCHAR(MAX),
    Education NVARCHAR(MAX),
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);

CREATE TABLE Jobs (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX) NOT NULL,
    Location NVARCHAR(100),
    SalaryRange DECIMAL(18, 2),
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETUTCDATE(),
    CreatedById INT NOT NULL,
    FOREIGN KEY (CreatedById) REFERENCES Users(Id)
);

CREATE TABLE Applications (
    Id INT PRIMARY KEY IDENTITY(1,1),
    JobId INT NOT NULL,
    CandidateId INT NOT NULL,
    ApplicationDate DATETIME DEFAULT GETUTCDATE(),
    Status INT DEFAULT 0, -- 0: Pending, 1: Reviewed, 2: Interview, 3: Rejected, 4: Selected
    FOREIGN KEY (JobId) REFERENCES Jobs(Id),
    FOREIGN KEY (CandidateId) REFERENCES Candidates(Id)
);

-- Seed Data
INSERT INTO Roles (Name) VALUES ('Admin'), ('Recruiter'), ('Candidate');
GO
