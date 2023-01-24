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
BEFORE UPDATE ON `TimeTracker`.`collection`
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
BEFORE UPDATE ON `TimeTracker`.`groupHasProject`
FOR EACH ROW
BEGIN
    SET NEW.update_time = NOW();
END$$
DELIMITER ;

DELIMITER $$
CREATE TRIGGER `groupHasCollectionUpdateTime`
BEFORE UPDATE ON `TimeTracker`.`groupHasCollection`
FOR EACH ROW
BEGIN
    SET NEW.update_time = NOW();
END$$
DELIMITER ;

DELIMITER $$
CREATE TRIGGER `clockHistoryUpdateTime`
BEFORE UPDATE ON `TimeTracker`.`clockHistory`
FOR EACH ROW
BEGIN
    SET NEW.update_time = NOW();
END$$
DELIMITER ;

DELIMITER $$
CREATE TRIGGER `groupUpdateTime`
BEFORE UPDATE ON `TimeTracker`.`group`
FOR EACH ROW
BEGIN
    SET NEW.update_time = NOW();
END$$
DELIMITER ;
