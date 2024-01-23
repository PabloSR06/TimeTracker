DROP DATABASE timeTracker;

CREATE DATABASE timeTracker;

USE timeTracker;

CREATE TABLE Users (
    [Id] [INT] IDENTITY(1,1) PRIMARY KEY,
	[Name] [NVARCHAR](50) NOT NULL,
	[Email] [NVARCHAR](256) NOT NULL,
	[Password] [NVARCHAR](256) NOT NULL,
	[CreateOnDate] [DATETIME],
	[LastModifiedOnDate] [DATETIME]
);

CREATE TABLE Projects (
    [Id] [INT] IDENTITY(1,1) PRIMARY KEY,
	[Name] [NVARCHAR](100) NOT NULL,
	[Description] [NVARCHAR](256) NOT NULL,
	[CreateOnDate] [DATETIME],
	[LastModifiedOnDate] [DATETIME]
);

CREATE TABLE Clients (
    [Id] [INT] IDENTITY(1,1) PRIMARY KEY,
	[Name] [NVARCHAR](100) NOT NULL,
	[Description] [NVARCHAR](256) NOT NULL,
	[CreateOnDate] [DATETIME],
	[LastModifiedOnDate] [DATETIME]
);

CREATE TABLE DayHours (
    [Id] [INT] IDENTITY(1,1) PRIMARY KEY,
	[UserID] [INT] REFERENCES Users(Id),
	[Type] [bit] NOT NULL,
	[Date] [DATETIME] NOT NULL,
	[CreateOnDate] [DATETIME],
	[LastModifiedOnDate] [DATETIME]
);


CREATE TABLE ProjectHours (
    [Id] [INT] IDENTITY(1,1) PRIMARY KEY,
	[UserID] [INT] REFERENCES Users(Id),
	[ProjectID] [INT] REFERENCES Projects(Id),
	[Minutes] [INT] NOT NULL,
	[CreateOnDate] [DATETIME],
	[LastModifiedOnDate] [DATETIME]
);



-- Drop the database if it exists
DROP DATABASE IF EXISTS timeTracker;

-- Create the timeTracker database
CREATE DATABASE timeTracker;

-- Switch to the timeTracker database
USE timeTracker;

-- Create the Users table
CREATE TABLE Users (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL,
    Email NVARCHAR(256) NOT NULL,
    Password NVARCHAR(256) NOT NULL,
    CreateOnDate DATETIME,
    LastModifiedOnDate DATETIME
);

-- Create the Projects table
CREATE TABLE Projects (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(256) NOT NULL,
    CreateOnDate DATETIME,
    LastModifiedOnDate DATETIME
);

-- Create the Clients table
CREATE TABLE Clients (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(256) NOT NULL,
    CreateOnDate DATETIME,
    LastModifiedOnDate DATETIME
);

-- Create the DayHours table
CREATE TABLE DayHours (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    UserID INT,
    Type BIT NOT NULL,
    Date DATETIME NOT NULL,
    CreateOnDate DATETIME,
    LastModifiedOnDate DATETIME,
    FOREIGN KEY (UserID) REFERENCES Users(Id)
);

-- Create the ProjectHours table
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

