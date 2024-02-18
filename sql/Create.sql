DROP DATABASE IF EXISTS timeTracker;

CREATE DATABASE timeTracker;

USE timeTracker;

SET GLOBAL time_zone = '+00:00';

CREATE TABLE Users (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL,
    Email NVARCHAR(256) NOT NULL,
    Password NVARCHAR(256) NOT NULL,
    CreateOnDate DATETIME NOT NULL default NOW(),
    LastModifiedOnDate DATETIME NOT NULL default NOW()
);
CREATE TABLE roles (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(50) NOT NULL
);

CREATE TABLE Clients (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    CreateOnDate DATETIME NOT NULL default NOW(),
    LastModifiedOnDate DATETIME NOT NULL default NOW()
);

CREATE TABLE Projects (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(256),
    ClientID INT NOT NULL,
    CreateOnDate DATETIME NOT NULL default NOW(),
    LastModifiedOnDate DATETIME NOT NULL default NOW(),
	FOREIGN KEY (ClientID) REFERENCES Clients(Id)

);

CREATE TABLE DayHours (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    UserID INT NOT NULL,
    Type BIT NOT NULL,
    Date DATETIME NOT NULL,
    CreateOnDate DATETIME NOT NULL default NOW(),
    LastModifiedOnDate DATETIME NOT NULL default NOW(),
    FOREIGN KEY (UserID) REFERENCES Users(Id)
);

CREATE TABLE ProjectHours (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    UserID INT NOT NULL,
    ProjectID INT NOT NULL,
    Minutes INT NOT NULL DEFAULT 0,
    Date DATE NOT NULL, 
    CreateOnDate DATETIME NOT NULL default NOW(),
    LastModifiedOnDate DATETIME NOT NULL default NOW(),
    FOREIGN KEY (UserID) REFERENCES Users(Id),
    FOREIGN KEY (ProjectID) REFERENCES Projects(Id)
);

CREATE TABLE user_roles (
    user_id INT,
    role_id INT,
    PRIMARY KEY (user_id, role_id),
    FOREIGN KEY (user_id) REFERENCES users(id),
    FOREIGN KEY (role_id) REFERENCES roles(id)
);