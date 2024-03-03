# Time Tracker

It is a system of control of the working day, created as a personal project, which allows you to save the time of entry and exit of each working day and add how much time you have used in a project.


## Technologies used

### Client
The client side has been developed using technologies such as React, TypeScript and JSX. Among the most used packages are Redux for application state management, React Router DOM v6 for navigation between pages, and Axios for HTTP requests. In addition, styles have been created using CSS, although it is intended to enhance them by using Tailwind CSS for more efficient and scalable style management.

### Server

On the server side, an API has been developed using ASP.NET Core Web API. An authentication system based on JWT tokens (JSON Web Token) has been implemented to ensure the security of communications between the client and the server. In addition, MySQL has been used as a database to store and manage the application data.

## How to edit

This project uses Docker The easiest way to run it is to open the terminal in the project folder and type `docker-compose up` or open the visual studio solution and execute with docker.

This should create 4 containers 
- phpMyAdmin
- mysql
- API Project
- Front Project

To improve the development flow and take advantage of fast-reload's fluidity, I usually disable the frontend containerizer to use it directly with `npm run dev`.
