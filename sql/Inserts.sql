USE timeTracker;

INSERT INTO roles (name) VALUES ('Admin'), ('User');

INSERT INTO Users (Name, Email, Password) 
VALUES ('John Doe', 'john@example.com', 'password123'),
       ('Jane Smith', 'jane@example.com', 'securepwd'),
       ('Jane Smith2', 'bob@example.com', 'securepwd');


INSERT INTO user_roles (user_id, role_id)
VALUES 
    ((SELECT id FROM users WHERE email='john@example.com'), (SELECT id FROM roles WHERE name='Admin')),
    ((SELECT id FROM users WHERE email='bob@example.com'), (SELECT id FROM roles WHERE name='User')),
    ((SELECT id FROM users WHERE email='john@example.com'), (SELECT id FROM roles WHERE name='User'));


INSERT INTO Clients (Name) 
VALUES ('Client X'),
       ('Client Y');
       
INSERT INTO Projects (Name, Description, ClientID)
VALUES 
    ('Project 1', 'Description for Project 1', 1),
    ('Project 2', 'Description for Project 2', 1),
    ('Project 3', 'Description for Project 3', 1),
    ('Project A', 'Description for Project A', 1),
	('Project B', 'Description for Project B', 1);
    



INSERT INTO DayHours (UserID, Type, Date) 
VALUES (1, 1, '2024-01-24 08:00:00'),
       (1, 0, '2024-01-24 16:00:00'), 
       (1, 1, '2024-01-25 08:00:00'),
       (1, 0, '2024-01-25 16:00:00'),
       (1, 1, '2024-01-26 08:00:00');

INSERT INTO ProjectHours (UserID, ProjectID, Minutes, Date) 
VALUES (1, 1, 120, '2024-02-09 16:00:00'),
       (2, 2, 180, '2024-02-09 16:00:00');