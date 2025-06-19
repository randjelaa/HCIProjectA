CREATE DATABASE IF NOT EXISTS VetClinic;
USE VetClinic;

-- Roles
CREATE TABLE Roles (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(50) NOT NULL
);

-- Users
CREATE TABLE Users (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL UNIQUE,
    Password VARCHAR(255) NOT NULL,
    Deleted datetime,
    RoleID INT,
    FOREIGN KEY (RoleID) REFERENCES Roles(ID)
);

-- User Preferences
CREATE TABLE UserPreferences (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    UserID INT NOT NULL,
    Theme VARCHAR(50),
    Language VARCHAR(10),
    FOREIGN KEY (UserID) REFERENCES Users(ID) ON DELETE CASCADE
);

-- Services
CREATE TABLE Services (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Description TEXT,
    Price DECIMAL(10,2) NOT NULL,
    DurationMinutes INT,
    Deleted datetime
);

-- Pets
CREATE TABLE Pets (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Species VARCHAR(50),
    Breed VARCHAR(50),
    OwnerID INT NOT NULL,
	Deleted datetime,
    FOREIGN KEY (OwnerID) REFERENCES Users(ID) ON DELETE CASCADE
);

-- Appointments
CREATE TABLE Appointments (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    Date DATETIME NOT NULL,
    Deleted datetime,
    PetID INT NOT NULL,
    VetID INT NOT NULL,
    ServiceID INT NOT NULL,
    FOREIGN KEY (PetID) REFERENCES Pets(ID) ON DELETE CASCADE,
    FOREIGN KEY (VetID) REFERENCES Users(ID),
    FOREIGN KEY (ServiceID) REFERENCES Services(ID)
);

-- Medical Records
CREATE TABLE MedicalRecords (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    AppointmentID INT NOT NULL,
    Diagnosis TEXT,
    Treatment TEXT,
    Medications TEXT,
    Notes TEXT,
    FOREIGN KEY (AppointmentID) REFERENCES Appointments(ID) ON DELETE CASCADE
);

-- Payments
CREATE TABLE Payments (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    Amount DECIMAL(10,2) NOT NULL,
    Date DATETIME NOT NULL,
    UserID INT NOT NULL,
    AppointmentID INT,
    FOREIGN KEY (UserID) REFERENCES Users(ID),
    FOREIGN KEY (AppointmentID) REFERENCES Appointments(ID)
);

-- Unpaid Services
CREATE TABLE UnpaidServices (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    UserID INT NOT NULL,
    AppointmentID INT,
    Amount DECIMAL(10,2) NOT NULL,
    Status ENUM('pending', 'overdue', 'partial') DEFAULT 'pending',
    FOREIGN KEY (UserID) REFERENCES Users(ID),
    FOREIGN KEY (AppointmentID) REFERENCES Appointments(ID)
);
