DROP TRIGGER IF EXISTS `userUpdateTime`;
DELIMITER $$
CREATE TRIGGER `userUpdateTime`
BEFORE UPDATE ON `TimeTracker`.`user`
FOR EACH ROW
BEGIN
    SET NEW.update_time = NOW();
END$$
DELIMITER ;

DROP TRIGGER IF EXISTS `colectionUpdateTime` ;
DELIMITER $$
CREATE TRIGGER `colectionUpdateTime`
BEFORE UPDATE ON `TimeTracker`.`collection`
FOR EACH ROW
BEGIN
    SET NEW.update_time = NOW();
END$$
DELIMITER ;

DROP TRIGGER IF EXISTS `projectUpdateTime` ;
DELIMITER $$
CREATE TRIGGER `projectUpdateTime`
BEFORE UPDATE ON `TimeTracker`.`project`
FOR EACH ROW
BEGIN
    SET NEW.update_time = NOW();
END$$
DELIMITER ;

DROP TRIGGER IF EXISTS `clockHistoryUpdateTime` ;
DELIMITER $$
CREATE TRIGGER `clockHistoryUpdateTime`
BEFORE UPDATE ON `TimeTracker`.`clockHistory`
FOR EACH ROW
BEGIN
    SET NEW.update_time = NOW();
END$$
DELIMITER ;
