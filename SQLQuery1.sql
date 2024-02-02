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

-- Inserts
INSERT INTO Clients (Name) 
VALUES ('Client X'),
       ('Client Y');
       
INSERT INTO Projects (Name, Description, ClientID)
VALUES 
    ('Project 1', 'Description for Project 1', 1),
    ('Project 2', 'Description for Project 2', 1),
    ('Project 3', 'Description for Project 3', 1),
    ('Project A', 'Description for Project A', 1),
	('Project B', 'Description for Project B', 1);
    
INSERT INTO Users (Name, Email, Password) 
VALUES ('John Doe', 'john@example.com', 'password123'),
       ('Jane Smith', 'jane@example.com', 'securepwd');


INSERT INTO DayHours (UserID, Type, Date) 
VALUES (1, 1, '2024-01-24 08:00:00'),
       (1, 0, '2024-01-24 16:00:00'), 
       (1, 1, '2024-01-25 08:00:00'),
       (1, 0, '2024-01-25 16:00:00'),
       (1, 1, '2024-01-26 08:00:00');

INSERT INTO ProjectHours (UserID, ProjectID, Minutes, Date) 
VALUES (1, 1, 120, '2024-01-24 16:00:00'),
       (2, 2, 180, '2024-01-24 16:00:00');


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
    SELECT ph.id, ph.userid, ph.projectid, ph.minutes, ph.date, p.name 'ProjectName', p.description, c.Name AS 'ClientName'
	FROM ProjectHours AS ph
	INNER JOIN Projects AS p ON ph.ProjectID = p.id
	INNER JOIN Clients AS c ON p.ClientID = c.Id
	WHERE ph.UserID = userId 
	AND ph.Date BETWEEN  `from` AND  `to`;

END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE InsertProjectHours(IN `userId` INT, IN `projectId` INT, IN `minutes` INT, IN `date` DATE)
BEGIN
    INSERT INTO `ProjectHours` (UserID, ProjectID, Minutes, Date) 
    VALUES (`userId`, `projectId`, `minutes`, `date`);
END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE InsertDayHours(IN `userId` INT, IN `type` BIT,IN `date` DATETIME)
BEGIN
    INSERT INTO `DayHours` (UserID, Type, Date) 
    VALUES (`userId`, `type`, `date`);
END //
DELIMITER ;



