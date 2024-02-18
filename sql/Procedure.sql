DELIMITER //
CREATE PROCEDURE GetDayHours(IN `userIdInput` INT, IN `from` DATETIME, IN `to` DATETIME)
BEGIN
    SELECT * FROM `DayHours` WHERE UserID = `userIdInput` AND `Date` BETWEEN `from` AND `to`;
END //
DELIMITER ;


DELIMITER //
CREATE PROCEDURE GetProjectHours(IN `userIdInput` INT, IN `from` DATETIME, IN `to` DATETIME)
BEGIN
    SELECT ph.id, ph.userid, ph.projectid, ph.minutes, ph.date, p.name 'ProjectName', p.description, c.Name AS 'ClientName'
	FROM ProjectHours AS ph
	INNER JOIN Projects AS p ON ph.ProjectID = p.id
	INNER JOIN Clients AS c ON p.ClientID = c.Id
	WHERE ph.UserID = `userIdInput` 
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

DELIMITER //

CREATE PROCEDURE ResetPassword(IN `id` INT, IN `password` LONGTEXT)
BEGIN
	UPDATE Users SET Password = `password` WHERE id = `id`;
END //

DELIMITER ;



DELIMITER //
CREATE PROCEDURE UserLogIn(IN `inputEmail` NVARCHAR(250), IN `inputPassword` NVARCHAR(500))
BEGIN
    SELECT users.id, users.name, users.`Email`, 
           GROUP_CONCAT(roles.name) AS Roles
    FROM users
    LEFT JOIN user_roles ON users.id = user_roles.user_id
    LEFT JOIN roles ON user_roles.role_id = roles.id
    WHERE users.email = `inputEmail` AND users.password = `inputPassword`
    GROUP BY users.Id;
END //

DELIMITER ;

