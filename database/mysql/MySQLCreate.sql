
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

-- -----------------------------------------------------
-- Table `TimeTracker`.`colection`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `TimeTracker`.`colection` ;

CREATE TABLE IF NOT EXISTS `TimeTracker`.`colection` (
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
  `colection_id` INT NOT NULL,
  `create_time` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `update_time` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`, `colection_id`),
	CONSTRAINT `fk_project_colection` FOREIGN KEY (`colection_id`) REFERENCES `TimeTracker`.`colection` (`id`)
  );

-- -----------------------------------------------------
-- Table `TimeTracker`.`time_clock`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `TimeTracker`.`time_clock` ;

CREATE TABLE IF NOT EXISTS `TimeTracker`.`time_clock` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `user_id` INT NOT NULL,
  `start_time` DATETIME NOT NULL,
  `finish_time` DATETIME NULL,
  `isFinish` BIT NOT NULL DEFAULT 0,
  `old_start_time` DATETIME NOT NULL,
  `old_finish_time` DATETIME NULL,
  PRIMARY KEY (`id`, `user_id`),
  CONSTRAINT `fk_time_clock_user` FOREIGN KEY (`user_id`) REFERENCES `TimeTracker`.`user` (`id`)
  );


-- -----------------------------------------------------
-- Table `TimeTracker`.`user_has_project`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `TimeTracker`.`user_has_project` ;

CREATE TABLE IF NOT EXISTS `TimeTracker`.`user_has_project` (
  `user_id` INT NOT NULL,
  `project_id` INT NOT NULL,
  `create_time` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `update_time` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`user_id`, `project_id`),
  CONSTRAINT `fk_user_has_project_user` FOREIGN KEY (`user_id`) REFERENCES `TimeTracker`.`user` (`id`),
  CONSTRAINT `fk_user_has_project_project` FOREIGN KEY (`user_id`) REFERENCES `TimeTracker`.`project` (`id`)
  );

