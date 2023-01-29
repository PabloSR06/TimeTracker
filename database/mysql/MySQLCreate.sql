
-- -----------------------------------------------------
-- Schema TimeTracker
-- -----------------------------------------------------
DROP SCHEMA IF EXISTS `TimeTracker` ;

-- -----------------------------------------------------
-- Schema TimeTracker
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `TimeTracker` DEFAULT CHARACTER SET utf8 ;

USE `TimeTracker` ;

-- -----------------------------------------------------
-- Table `TimeTracker`.`user`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `TimeTracker`.`user` ;

CREATE TABLE IF NOT EXISTS `TimeTracker`.`user` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(45) NOT NULL,
  `email` VARCHAR(255) NOT NULL,
  `password` VARCHAR(500) NOT NULL,
  `create_time` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `update_time` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
  );

INSERT INTO `user` (`id`,`name`) VALUES (1, 'SuperUser');


-- -----------------------------------------------------
-- Table `TimeTracker`.`collection`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `TimeTracker`.`collection` ;

CREATE TABLE IF NOT EXISTS `TimeTracker`.`collection` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(45) NOT NULL,
  `create_time` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `update_time` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
  );




-- -----------------------------------------------------
-- Table `TimeTracker`.`project`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `TimeTracker`.`project` ;

CREATE TABLE IF NOT EXISTS `TimeTracker`.`project` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(45) NOT NULL,
  `collection_id` INT NOT NULL,
  `create_time` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `update_time` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`, `collection_id`),
	CONSTRAINT `fk_project_collection` FOREIGN KEY (`collection_id`) REFERENCES `TimeTracker`.`collection` (`id`)
  );

-- -----------------------------------------------------
-- Table `TimeTracker`.`time_clock`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `TimeTracker`.`time_clock` ;

CREATE TABLE IF NOT EXISTS `TimeTracker`.`time_clock` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `user_id` INT NOT NULL,
  `start_time` TIME NOT NULL,
  `finish_time` TIME NULL,
  `isFinish` BIT NOT NULL DEFAULT 0,
  `old_start_time` TIME NOT NULL,
  `old_finish_time` TIME NULL,
  `date` DATE NOT NULL,
  PRIMARY KEY (`id`, `user_id`),
  CONSTRAINT `fk_time_clock_user` FOREIGN KEY (`user_id`) REFERENCES `TimeTracker`.`user` (`id`)
  );


-- -----------------------------------------------------
-- Table `TimeTracker`.`userHasProject`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `TimeTracker`.`userHasProject` ;

CREATE TABLE IF NOT EXISTS `TimeTracker`.`userHasProject` (
  `user_id` INT NOT NULL,
  `project_id` INT NOT NULL,
  PRIMARY KEY (`user_id`, `project_id`),
  CONSTRAINT `fk_userHasProject_group` FOREIGN KEY (`user_id`) REFERENCES `TimeTracker`.`user` (`id`),
  CONSTRAINT `fk_userHasProject_project` FOREIGN KEY (`project_id`) REFERENCES `TimeTracker`.`project` (`id`)
  );

DELIMITER $$
CREATE TRIGGER `addAdminUserProject`
AFTER INSERT ON `TimeTracker`.`project`
FOR EACH ROW
BEGIN
    INSERT INTO userHasProject (`user_id`, `project_id`) VALUES (1, NEW.id);
END$$
DELIMITER ;

-- -----------------------------------------------------
-- Table `TimeTracker`.`userHasCollection`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `TimeTracker`.`userHasCollection` ;

CREATE TABLE IF NOT EXISTS `TimeTracker`.`userHasCollection` (
  `user_id` INT NOT NULL,
  `collection_id` INT NOT NULL,
  PRIMARY KEY (`user_id`, `collection_id`),
  CONSTRAINT `fk_userHasCollection_group` FOREIGN KEY (`user_id`) REFERENCES `TimeTracker`.`user` (`id`),
  CONSTRAINT `fk_userHasCollection_collection` FOREIGN KEY (`collection_id`) REFERENCES `TimeTracker`.`collection` (`id`)
  );

DELIMITER $$
CREATE TRIGGER `addAdminUser`
AFTER INSERT ON `TimeTracker`.`collection`
FOR EACH ROW
BEGIN
    INSERT INTO userHasCollection (`user_id`, `collection_id`) VALUES (1, NEW.id);
END$$
DELIMITER ;

-- -----------------------------------------------------
-- Table `TimeTracker`.`clockHistory`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `TimeTracker`.`clockHistory` ;

CREATE TABLE IF NOT EXISTS `TimeTracker`.`clockHistory` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `project_id` INT NOT NULL,
  `timeClock_id` INT NOT NULL,
  `minutes` INT NOT NULL,
  `description` VARCHAR(255),
  `create_time` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `update_time` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  CONSTRAINT `fk_clockHistory_project` FOREIGN KEY (`project_id`) REFERENCES `TimeTracker`.`project` (`id`),
  CONSTRAINT `fk_clockHistory_timeClock` FOREIGN KEY (`timeClock_id`) REFERENCES `TimeTracker`.`time_clock` (`id`)

  );

INSERT INTO `clockHistory` (`project_id`, `timeClock_id`, `minutes`, `description`) 
VALUES (@project_id, @timeClock_id, @minutes, @description);
