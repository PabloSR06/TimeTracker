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

-- Inserts
INSERT INTO Projects (Name, Description)
VALUES 
    ('Project 1', 'Description for Project 1'),
    ('Project 2', 'Description for Project 2'),
    ('Project 3', 'Description for Project 3');
    
INSERT INTO Users (Name, Email, Password) 
VALUES ('John Doe', 'john@example.com', 'password123'),
       ('Jane Smith', 'jane@example.com', 'securepwd');

INSERT INTO Projects (Name, Description) 
VALUES ('Project A', 'Description for Project A'),
       ('Project B', 'Description for Project B');

INSERT INTO Clients (Name, Description) 
VALUES ('Client X', 'Description for Client X'),
       ('Client Y', 'Description for Client Y');

INSERT INTO DayHours (UserID, Type, Date) 
VALUES (1, 1, '2024-01-24 08:00:00'),
       (1, 0, '2024-01-24 16:00:00'), 
       (1, 1, '2024-01-25 08:00:00'),
       (1, 0, '2024-01-25 16:00:00'),
       (1, 1, '2024-01-26 08:00:00');

INSERT INTO ProjectHours (UserID, ProjectID, Minutes) 
VALUES (1, 1, 120),
       (2, 2, 180);

    
    
-- Procedures
DELIMITER //
CREATE PROCEDURE GetDayHours(IN userId INT, IN `from` DATETIME, IN `to` DATETIME)
BEGIN
    SELECT * FROM `DayHours` WHERE UserID = userId AND `Date` BETWEEN `from` AND `to`;
END //
DELIMITER ;

SELECT * FROM `DayHours`
