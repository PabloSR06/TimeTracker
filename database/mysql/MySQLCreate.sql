
-- -----------------------------------------------------
-- Schema db_timetracker
-- -----------------------------------------------------
DROP SCHEMA IF EXISTS `db_timetracker` ;

-- -----------------------------------------------------
-- Schema db_timetracker
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `db_timetracker` DEFAULT CHARACTER SET utf8 ;

USE `db_timetracker` ;

-- -----------------------------------------------------
-- Table `db_timetracker`.`user`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `db_timetracker`.`user` ;

CREATE TABLE IF NOT EXISTS `db_timetracker`.`user` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(45) NOT NULL,
  `email` VARCHAR(255) UNIQUE,
  `password` VARCHAR(500) NOT NULL,
  `create_time` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `update_time` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
  );

INSERT INTO `user` (`id`,`name`, `password`) VALUES (1, 'SuperUser', '8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918');


-- -----------------------------------------------------
-- Table `db_timetracker`.`collection`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `db_timetracker`.`collection` ;

CREATE TABLE IF NOT EXISTS `db_timetracker`.`collection` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(45) NOT NULL,
  `create_time` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `update_time` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
  );




-- -----------------------------------------------------
-- Table `db_timetracker`.`project`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `db_timetracker`.`project` ;

CREATE TABLE IF NOT EXISTS `db_timetracker`.`project` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(45) NOT NULL,
  `collection_id` INT NOT NULL,
  `create_time` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `update_time` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`, `collection_id`),
	CONSTRAINT `fk_project_collection` FOREIGN KEY (`collection_id`) REFERENCES `db_timetracker`.`collection` (`id`)
  );

-- -----------------------------------------------------
-- Table `db_timetracker`.`time_clock`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `db_timetracker`.`time_clock` ;

CREATE TABLE IF NOT EXISTS `db_timetracker`.`time_clock` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `user_id` INT NOT NULL,
  `start_time` TIME NOT NULL,
  `finish_time` TIME NULL,
  `isFinish` BIT NOT NULL DEFAULT 0,
  `old_start_time` TIME NOT NULL,
  `old_finish_time` TIME NULL,
  `date` DATE NOT NULL,
  PRIMARY KEY (`id`, `user_id`),
  CONSTRAINT `fk_time_clock_user` FOREIGN KEY (`user_id`) REFERENCES `db_timetracker`.`user` (`id`)
  );


-- -----------------------------------------------------
-- Table `db_timetracker`.`userHasProject`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `db_timetracker`.`userHasProject` ;

CREATE TABLE IF NOT EXISTS `db_timetracker`.`userHasProject` (
  `user_id` INT NOT NULL,
  `project_id` INT NOT NULL,
  PRIMARY KEY (`user_id`, `project_id`),
  CONSTRAINT `fk_userHasProject_group` FOREIGN KEY (`user_id`) REFERENCES `db_timetracker`.`user` (`id`),
  CONSTRAINT `fk_userHasProject_project` FOREIGN KEY (`project_id`) REFERENCES `db_timetracker`.`project` (`id`)
  );

DELIMITER $$
CREATE TRIGGER `addAdminUserProject`
AFTER INSERT ON `db_timetracker`.`project`
FOR EACH ROW
BEGIN
    INSERT INTO userHasProject (`user_id`, `project_id`) VALUES (1, NEW.id);
END$$
DELIMITER ;

-- -----------------------------------------------------
-- Table `db_timetracker`.`userHasCollection`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `db_timetracker`.`userHasCollection` ;

CREATE TABLE IF NOT EXISTS `db_timetracker`.`userHasCollection` (
  `user_id` INT NOT NULL,
  `collection_id` INT NOT NULL,
  PRIMARY KEY (`user_id`, `collection_id`),
  CONSTRAINT `fk_userHasCollection_group` FOREIGN KEY (`user_id`) REFERENCES `db_timetracker`.`user` (`id`),
  CONSTRAINT `fk_userHasCollection_collection` FOREIGN KEY (`collection_id`) REFERENCES `db_timetracker`.`collection` (`id`)
  );

DELIMITER $$
CREATE TRIGGER `addAdminUser`
AFTER INSERT ON `db_timetracker`.`collection`
FOR EACH ROW
BEGIN
    INSERT INTO userHasCollection (`user_id`, `collection_id`) VALUES (1, NEW.id);
END$$
DELIMITER ;

-- -----------------------------------------------------
-- Table `db_timetracker`.`clockHistory`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `db_timetracker`.`clockHistory` ;

CREATE TABLE IF NOT EXISTS `db_timetracker`.`clockHistory` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `project_id` INT NOT NULL,
  `timeClock_id` INT NOT NULL,
  `minutes` INT NOT NULL,
  `description` VARCHAR(255),
  `create_time` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `update_time` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  CONSTRAINT `fk_clockHistory_project` FOREIGN KEY (`project_id`) REFERENCES `db_timetracker`.`project` (`id`),
  CONSTRAINT `fk_clockHistory_timeClock` FOREIGN KEY (`timeClock_id`) REFERENCES `db_timetracker`.`time_clock` (`id`)

  );

