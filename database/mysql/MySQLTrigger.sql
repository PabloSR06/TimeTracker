DELIMITER $$
CREATE TRIGGER `userUpdateTime`
BEFORE UPDATE ON `TimeTracker`.`user`
FOR EACH ROW
BEGIN
    SET NEW.update_time = NOW();
END$$
DELIMITER ;

DELIMITER $$
CREATE TRIGGER `colectionUpdateTime`
BEFORE UPDATE ON `TimeTracker`.`colection`
FOR EACH ROW
BEGIN
    SET NEW.update_time = NOW();
END$$
DELIMITER ;

DELIMITER $$
CREATE TRIGGER `projectUpdateTime`
BEFORE UPDATE ON `TimeTracker`.`project`
FOR EACH ROW
BEGIN
    SET NEW.update_time = NOW();
END$$
DELIMITER ;

DELIMITER $$
CREATE TRIGGER `user_has_projectUpdateTime`
BEFORE UPDATE ON `TimeTracker`.`user_has_project`
FOR EACH ROW
BEGIN
    SET NEW.update_time = NOW();
END$$
DELIMITER ;
