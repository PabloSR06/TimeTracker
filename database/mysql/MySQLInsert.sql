INSERT INTO `user` (`id`, `name`, `email`, `password`, `create_time`, `update_time`) VALUES (NULL, 'Pablo', 'test@gmail.com', '1234', current_timestamp(), current_timestamp());

INSERT INTO `collection` (`id`, `name`, `create_time`, `update_time`) VALUES (NULL, 'test1', current_timestamp(), current_timestamp()), (NULL, 'test2', current_timestamp(), current_timestamp()), (NULL, 'test3', current_timestamp(), current_timestamp());

INSERT INTO `project` (`id`, `name`, `collection_id`,`create_time`, `update_time`) VALUES (NULL, 'test13',1,  current_timestamp(), current_timestamp()), (NULL, 'test243',1, current_timestamp(), current_timestamp()), (NULL, 'test34324',2, current_timestamp(), current_timestamp());

INSERT INTO `userHasCollection` (`user_id`, `collection_id`, `create_time`, `update_time`) VALUES 
('2', '1', current_timestamp(), current_timestamp()), 
('2', '2', current_timestamp(), current_timestamp());