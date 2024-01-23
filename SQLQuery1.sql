DROP DATABASE IF EXISTS timeTracker;

CREATE DATABASE timeTracker;

USE timeTracker;

CREATE TABLE Users (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL,
    Email NVARCHAR(256) NOT NULL,
    Password NVARCHAR(256) NOT NULL,
    CreateOnDate DATETIME,
    LastModifiedOnDate DATETIME
);

CREATE TABLE Projects (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(256) NOT NULL,
    CreateOnDate DATETIME,
    LastModifiedOnDate DATETIME
);

CREATE TABLE Clients (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(256) NOT NULL,
    CreateOnDate DATETIME,
    LastModifiedOnDate DATETIME
);

CREATE TABLE DayHours (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    UserID INT,
    Type BIT NOT NULL,
    Date DATETIME NOT NULL,
    CreateOnDate DATETIME,
    LastModifiedOnDate DATETIME,
    FOREIGN KEY (UserID) REFERENCES Users(Id)
);

CREATE TABLE ProjectHours (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    UserID INT,
    ProjectID INT,
    Minutes INT NOT NULL,
    CreateOnDate DATETIME,
    LastModifiedOnDate DATETIME,
    FOREIGN KEY (UserID) REFERENCES Users(Id),
    FOREIGN KEY (ProjectID) REFERENCES Projects(Id)
);

-- Triggers
CREATE TRIGGER tr_insertProject
BEFORE INSERT ON Projects FOR EACH ROW
SET NEW.CreateOnDate = NOW(), NEW.LastModifiedOnDate = NOW();
CREATE TRIGGER tr_insertClient
BEFORE INSERT ON Projects FOR EACH ROW
SET NEW.CreateOnDate = NOW(), NEW.LastModifiedOnDate = NOW();
CREATE TRIGGER tr_insertUser
BEFORE INSERT ON Projects FOR EACH ROW
SET NEW.CreateOnDate = NOW(), NEW.LastModifiedOnDate = NOW();
CREATE TRIGGER tr_insertProjectHours
BEFORE INSERT ON Projects FOR EACH ROW
SET NEW.CreateOnDate = NOW(), NEW.LastModifiedOnDate = NOW();
CREATE TRIGGER tr_insertDayHours
BEFORE INSERT ON Projects FOR EACH ROW
SET NEW.CreateOnDate = NOW(), NEW.LastModifiedOnDate = NOW();


CREATE TRIGGER tr_updateProject
BEFORE UPDATE ON Projects FOR EACH ROW
SET NEW.LastModifiedOnDate = NOW();
CREATE TRIGGER tr_updateClient
BEFORE UPDATE ON Clients FOR EACH ROW
SET NEW.LastModifiedOnDate = NOW();
CREATE TRIGGER tr_updateUser
BEFORE UPDATE ON Users FOR EACH ROW
SET NEW.LastModifiedOnDate = NOW();
CREATE TRIGGER tr_updateProjectHours
BEFORE UPDATE ON ProjectHours FOR EACH ROW
SET NEW.LastModifiedOnDate = NOW();
CREATE TRIGGER tr_updateDayHours
BEFORE UPDATE ON DayHours FOR EACH ROW
SET NEW.LastModifiedOnDate = NOW();

-- Inserts
INSERT INTO Projects (Name, Description)
VALUES 
    ('Project 1', 'Description for Project 1'),
    ('Project 2', 'Description for Project 2'),
    ('Project 3', 'Description for Project 3');
