# MonsterHunterApp
Final Project work for .NET2 and .NET3

## Summary
This was my final project for .NET2 and .NET3 made using Microsoft Visual Studio. This project is an application
that stores and shows data on monsters from the Monster Hunter Series (mainly Rise/Sunbreak).
My .NET2 consisted of creating the database (Microsoft TSQL) and the WPF project presentation layer, logic layer,
data access layer, and objects. My work for .NET3 is the MVC presentation layer as well as using
AspNet integration for login, role access and migrations.

## .NET2
(Set the PresentationLayer as the startup project)
1. Allows for a general user to scroll through a list of monsters or materials.
2. Clicking on the monster or material allows the user to look at more detailed information.
3. A user with login access is able to create monsters, materials, parts, and terrain.
4. A user with login access can also update monsters, materials, and parts as well as assign terrain to a monster.

## .NET3
(Set the MVCPresentationLayer as the startup project)
1. Admin user and create new users and can add or remove roles to users.
2. New user can register and create an account
3. Features from the .NET2 project still work.
4. A user with manager or admin access can create droprates for a material

