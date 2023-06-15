-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema social_network
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema social_network
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `social_network` DEFAULT CHARACTER SET utf8 ;
USE `social_network` ;

-- -----------------------------------------------------
-- Table `social_network`.`user_types`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `social_network`.`user_types` ;

CREATE TABLE IF NOT EXISTS `social_network`.`user_types` (
  `user_type_id` TINYINT(1) UNSIGNED NOT NULL,
  `user_type_name` VARCHAR(45) NULL,
  PRIMARY KEY (`user_type_id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `social_network`.`users`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `social_network`.`users` ;

CREATE TABLE IF NOT EXISTS `social_network`.`users` (
  `user_id` INT UNSIGNED NOT NULL,
  `login` VARCHAR(20) NULL,
  `password` BINARY(32) NULL,
  `salt` VARCHAR(20) NULL,
  `user_type_id` TINYINT(1) UNSIGNED NULL,
  `last_active_at` DATETIME NULL,
  `created_at` DATETIME NOT NULL,
  `is_deleted` TINYINT NULL,
  `deleted_at` DATETIME NULL,
  `is_deactivated` TINYINT NULL,
  `deactivated_at` DATETIME NULL,
  `updated_at` DATETIME NULL,
  PRIMARY KEY (`user_id`),
  INDEX `FK_users_user_types_idx` (`user_type_id` ASC) VISIBLE,
  CONSTRAINT `FK_users_user_types`
    FOREIGN KEY (`user_type_id`)
    REFERENCES `social_network`.`user_types` (`user_type_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `social_network`.`chats`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `social_network`.`chats` ;

CREATE TABLE IF NOT EXISTS `social_network`.`chats` (
  `chat_id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(45) NULL,
  `created_at` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`chat_id`),
  UNIQUE INDEX `chat_id_UNIQUE` (`chat_id` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `social_network`.`chat_members_types`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `social_network`.`chat_members_types` ;

CREATE TABLE IF NOT EXISTS `social_network`.`chat_members_types` (
  `chat_members_type_id` TINYINT(1) UNSIGNED NOT NULL,
  `chat_members_type_name` VARCHAR(45) NULL,
  PRIMARY KEY (`chat_members_type_id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `social_network`.`chat_members`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `social_network`.`chat_members` ;

CREATE TABLE IF NOT EXISTS `social_network`.`chat_members` (
  `user_chat_id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `user_id` INT UNSIGNED NOT NULL,
  `chat_id` INT UNSIGNED NOT NULL,
  `chat_member_type` TINYINT(1) UNSIGNED NOT NULL,
  `created_at` DATETIME NOT NULL,
  PRIMARY KEY (`user_chat_id`),
  INDEX `FK_chat_members_chats_idx` (`chat_id` ASC) INVISIBLE,
  UNIQUE INDEX `user_chat_id_UNIQUE` (`user_chat_id` ASC) VISIBLE,
  INDEX `FK_chat_members_chat_members_types_idx` (`chat_member_type` ASC) VISIBLE,
  INDEX `FK_chat_members_users_idx` (`user_id` ASC) VISIBLE,
  CONSTRAINT `FK_chat_members_users`
    FOREIGN KEY (`user_id`)
    REFERENCES `social_network`.`users` (`user_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_chat_members_chats`
    FOREIGN KEY (`chat_id`)
    REFERENCES `social_network`.`chats` (`chat_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_chat_members_chat_members_types`
    FOREIGN KEY (`chat_member_type`)
    REFERENCES `social_network`.`chat_members_types` (`chat_members_type_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `social_network`.`messages`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `social_network`.`messages` ;

CREATE TABLE IF NOT EXISTS `social_network`.`messages` (
  `message_id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `chat_id` INT UNSIGNED NOT NULL,
  `content` TEXT(1000) NULL,
  `replied_message_id` INT UNSIGNED NULL,
  `sender_id` INT UNSIGNED NOT NULL,
  `created_at` DATETIME NOT NULL,
  `updated_at` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`message_id`),
  INDEX `FK_messages_chats_idx` (`chat_id` ASC) INVISIBLE,
  INDEX `FK_messages_messages_idx` (`replied_message_id` ASC) INVISIBLE,
  INDEX `FK_messages_chat_members_idx` (`sender_id` ASC) VISIBLE,
  UNIQUE INDEX `message_id_UNIQUE` (`message_id` ASC) VISIBLE,
  CONSTRAINT `FK_messages_chats`
    FOREIGN KEY (`chat_id`)
    REFERENCES `social_network`.`chats` (`chat_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_messages_messages`
    FOREIGN KEY (`replied_message_id`)
    REFERENCES `social_network`.`messages` (`message_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_messages_chat_members`
    FOREIGN KEY (`sender_id`)
    REFERENCES `social_network`.`chat_members` (`user_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `social_network`.`communities`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `social_network`.`communities` ;

CREATE TABLE IF NOT EXISTS `social_network`.`communities` (
  `community_id` INT UNSIGNED NOT NULL,
  `name` VARCHAR(45) NULL,
  `description` TEXT(1000) NULL,
  `is_private` TINYINT(1) NOT NULL,
  `created_at` DATETIME NOT NULL,
  `updated_at` DATETIME NOT NULL,
  PRIMARY KEY (`community_id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `social_network`.`community_member_types`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `social_network`.`community_member_types` ;

CREATE TABLE IF NOT EXISTS `social_network`.`community_member_types` (
  `community_member_type_id` TINYINT(1) UNSIGNED NOT NULL,
  `type_name` VARCHAR(45) NULL,
  PRIMARY KEY (`community_member_type_id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `social_network`.`community_members`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `social_network`.`community_members` ;

CREATE TABLE IF NOT EXISTS `social_network`.`community_members` (
  `community_member_id` INT UNSIGNED NOT NULL,
  `user_id` INT UNSIGNED NULL,
  `community_id` INT UNSIGNED NULL,
  `community_member_type_id` TINYINT(1) UNSIGNED NULL,
  `created_at` DATETIME NOT NULL,
  PRIMARY KEY (`community_member_id`),
  INDEX `FK_community_members_users_idx` (`user_id` ASC) VISIBLE,
  INDEX `FK_community_members_communities_idx` (`community_id` ASC) VISIBLE,
  INDEX `FK_community_members_community_member_types_idx` (`community_member_type_id` ASC) VISIBLE,
  CONSTRAINT `FK_community_members_users`
    FOREIGN KEY (`user_id`)
    REFERENCES `social_network`.`users` (`user_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_community_members_communities`
    FOREIGN KEY (`community_id`)
    REFERENCES `social_network`.`communities` (`community_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_community_members_community_member_types`
    FOREIGN KEY (`community_member_type_id`)
    REFERENCES `social_network`.`community_member_types` (`community_member_type_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `social_network`.`posts`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `social_network`.`posts` ;

CREATE TABLE IF NOT EXISTS `social_network`.`posts` (
  `post_id` INT UNSIGNED NOT NULL,
  `content` VARCHAR(45) NULL,
  `repost_id` INT UNSIGNED NOT NULL,
  `created_at` DATETIME NOT NULL,
  `updated_at` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`post_id`),
  INDEX `FK_posts_posts_idx` (`repost_id` ASC) VISIBLE,
  CONSTRAINT `FK_posts_posts`
    FOREIGN KEY (`repost_id`)
    REFERENCES `social_network`.`posts` (`post_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `social_network`.`community_posts`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `social_network`.`community_posts` ;

CREATE TABLE IF NOT EXISTS `social_network`.`community_posts` (
  `community_post_id` INT UNSIGNED NOT NULL,
  `community_id` INT UNSIGNED NULL,
  `post_id` INT UNSIGNED NULL,
  `proposer_id` INT UNSIGNED NULL,
  PRIMARY KEY (`community_post_id`),
  INDEX `FK_community_posts_communities_idx` (`community_id` ASC) VISIBLE,
  INDEX `FK_community_posts_posts_idx` (`post_id` ASC) INVISIBLE,
  INDEX `FK_community_posts_users_idx` (`proposer_id` ASC) INVISIBLE,
  CONSTRAINT `FK_community_posts_communities`
    FOREIGN KEY (`community_id`)
    REFERENCES `social_network`.`communities` (`community_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_community_posts_posts`
    FOREIGN KEY (`post_id`)
    REFERENCES `social_network`.`posts` (`post_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_community_posts_users`
    FOREIGN KEY (`proposer_id`)
    REFERENCES `social_network`.`users` (`user_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `social_network`.`user_profile_posts`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `social_network`.`user_profile_posts` ;

CREATE TABLE IF NOT EXISTS `social_network`.`user_profile_posts` (
  `user_post_id` INT UNSIGNED NOT NULL,
  `post_id` INT UNSIGNED NULL,
  `user_id` INT UNSIGNED NULL,
  PRIMARY KEY (`user_post_id`),
  INDEX `FK_user_profile_posts_posts_idx` (`post_id` ASC) VISIBLE,
  INDEX `FK_user_profile_posts_users_idx` (`user_id` ASC) VISIBLE,
  CONSTRAINT `FK_user_profile_posts_posts`
    FOREIGN KEY (`post_id`)
    REFERENCES `social_network`.`posts` (`post_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_user_profile_posts_users`
    FOREIGN KEY (`user_id`)
    REFERENCES `social_network`.`users` (`user_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `social_network`.`media_types`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `social_network`.`media_types` ;

CREATE TABLE IF NOT EXISTS `social_network`.`media_types` (
  `media_type_id` TINYINT(1) UNSIGNED NOT NULL,
  `media_type` VARCHAR(45) NULL,
  PRIMARY KEY (`media_type_id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `social_network`.`medias`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `social_network`.`medias` ;

CREATE TABLE IF NOT EXISTS `social_network`.`medias` (
  `media_id` INT UNSIGNED NOT NULL,
  `entity_type` VARCHAR(45) NULL,
  `file_name` VARCHAR(255) NULL,
  `file_path` VARCHAR(1024) NULL,
  `media_type` TINYINT(1) UNSIGNED NULL,
  `created_at` DATETIME NOT NULL,
  `updated_at` DATETIME NOT NULL,
  PRIMARY KEY (`media_id`),
  INDEX `FK_medias_media_types_idx` (`media_type` ASC) VISIBLE,
  CONSTRAINT `FK_medias_media_types`
    FOREIGN KEY (`media_type`)
    REFERENCES `social_network`.`media_types` (`media_type_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `social_network`.`post_medias`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `social_network`.`post_medias` ;

CREATE TABLE IF NOT EXISTS `social_network`.`post_medias` (
  `post_media_id` INT UNSIGNED NOT NULL,
  `post_id` INT UNSIGNED NULL,
  `media_id` INT UNSIGNED NULL,
  PRIMARY KEY (`post_media_id`),
  INDEX `FK_post_medias_posts_idx` (`post_id` ASC) VISIBLE,
  INDEX `FK_post_medias_medias_idx` (`media_id` ASC) VISIBLE,
  CONSTRAINT `FK_post_medias_posts`
    FOREIGN KEY (`post_id`)
    REFERENCES `social_network`.`posts` (`post_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_post_medias_medias`
    FOREIGN KEY (`media_id`)
    REFERENCES `social_network`.`medias` (`media_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `social_network`.`comments`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `social_network`.`comments` ;

CREATE TABLE IF NOT EXISTS `social_network`.`comments` (
  `comment_id` INT UNSIGNED NOT NULL,
  `author_id` INT UNSIGNED NOT NULL,
  `content` TEXT(1000) NULL,
  `post_id` INT UNSIGNED NOT NULL,
  `replied_comment` INT UNSIGNED NULL,
  `created_at` DATETIME NOT NULL,
  `updated_at` DATETIME NOT NULL,
  PRIMARY KEY (`comment_id`),
  INDEX `FK_comments_users_idx` (`author_id` ASC) INVISIBLE,
  INDEX `FK_comments_posts_idx` (`post_id` ASC) VISIBLE,
  INDEX `FK_comments_comments_idx` (`replied_comment` ASC) VISIBLE,
  CONSTRAINT `FK_comments_users`
    FOREIGN KEY (`author_id`)
    REFERENCES `social_network`.`users` (`user_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_comments_posts`
    FOREIGN KEY (`post_id`)
    REFERENCES `social_network`.`posts` (`post_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_comments_comments`
    FOREIGN KEY (`replied_comment`)
    REFERENCES `social_network`.`comments` (`comment_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `social_network`.`comment_medias`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `social_network`.`comment_medias` ;

CREATE TABLE IF NOT EXISTS `social_network`.`comment_medias` (
  `comment_media_id` INT NOT NULL,
  `comment_id` INT UNSIGNED NOT NULL,
  `media_id` INT UNSIGNED NOT NULL,
  PRIMARY KEY (`comment_media_id`),
  INDEX `FK_comment_medias_comments_idx` (`comment_id` ASC) VISIBLE,
  INDEX `FK_comment_medias_medias_idx` (`media_id` ASC) VISIBLE,
  CONSTRAINT `FK_comment_medias_comments`
    FOREIGN KEY (`comment_id`)
    REFERENCES `social_network`.`comments` (`comment_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_comment_medias_medias`
    FOREIGN KEY (`media_id`)
    REFERENCES `social_network`.`medias` (`media_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `social_network`.`friendship_types`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `social_network`.`friendship_types` ;

CREATE TABLE IF NOT EXISTS `social_network`.`friendship_types` (
  `friendship_type_id` TINYINT(1) UNSIGNED NOT NULL,
  `friendship_type` VARCHAR(45) NULL,
  PRIMARY KEY (`friendship_type_id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `social_network`.`user_friends`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `social_network`.`user_friends` ;

CREATE TABLE IF NOT EXISTS `social_network`.`user_friends` (
  `user_friend_id` INT UNSIGNED NOT NULL,
  `user_1_id` INT UNSIGNED NULL,
  `user_2_id` INT UNSIGNED NULL,
  `friendship_type` TINYINT(1) UNSIGNED NULL,
  `created_at` DATETIME NOT NULL,
  PRIMARY KEY (`user_friend_id`),
  INDEX `FK_user_friends_users_1_idx` (`user_1_id` ASC) INVISIBLE,
  INDEX `FK_user_friends_users_2_idx` (`user_2_id` ASC) INVISIBLE,
  INDEX `FK_user_friends_friendship_types_idx` (`friendship_type` ASC) VISIBLE,
  CONSTRAINT `FK_user_friends_users_1`
    FOREIGN KEY (`user_1_id`)
    REFERENCES `social_network`.`users` (`user_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_user_friends_users_2`
    FOREIGN KEY (`user_2_id`)
    REFERENCES `social_network`.`users` (`user_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_user_friends_friendship_types`
    FOREIGN KEY (`friendship_type`)
    REFERENCES `social_network`.`friendship_types` (`friendship_type_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `social_network`.`user_profiles`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `social_network`.`user_profiles` ;

CREATE TABLE IF NOT EXISTS `social_network`.`user_profiles` (
  `user_profile_id` INT UNSIGNED NOT NULL,
  `user_id` INT UNSIGNED NOT NULL,
  `user_email` VARCHAR(100) NULL,
  `user_name` VARCHAR(45) NULL,
  `user_surname` VARCHAR(45) NULL,
  `user_sex` VARCHAR(45) NULL,
  `user_country` VARCHAR(45) NULL,
  `user_education` VARCHAR(45) NULL,
  `created_at` DATETIME NOT NULL,
  `updated_at` DATETIME NOT NULL,
  PRIMARY KEY (`user_profile_id`),
  INDEX `FK_user_profiles_users_idx` (`user_id` ASC) VISIBLE,
  CONSTRAINT `FK_user_profiles_users`
    FOREIGN KEY (`user_id`)
    REFERENCES `social_network`.`users` (`user_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `social_network`.`user_profile_medias`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `social_network`.`user_profile_medias` ;

CREATE TABLE IF NOT EXISTS `social_network`.`user_profile_medias` (
  `user_profile_media_id` INT UNSIGNED NOT NULL,
  `user_profile_id` INT UNSIGNED NULL,
  `media_id` INT UNSIGNED NULL,
  PRIMARY KEY (`user_profile_media_id`),
  INDEX `FK_user_profile_medias_user_profiles_idx` (`user_profile_id` ASC) VISIBLE,
  INDEX `FK_user_profile_medias_medias_idx` (`media_id` ASC) VISIBLE,
  CONSTRAINT `FK_user_profile_medias_user_profiles`
    FOREIGN KEY (`user_profile_id`)
    REFERENCES `social_network`.`user_profiles` (`user_profile_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_user_profile_medias_medias`
    FOREIGN KEY (`media_id`)
    REFERENCES `social_network`.`medias` (`media_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `social_network`.`message_medias`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `social_network`.`message_medias` ;

CREATE TABLE IF NOT EXISTS `social_network`.`message_medias` (
  `message_media_id` INT UNSIGNED NOT NULL,
  `message_id` INT UNSIGNED NOT NULL,
  `media_id` INT UNSIGNED NOT NULL,
  `chat_id` INT UNSIGNED NOT NULL,
  PRIMARY KEY (`message_media_id`),
  INDEX `FK_message_medias_messages_idx` (`message_id` ASC) VISIBLE,
  INDEX `FK_message_medias_medias_idx` (`media_id` ASC) VISIBLE,
  INDEX `FK_message_medias_chats_idx` (`chat_id` ASC) VISIBLE,
  CONSTRAINT `FK_message_medias_messages`
    FOREIGN KEY (`message_id`)
    REFERENCES `social_network`.`messages` (`message_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_message_medias_medias`
    FOREIGN KEY (`media_id`)
    REFERENCES `social_network`.`medias` (`media_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_message_medias_chats`
    FOREIGN KEY (`chat_id`)
    REFERENCES `social_network`.`chats` (`chat_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `social_network`.`user_followers`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `social_network`.`user_followers` ;

CREATE TABLE IF NOT EXISTS `social_network`.`user_followers` (
  `user_follower_id` INT UNSIGNED NOT NULL,
  `target_id` INT UNSIGNED NULL,
  `source_id` INT UNSIGNED NULL,
  `created_at` DATETIME NOT NULL,
  PRIMARY KEY (`user_follower_id`),
  INDEX `FK_user_followers_source_users_idx` (`source_id` ASC) VISIBLE,
  INDEX `FK_user_followers_target_users_idx` (`target_id` ASC) VISIBLE,
  CONSTRAINT `FK_user_followers_source_users`
    FOREIGN KEY (`source_id`)
    REFERENCES `social_network`.`users` (`user_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_user_followers_target_users`
    FOREIGN KEY (`target_id`)
    REFERENCES `social_network`.`users` (`user_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `social_network`.`message_likes`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `social_network`.`message_likes` ;

CREATE TABLE IF NOT EXISTS `social_network`.`message_likes` (
  `message_like_id` INT NOT NULL,
  `chat_member_id` INT UNSIGNED NOT NULL,
  `message_id` INT UNSIGNED NOT NULL,
  PRIMARY KEY (`message_like_id`),
  INDEX `FK_message_likes_chat_members_idx` (`chat_member_id` ASC) INVISIBLE,
  INDEX `FK_message_likes_messages_idx` (`message_id` ASC) INVISIBLE,
  CONSTRAINT `FK_message_likes_chat_members`
    FOREIGN KEY (`chat_member_id`)
    REFERENCES `social_network`.`chat_members` (`user_chat_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_message_likes_messages`
    FOREIGN KEY (`message_id`)
    REFERENCES `social_network`.`messages` (`message_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `social_network`.`comment_likes`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `social_network`.`comment_likes` ;

CREATE TABLE IF NOT EXISTS `social_network`.`comment_likes` (
  `comment_like_id` INT NOT NULL,
  `comment_id` INT UNSIGNED NOT NULL,
  `user_id` INT UNSIGNED NOT NULL,
  PRIMARY KEY (`comment_like_id`),
  INDEX `FK_comment_likes_comments_idx` (`comment_id` ASC) INVISIBLE,
  INDEX `FK_comment_likes_users_idx` (`user_id` ASC) VISIBLE,
  CONSTRAINT `FK_comment_likes_comments`
    FOREIGN KEY (`comment_id`)
    REFERENCES `social_network`.`comments` (`comment_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_comment_likes_users`
    FOREIGN KEY (`user_id`)
    REFERENCES `social_network`.`users` (`user_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `social_network`.`post_likes`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `social_network`.`post_likes` ;

CREATE TABLE IF NOT EXISTS `social_network`.`post_likes` (
  `post_like_id` INT NOT NULL,
  `post_id` INT UNSIGNED NOT NULL,
  `user_id` INT UNSIGNED NOT NULL,
  PRIMARY KEY (`post_like_id`),
  INDEX `FK_post_likes_posts_idx` (`post_id` ASC) INVISIBLE,
  INDEX `FK_post_likes_users_idx` (`user_id` ASC) VISIBLE,
  CONSTRAINT `FK_post_likes_posts`
    FOREIGN KEY (`post_id`)
    REFERENCES `social_network`.`posts` (`post_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_post_likes_users`
    FOREIGN KEY (`user_id`)
    REFERENCES `social_network`.`users` (`user_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
