DROP TRIGGER IF EXISTS `userUpdateTime`;
DELIMITER $$
CREATE TRIGGER `userUpdateTime`
BEFORE UPDATE ON `db_timetracker`.`user`
FOR EACH ROW
BEGIN
    SET NEW.update_time = NOW();
END$$
DELIMITER ;

DROP TRIGGER IF EXISTS `colectionUpdateTime` ;
DELIMITER $$
CREATE TRIGGER `colectionUpdateTime`
BEFORE UPDATE ON `db_timetracker`.`collection`
FOR EACH ROW
BEGIN
    SET NEW.update_time = NOW();
END$$
DELIMITER ;

DROP TRIGGER IF EXISTS `projectUpdateTime` ;
DELIMITER $$
CREATE TRIGGER `projectUpdateTime`
BEFORE UPDATE ON `db_timetracker`.`project`
FOR EACH ROW
BEGIN
    SET NEW.update_time = NOW();
END$$
DELIMITER ;

DROP TRIGGER IF EXISTS `clockHistoryUpdateTime` ;
DELIMITER $$
CREATE TRIGGER `clockHistoryUpdateTime`
BEFORE UPDATE ON `db_timetracker`.`clockHistory`
FOR EACH ROW
BEGIN
    SET NEW.update_time = NOW();
END$$
DELIMITER ;
