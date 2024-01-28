DROP DATABASE IF EXISTS timeTracker;

CREATE DATABASE timeTracker;

USE timeTracker;

CREATE TABLE Users (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL,
    Email NVARCHAR(256) NOT NULL,
    Password NVARCHAR(256) NOT NULL,
    CreateOnDate DATETIME NOT NULL,
    LastModifiedOnDate DATETIME NOT NULL
);

CREATE TABLE Projects (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(256),
    CreateOnDate DATETIME NOT NULL,
    LastModifiedOnDate DATETIME NOT NULL
);

CREATE TABLE Clients (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    CreateOnDate DATETIME NOT NULL,
    LastModifiedOnDate DATETIME NOT NULL
);

CREATE TABLE DayHours (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    UserID INT NOT NULL,
    Type BIT NOT NULL,
    Date DATETIME NOT NULL,
    CreateOnDate DATETIME NOT NULL,
    LastModifiedOnDate DATETIME NOT NULL,
    FOREIGN KEY (UserID) REFERENCES Users(Id)
);

CREATE TABLE ProjectHours (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    UserID INT NOT NULL,
    ProjectID INT NOT NULL,
    Minutes INT NOT NULL DEFAULT 0,
    Date DATE NOT NULL, 
    CreateOnDate DATETIME NOT NULL,
    LastModifiedOnDate DATETIME NOT NULL,
    FOREIGN KEY (UserID) REFERENCES Users(Id),
    FOREIGN KEY (ProjectID) REFERENCES Projects(Id)
);

-- Inserts
INSERT INTO Projects (Name, Description, CreateOnDate, LastModifiedOnDate)
VALUES 
    ('Project 1', 'Description for Project 1', NOW(), NOW()),
    ('Project 2', 'Description for Project 2', NOW(), NOW()),
    ('Project 3', 'Description for Project 3', NOW(), NOW());
    
INSERT INTO Users (Name, Email, Password, CreateOnDate, LastModifiedOnDate) 
VALUES ('John Doe', 'john@example.com', 'password123', NOW(), NOW()),
       ('Jane Smith', 'jane@example.com', 'securepwd', NOW(), NOW());

INSERT INTO Projects (Name, Description, CreateOnDate, LastModifiedOnDate) 
VALUES ('Project A', 'Description for Project A', NOW(), NOW()),
       ('Project B', 'Description for Project B', NOW(), NOW());

INSERT INTO Clients (Name, CreateOnDate, LastModifiedOnDate) 
VALUES ('Client X', NOW(), NOW()),
       ('Client Y', NOW(), NOW());

INSERT INTO DayHours (UserID, Type, Date, CreateOnDate, LastModifiedOnDate) 
VALUES (1, 1, '2024-01-24 08:00:00', NOW(), NOW()),
       (1, 0, '2024-01-24 16:00:00', NOW(), NOW()), 
       (1, 1, '2024-01-25 08:00:00', NOW(), NOW()),
       (1, 0, '2024-01-25 16:00:00', NOW(), NOW()),
       (1, 1, '2024-01-26 08:00:00', NOW(), NOW());

INSERT INTO ProjectHours (UserID, ProjectID, Minutes, Date, CreateOnDate, LastModifiedOnDate) 
VALUES (1, 1, 120, '2024-01-24 16:00:00', NOW(), NOW()),
       (2, 2, 180, '2024-01-24 16:00:00', NOW(), NOW());

    
    
-- Procedures
DELIMITER //
CREATE PROCEDURE GetDayHours(IN userId INT, IN `from` DATETIME, IN `to` DATETIME)
BEGIN
    SELECT * FROM `DayHours` WHERE UserID = userId AND `Date` BETWEEN `from` AND `to`;
END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE GetProjectHours(IN userId INT, IN `from` DATETIME, IN `to` DATETIME)
BEGIN
    SELECT ph.id, ph.userid, ph.projectid, ph.minutes, ph.date, p.name, p.description
	FROM ProjectHours AS ph
	INNER JOIN Projects AS p ON ph.ProjectID = p.id
	WHERE ph.UserID = userId 
	AND ph.Date BETWEEN  `from` AND  `to`;

END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE InsertProjectHours(IN `userId` INT, IN `projectId` INT, IN `minutes` INT, IN `date` DATE, IN `createOnDate` DATETIME, IN `lastModifiedOnDate` DATETIME)
BEGIN
    INSERT INTO `ProjectHours` (UserID, ProjectID, Minutes, Date, CreateOnDate, LastModifiedOnDate) 
    VALUES (`userId`, `projectId`, `minutes`, `date`, `createOnDate`, `lastModifiedOnDate`);
END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE InsertDayHours(IN `userId` INT, IN `type` BIT,IN `date` DATETIME,IN `createOnDate` DATETIME, IN `lastModifiedOnDate` DATETIME)
BEGIN
    INSERT INTO `DayHours` (UserID, Type, Date, CreateOnDate, LastModifiedOnDate) 
    VALUES (`userId`, `type`, `date`, `createOnDate`, `lastModifiedOnDate`);
END //
DELIMITER ;





