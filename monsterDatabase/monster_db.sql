/* check wether the database exists; if so, drop it */

IF EXISTS(SELECT 1 FROM master.dbo.sysdatabases
		WHERE name = 'monster_db')
BEGIN
	DROP DATABASE monster_db
	print '' print '*** dropping database monster_db'
END
GO

print '' print '*** creating database monster_db'
GO
CREATE DATABASE monster_db
GO

print '' print '*** using monster_db'
GO
USE [monster_db]
GO

/* AppUser table */
print '' print '*** creating AppUser table'
GO
CREATE TABLE [dbo].[AppUser] (
	[appuser_id]	[int] IDENTITY(10000, 1)	NOT NULL,
	[username]		[nvarchar](30)				NOT NULL,
	[password_hash]	[nvarchar](100)				NOT NULL DEFAULT '9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e',
	[active]		[bit]						NOT NULL DEFAULT 1,
	
	CONSTRAINT [pk_appuser_id] PRIMARY KEY([appuser_id]),
	CONSTRAINT [ak_username] UNIQUE([username])
)
GO

/* AppUser test records */
print '' print '*** creating AppUser test records'
GO
INSERT INTO [dbo].[AppUser]
		([username])
	VALUES
		('Nathan'),
		('Martin'),
		('Leo'),
		('Maria'),
		('Ahmed')
GO



/* Role table */
print '' print'*** creating role table'
GO
CREATE TABLE [dbo].[Role](
	[role_id]		[nvarchar](50)		NOT NULL,
	[description]	[nvarchar](250)		NULL,

	CONSTRAINT [pk_role_id] PRIMARY KEY ([role_id])
)
GO

/* Role test records */
print '' print '*** inserting sample role records'
GO
INSERT INTO [dbo].[Role]
		([role_id],[description])
	VALUES
		('Admin', 'administers user accounts and assigns roles'),
		('Manager', 'Manages and updates monster information')
GO

/* UserRole join table */
print '' print '*** creating UserRole table'
GO
CREATE TABLE [dbo].[UserRole](
	[appuser_id]	[int] 				NOT NULL,
	[role_id]		[nvarchar](50)		NOT NULL,
	
	CONSTRAINT [fk_UserRole_appuser_id]FOREIGN KEY ([appuser_id])
		REFERENCES [dbo].[AppUser]([appuser_id]),
	CONSTRAINT [fk_UserRole_role_id]FOREIGN KEY ([role_id])
		REFERENCES [dbo].[Role]([role_id]),
	CONSTRAINT [pk_UserRole] PRIMARY KEY ([appuser_id], [role_id])
)
GO

/* UserRole test records */
print '' print '*** inserting userRole sample data'
GO
INSERT INTO [dbo].[UserRole]
		([appuser_id], [role_id])
	VALUES
		(10000, 'Admin'),
		(10000, 'Manager'),
		(10001, 'Manager'),
		(10002, 'Manager'),
		(10003, 'Manager'),
		(10004, 'Manager')
GO

/* login related stored procedures */
print '' print '*** creating sp_authenticate_user'
GO
CREATE PROCEDURE [dbo].[sp_authenticate_user]
(
	@username		[nvarchar](30),
	@password_hash 	[nvarchar](100)
)
AS
	BEGIN
		SELECT COUNT([appuser_id]) AS 'Authenticated'
		FROM [AppUser]
		WHERE @username = [username]
			AND @password_hash = [password_hash]
			AND [active] = 1
	END
GO

/* insert new user */
print '' print '*** creating sp_insert_user'
GO
CREATE PROCEDURE [dbo].[sp_insert_user]
(
	@username		[nvarchar](30)
)
AS
	BEGIN
		INSERT INTO [dbo].[AppUser]
		([username])
		VALUES
		(@username)
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_retrieve_employee_roles'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_employee_roles]
	(
		@username		[nvarchar](30)
	)
AS	
	BEGIN
		SELECT 	[role_id]
		FROM	[UserRole] INNER JOIN [AppUser]
				ON [UserRole].[appuser_id] = [AppUser].[appuser_id]
		WHERE	[AppUser].[username] = @username
	END
GO

print '' print '*** Creating sp_insert_user_role'
GO
CREATE PROCEDURE [dbo].[sp_insert_user_role]
	(
		@appuser_id		[int],
		@role 			[nvarchar](50)
	)
AS	
	BEGIN
		INSERT INTO [dbo].[UserRole]
		([appuser_id], [role_id])
		VALUES
		(@appuser_id, @role)
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_delete_user_role'
GO
CREATE PROCEDURE [dbo].[sp_delete_user_role]
	(
		@appuser_id		[int],
		@role 			[nvarchar](50)
	)
AS	
	BEGIN
		DELETE FROM [dbo].[UserRole]
		WHERE @appuser_id = [UserRole].[appuser_id] AND
			  @role = [UserRole].[role_id]
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_select_all_roles'
GO
CREATE PROCEDURE [sp_select_all_roles]
AS
BEGIN
	SELECT [role_id]
	FROM [dbo].[Role]
	ORDER BY [role_id]
END
GO

print '' print '*** creating sp_select_user_by_username'
GO
CREATE PROCEDURE [dbo].[sp_select_user_by_username]
(
	@username		[nvarchar](30)
)
AS
	BEGIN
		SELECT [appuser_id], [username], [active]
		FROM [AppUser]
		WHERE @username = [username]
	END
GO

print '' print '*** creating sp_select_roles_by_appuser_id'
GO
	CREATE PROCEDURE [dbo].[sp_select_roles_by_appuser_id]
(
	@appuser_id		[int]
)
AS
	BEGIN
		SELECT [role_id]
		FROM [UserRole]
		WHERE @appuser_id = [appuser_id]
	END
GO

print '' print '*** creating sp_update_password_hash'
GO
CREATE PROCEDURE [dbo].[sp_update_password_hash]
(
	@appuser_id		 [int],
	@password_hash	 [nvarchar](100),
	@OldPasswordHash [nvarchar](100)
)
AS 
	BEGIN
		UPDATE [AppUser]
			SET [password_hash] = @password_hash
		WHERE @appuser_id = [appuser_id]
			AND @OldPasswordHash = [password_hash]
		RETURN @@ROWCOUNT
	END
GO

/* Monster table */
print '' print '*** creating Monster table'
GO
CREATE TABLE [dbo].[Monster] (
	[monster_id]		[int]IDENTITY(10000, 1)     NOT NULL,
	[appuser_id]		[int] 						NOT NULL,
	[monster_name]		[nvarchar](30)				NOT NULL,
	[monster_type]		[nvarchar](15)				NOT NULL,
	[active]			[bit]						NOT NULL DEFAULT 1,
	[Poison]			[int]						NOT NULL,
	[Stun]				[int]						NOT NULL,
	[Paralysis]			[int]						NOT NULL,
	[Sleep]				[int]						NOT NULL,
	[Blast]				[int]						NOT NULL,
	[Exhaust]			[int]						NOT NULL,
	[Fireblight]		[int]						NOT NULL,
	[Waterblight]		[int]						NOT NULL,
	[Thunderblight]		[int]						NOT NULL,
	[Iceblight]			[int]						NOT NULL,
	
	CONSTRAINT [pk_monster_id] PRIMARY KEY([monster_id]),
	CONSTRAINT [fk_Monster_appuser_id]FOREIGN KEY ([appuser_id])
		REFERENCES [dbo].[AppUser]([appuser_id])
)
GO

/* Monster test records */
print '' print '*** creating Monster test records'
GO
INSERT INTO [dbo].[Monster]
		([appuser_id], [monster_name], [monster_type], [Poison], [Stun], [Paralysis],		
		 [Sleep], [Blast], [Exhaust], [Fireblight], [Waterblight], [Thunderblight],	
		 [Iceblight])		
	VALUES
	/* Monster "Arzuros" */
	(10000, "Arzuros", "Fanged Beast", 3, 2, 3, 3, 2, 3, 2, 1, 1, 1),
    /* Monster "Great Izuchi" */
    (10000, "Great Izuchi", "Bird Wyvern", 3, 2, 3, 3, 2, 3, 1, 1, 2, 1),
    /* Monster "Barroth" */
    (10000, "Barroth", "Brute Wyvern", 3, 0, 3, 1, 3, 1, 2, 1, 1, 1)
GO

print '' print '*** creating sp_select_monster_by_active'
GO
CREATE PROCEDURE [dbo].[sp_select_monster_by_active]
(
	@active		bit
)
AS
	BEGIN
		SELECT [monster_id], [appuser_id], [monster_name], [monster_type], [active], [Poison], [Stun], [Paralysis],		
		 [Sleep], [Blast], [Exhaust], [Fireblight], [Waterblight], [Thunderblight],	
		 [Iceblight]
		FROM [Monster]
		WHERE @active = [active]
	END
GO

print '' print '*** creating sp_select_monster_by_id'
GO
CREATE PROCEDURE [dbo].[sp_select_monster_by_id]
(
	@id		int
)
AS
	BEGIN
		SELECT [monster_id], [appuser_id], [monster_name], [monster_type], [active], [Poison], [Stun], [Paralysis],		
		 [Sleep], [Blast], [Exhaust], [Fireblight], [Waterblight], [Thunderblight],	
		 [Iceblight]
		FROM [Monster]
		WHERE @id = [monster_id]
	END
GO

print '' print '*** creating sp_insert_monster'
GO
CREATE PROCEDURE [dbo].[sp_insert_monster]
(
	@appuser_id			int,
	@monster_name		nvarchar(30),	
	@monster_type		nvarchar(15),		
	@Poison				int,			
	@Stun				int,			
	@Paralysis			int,			
	@Sleep				int,			
	@Blast				int,			
	@Exhaust			int,			
	@Fireblight			int,			
	@Waterblight		int,			
	@Thunderblight		int,			
	@Iceblight			int			
)
AS
	BEGIN
		INSERT INTO [dbo].[Monster]
			([appuser_id], [monster_name], [monster_type], [Poison], [Stun], [Paralysis],		
			[Sleep], [Blast], [Exhaust], [Fireblight], [Waterblight], [Thunderblight],	
			[Iceblight])
		VALUES
			(@appuser_id, @monster_name, @monster_type, @Poison, @Stun, @Paralysis,
			@Sleep, @Blast, @Exhaust, @Fireblight, @Waterblight, @Thunderblight,
			@Iceblight)
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** creating sp_update_monster'
GO
CREATE PROCEDURE [dbo].[sp_update_monster]
(
	@monster_id			int,
	@monster_name		nvarchar(30),	
	@monster_type		nvarchar(15),		
	@Poison				int,			
	@Stun				int,			
	@Paralysis			int,			
	@Sleep				int,			
	@Blast				int,			
	@Exhaust			int,			
	@Fireblight			int,			
	@Waterblight		int,			
	@Thunderblight		int,			
	@Iceblight			int,
	@oldmonster_name	nvarchar(30),	
	@oldmonster_type	nvarchar(15),		
	@oldPoison			int,			
	@oldStun			int,			
	@oldParalysis		int,			
	@oldSleep			int,			
	@oldBlast			int,			
	@oldExhaust			int,			
	@oldFireblight		int,			
	@oldWaterblight		int,			
	@oldThunderblight	int,			
	@oldIceblight		int
)
AS
	BEGIN
		UPDATE [dbo].[Monster]
			SET [monster_name] = @monster_name,
				[monster_type] = @monster_type,
				[Poison] = @Poison, 
				[Stun] = @Stun, 
				[Paralysis] = @Paralysis,				
				[Sleep] = @Sleep, 
				[Blast] = @Blast, 
				[Exhaust] = @Exhaust, 
				[Fireblight] = @Fireblight, 
				[Waterblight] = @Waterblight, 
				[Thunderblight] = @Thunderblight,	
				[Iceblight] = @Iceblight
		WHERE [monster_id] = @monster_id
			AND [monster_name] = @oldmonster_name
			AND	[monster_type] = @oldmonster_type
			AND	[Poison] = @oldPoison 
			AND	[Stun] = @oldStun
			AND	[Paralysis] = @oldParalysis				
			AND	[Sleep] = @oldSleep 
			AND	[Blast] = @oldBlast
			AND	[Exhaust] = @oldExhaust
			AND	[Fireblight] = @oldFireblight
			AND	[Waterblight] = @oldWaterblight
			AND	[Thunderblight] = @oldThunderblight	
			AND	[Iceblight] = @oldIceblight
		RETURN @@ROWCOUNT
	END
GO


/* Material table */
print '' print '*** creating Material table'
GO
CREATE TABLE [dbo].[Material] (
	[material_id]		[int]IDENTITY(10000, 1)     NOT NULL,
	[monster_id]		[int] 						NOT NULL,
	[material_name]		[nvarchar](30)				NOT NULL,
	[price]				[int]						NOT NULL,
	[active]			[bit]						NOT NULL DEFAULT 1,
	
	CONSTRAINT [pk_material_id] PRIMARY KEY([material_id]),
	CONSTRAINT [fk_Material_monster_id]FOREIGN KEY ([monster_id])
		REFERENCES [dbo].[Monster]([monster_id])
)
GO

/* Material test records */
print '' print '*** creating Material test records'
GO
INSERT INTO [dbo].[Material]
		([monster_id], [material_name], [price])		
	VALUES
	-- "Arzuros" materials
	(10000, "Arzuros Pelt", 910), -- 1
    (10000, "Arzuros Carapace", 1340), -- 2
    (10000, "Arzuros Brace", 1590), -- 3
    (10000, "Beast Gem", 5100), -- 4
    -- "Great Izuchi" materials
    (10001, "Great Izuchi Hide", 870), -- 5
    (10001, "Great Izuchi Pelt", 1300), -- 6
    (10001, "Great Izuchi Tail", 1720), -- 7
    (10001, "Screamer Sac", 100), -- 8
    (10001, "Bird Wyvern Gem", 4800), -- 9
    -- "Barroth" materials
    (10002, "Barroth Tail", 660), -- 10
    (10002, "Barroth Carapace", 1230), -- 11
    (10002, "Barroth Ridge", 1830), -- 12
    (10002, "Barroth Claw", 2190), -- 13
    (10002, "Barroth Scalp", 720), -- 14
    (10002, "Wyvern Gem", 6000) -- 15
GO

print '' print '*** creating sp_select_material_by_active'
GO
CREATE PROCEDURE [dbo].[sp_select_material_by_active]
(
	@active		bit
)
AS
	BEGIN
		SELECT [material_id], [Material].[monster_id], [material_name], [price], [Material].[active], [Monster].[monster_name]
		FROM [Material] join [Monster] on [Material].[monster_id] = [Monster].[monster_id]
		WHERE @active = [Material].[active]
	END
GO

print '' print '*** creating sp_select_material_by_id'
GO
CREATE PROCEDURE [dbo].[sp_select_material_by_id]
(
	@material_id int
)
AS
	BEGIN
		SELECT [material_id], [Material].[monster_id], [material_name], [price], [Material].[active], [Monster].[monster_name]
		FROM [Material] join [Monster] on [Material].[monster_id] = [Monster].[monster_id]
		WHERE @material_id = [Material].[material_id]
	END
GO

print '' print '*** creating sp_insert_material'
GO
CREATE PROCEDURE [dbo].[sp_insert_material]
(
	@monster_id			int,
	@material_name		nvarchar(30),	
	@price				int				
)
AS
	BEGIN
		INSERT INTO [dbo].[Material]
			([monster_id], [material_name], [price])
		VALUES
			(@monster_id, @material_name, @price)
		SELECT SCOPE_IDENTITY()
	END
GO

print '' print '*** creating sp_update_material'
GO
CREATE PROCEDURE [dbo].[sp_update_material]
(
	@material_id        int,
	@monster_id			int,
	@material_name		nvarchar(30),	
	@price				int,
	@oldmonster_id		int,
	@oldmaterial_name	nvarchar(30),	
	@oldprice			int
)
AS
	BEGIN
		UPDATE [dbo].[Material]
			SET [monster_id] = @monster_id, 
				[material_name] = @material_name, 
				[price] = @price
		WHERE   [material_id] = @material_id
			AND [monster_id] = @oldmonster_id
			AND [material_name] = @oldmaterial_name
			AND [price] = @oldprice
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** creating sp_select_materials_by_monster_id'
GO
CREATE PROCEDURE [dbo].[sp_select_materials_by_monster_id]
(
	@monster_id		int
)
AS
	BEGIN
		SELECT [material_id], [monster_id], [material_name], [price], [active]
		FROM [Material]
		WHERE @monster_id = [monster_id]
	END
GO

/* DropType table */
print '' print '*** creating DropType table'
GO
CREATE TABLE [dbo].[DropType] (
	[droptype_id]		[int]IDENTITY(10000, 1)     NOT NULL,
	[droptype_name]		[nvarchar](30)				NOT NULL,
	
	CONSTRAINT [pk_droptype_id] PRIMARY KEY([droptype_id])
)
GO

/* DropType test records */
print '' print '*** creating DropType test records'
GO
INSERT INTO [dbo].[DropType]
		([droptype_name])		
	VALUES
	("Carve"),
	("Capture"),
	("Target"),
	("Dropped")
GO

/* MaterialDropRate table */
print '' print '*** creating MaterialDropRate table'
GO
CREATE TABLE [dbo].[MaterialDropRate] (
	[material_id]	[int]	      	NOT NULL,
	[droptype_id]	[int]   	  	NOT NULL,
	[droprate]		[decimal](3,2)  NOT NULL,
	
	CONSTRAINT [fk_MaterialDropRate_material_id]FOREIGN KEY ([material_id])
		REFERENCES [dbo].[Material]([material_id]),
	CONSTRAINT [fk_MaterialDropRate_droptype_id]FOREIGN KEY ([droptype_id])
		REFERENCES [dbo].[DropType]([droptype_id]),
	CONSTRAINT [pk_MaterialDropRate] PRIMARY KEY ([material_id], [droptype_id])
)
GO

/* MaterialDropRate test records */
print '' print '*** creating MaterialDropRate test records'
GO
INSERT INTO [dbo].[MaterialDropRate]
		([material_id], [droptype_id], [droprate])		
	VALUES
	-- "Arzuros" material droprates 10000-10003
	(10000, 10000, 0.52),
    (10001, 10000, 0.30),
    (10003, 10000, 0.03),
    (10001, 10001, 0.37),
    (10002, 10001, 0.22),
    (10000, 10001, 0.16),
    (10003, 10001, 0.07),
    (10000, 10002, 0.36),
    (10002, 10002, 0.21),
    (10001, 10002, 0.14),
    (10003, 10002, 0.05),
    (10000, 10003, 0.39),
    (10001, 10003, 0.40),
    (10002, 10003, 0.10),
    (10003, 10003, 0.01),
    -- "Great Izuchi" material droprates 10004-10008
    (10004, 10000, 0.44),
    (10005, 10000, 0.35),
    (10007, 10000, 0.20),
    (10008, 10000, 0.01),
    (10005, 10001, 0.39),
    (10004, 10001, 0.27),
    (10007, 10001, 0.16),
    (10006, 10001, 0.15),
    (10008, 10001, 0.03),
    (10005, 10002, 0.39),
    (10004, 10002, 0.23),
    (10007, 10002, 0.18),
    (10006, 10002, 0.05),
    (10008, 10002, 0.02),
    (10004, 10003, 0.29),
    (10005, 10003, 0.20),
    (10008, 10003, 0.01),
    -- "Barroth" material droprates 10009-10014
    (10010, 10000, 0.42), 
    (10011, 10000, 0.31),
    (10012, 10000, 0.21),
    (10013, 10000, 0.06),
    (10012, 10001, 0.32),
    (10011, 10001, 0.23),
    (10010, 10001, 0.13),
    (10013, 10001, 0.08),
    (10009, 10001, 0.08),
    (10014, 10001, 0.05),
    (10011, 10002, 0.29),
    (10012, 10002, 0.21),
    (10010, 10002, 0.15),
    (10009, 10002, 0.06),
    (10013, 10002, 0.06),
    (10014, 10002, 0.01),
    (10010, 10003, 0.50),
    (10011, 10003, 0.20),
    (10014, 10003, 0.01)
GO

print '' print '*** creating sp_select_droprates_by_materialid'
GO
CREATE PROCEDURE [dbo].[sp_select_droprates_by_materialid]
(
	@material_id	int
)
AS
	BEGIN
		SELECT [material_id], [MaterialDropRate].[droptype_id], [DropType].[droptype_name], [droprate]
		FROM [MaterialDropRate] join [DropType] on [MaterialDropRate].[droptype_id] = [DropType].[droptype_id]
		WHERE @material_id = [material_id]
	END
GO

print '' print '*** creating sp_insert_droprate'
GO
CREATE PROCEDURE [dbo].[sp_insert_droprate]
(
	@material_id	int,
	@droptype_id	int,
	@droprate		decimal(3, 2)
)
AS
	BEGIN
		INSERT INTO [dbo].[MaterialDropRate]
			([material_id], [droptype_id], [droprate])
		VALUES
			(@material_id, @droptype_id, @droprate)
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** creating sp_update_droprate'
GO
CREATE PROCEDURE [dbo].[sp_update_droprate]
(
	@material_id	int,
	@droptype_id	int,
	@droprate		decimal(3, 2),
	@oldmaterial_id	int,
	@olddroptype_id	int,
	@olddroprate	decimal(3, 2)
)
AS
	BEGIN
		UPDATE [dbo].[MaterialDropRate]
			SET [material_id] = @material_id,
				[droptype_id] = @droptype_id,
				[droprate] = @droprate
		WHERE [material_id] = @material_id
			AND [droptype_id] = @olddroptype_id
			AND [droprate] = @olddroprate
		RETURN @@ROWCOUNT
	END
GO

/* Part table */
print '' print '*** creating Part table'
GO
CREATE TABLE [dbo].[Part] (
	[part_id]		[int]IDENTITY(10000, 1)     NOT NULL,
	[monster_id]	[int] 						NOT NULL,
	[part_name]		[nvarchar](30)				NOT NULL,
	[Fire]			[int]						NOT NULL,
	[Water]			[int]						NOT NULL,
	[Thunder]		[int]						NOT NULL,
	[Ice]			[int]						NOT NULL,
	[Dragon]		[int]						NOT NULL,
	[Cut]			[int]						NOT NULL,
	[Blunt]			[int]						NOT NULL,
	[Ammo]			[int]						NOT NULL,
	
	CONSTRAINT [pk_part_id] PRIMARY KEY([part_id]),
	CONSTRAINT [fk_Part_monster_id]FOREIGN KEY ([monster_id])
		REFERENCES [dbo].[Monster]([monster_id])
)
GO

/* Part test records */
print '' print '*** creating Part test records'
GO
INSERT INTO [dbo].[Part]
		([monster_id], [part_name], [Fire], [Water], [Thunder], [Ice],		
		 [Dragon], [Cut], [Blunt], [Ammo])		
	VALUES
	-- "Arzuros" Parts
	(10000, "Head", 20, 5, 10, 15, 0, 55, 55, 55), -- 1
    (10000, "Upper Half", 25, 5, 10, 15, 0, 50, 50, 62), -- 2
    (10000, "Foreleg", 30, 5, 30, 20, 0, 33, 35, 28), -- 3
    (10000, "Abdomen", 15, 5, 10, 20, 0, 55, 55, 38), -- 4
    (10000, "Rear", 15, 5, 10, 20, 0, 66, 66, 43), -- 5
    -- "Great Izuchi" Parts
    (10001, "Head", 10, 20, 25, 10, 5, 80, 80, 75), -- 6
    (10001, "Torso", 10, 10, 15, 10, 5, 45, 45, 40), -- 7
    (10001, "Foreleg", 10, 10, 15, 10, 0, 50, 50, 55), -- 8
    (10001, "Tail", 10, 10, 20, 10, 5, 55, 50, 35), -- 9
    (10001, "Tail Tip", 15, 15, 25, 15, 10, 75, 80, 75), -- 10
    -- "Barroth" Parts
    (10002, "Head", 40, 0, 0, 20, 10, 22, 25, 15), -- 11
    (10002, "Torso", 25, 0, 0, 10, 5, 36, 45, 35), -- 12
    (10002, "Foreleg", 20, 0, 0, 10, 5, 50, 55, 50), -- 13
    (10002, "Hind Leg", 20, 0, 0, 15, 5, 36, 30, 25), -- 14
    (10002, "Tail", 25, 0, 0, 20, 10, 47, 47, 50) -- 15
GO

print '' print '*** creating sp_select_parts_by_monster_id'
GO
CREATE PROCEDURE [dbo].[sp_select_parts_by_monster_id]
(
	@monster_id		int
)
AS
	BEGIN
		SELECT [part_id], [monster_id], [part_name], [Fire], [Water], [Thunder], [Ice],		
		 [Dragon], [Cut], [Blunt], [Ammo]
		FROM [Part]
		WHERE [monster_id] = @monster_id	
	END
GO

print '' print '*** creating sp_select_parts'
GO
CREATE PROCEDURE [dbo].[sp_select_parts]
AS
	BEGIN
		SELECT [part_id], [Part].[monster_id], [Monster].[monster_name], [part_name], [Fire], [Water], [Thunder], [Ice],		
		 [Dragon], [Cut], [Blunt], [Ammo]
		FROM [Part]	join [Monster] on [Part].[monster_id] = [Monster].[monster_id]
	END
GO

print '' print '*** creating sp_select_part_by_id'
GO
CREATE PROCEDURE [dbo].[sp_select_part_by_id]
(
	@part_id int
)
AS
	BEGIN
		SELECT [part_id], [Part].[monster_id], [Monster].[monster_name], [part_name], [Fire], [Water], [Thunder], [Ice],		
		 [Dragon], [Cut], [Blunt], [Ammo]
		FROM [Part]	join [Monster] on [Part].[monster_id] = [Monster].[monster_id]
		WHERE @part_id = [part_id]
	END
GO

print '' print '*** creating sp_insert_part'
GO
CREATE PROCEDURE [dbo].[sp_insert_part]
(
	@monster_id		int,			
	@part_name		nvarchar(30),	
	@Fire			int,			
	@Water			int,			
	@Thunder		int,			
	@Ice			int,			
	@Dragon			int,			
	@Cut			int,			
	@Blunt			int,			
	@Ammo			int			
)
AS
	BEGIN
		INSERT INTO [dbo].[Part]
			([monster_id], [part_name], [Fire], [Water], [Thunder], [Ice],		
		 [Dragon], [Cut], [Blunt], [Ammo])
		VALUES
			(@monster_id, @part_name, @Fire, @Water, @Thunder, @Ice, @Dragon,
			@Cut, @Blunt, @Ammo)
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** creating sp_update_part'
GO
CREATE PROCEDURE [dbo].[sp_update_part]
(
	@part_id		int,
	@part_name		nvarchar(30),	
	@Fire			int,			
	@Water			int,			
	@Thunder		int,			
	@Ice			int,			
	@Dragon			int,			
	@Cut			int,			
	@Blunt			int,			
	@Ammo			int,
	@oldpart_name	nvarchar(30),	
	@oldFire		int,			
	@oldWater		int,			
	@oldThunder		int,			
	@oldIce			int,			
	@oldDragon		int,			
	@oldCut			int,			
	@oldBlunt		int,			
	@oldAmmo		int
)
AS
	BEGIN
		UPDATE [dbo].[Part]
			SET [part_name] = @part_name,
				[Fire] = @Fire,
				[Water] = @Water, 
				[Thunder] = @Thunder, 
				[Ice] = @Ice,				
				[Dragon] = @Dragon, 
				[Cut] = @Cut, 
				[Blunt] = @Blunt, 
				[Ammo] = @Ammo
		WHERE [part_id] = @part_id
			AND [part_name] = @oldpart_name
			AND [Fire] = @oldFire
			AND[Water] = @oldWater
			AND[Thunder] = @oldThunder
			AND[Ice] = @oldIce		
			AND[Dragon] = @oldDragon 
			AND[Cut] = @oldCut
			AND[Blunt] = @oldBlunt
			AND[Ammo] = @oldAmmo
		RETURN @@ROWCOUNT
	END
GO

/* PartMaterialDropRate table */
print '' print '*** creating PartMaterialDropRate table'
GO
CREATE TABLE [dbo].[PartMaterialDropRate] (
	[material_id]	[int]	      	NOT NULL,
	[part_id]		[int]   	  	NOT NULL,
	[droprate]		[decimal](3,2)  NOT NULL,
	
	CONSTRAINT [fk_PartMaterialDropRate_material_id]FOREIGN KEY ([material_id])
		REFERENCES [dbo].[Material]([material_id]),
	CONSTRAINT [fk_PartMaterialDropRate_part_id]FOREIGN KEY ([part_id])
		REFERENCES [dbo].[Part]([part_id]),
	CONSTRAINT [pk_PartMaterialDropRate] PRIMARY KEY ([material_id], [part_id])
)
GO

/* PartMaterialDropRate test records */
print '' print '*** creating PartMaterialDropRate test records'
GO
INSERT INTO [dbo].[PartMaterialDropRate]
		([material_id], [part_id], [droprate])		
	VALUES
	-- "Arzuros" part material droprates
	(10002, 10002, 0.80),
    (10000, 10002, 0.20),
    -- "Great Izuchi" part material droprates
    (10006, 10008, 0.80),
    (10007, 10006, 0.65),
    (10005, 10005, 0.32),
    (10004, 10008, 0.20),
    (10008, 10005, 0.03),
    -- "Barroth" part material droprates
    (10009, 10014, 0.60),
    (10010, 10014, 0.37),
    (10011, 10010, 0.27),
    (10013, 10010, 0.70),
    (10014, 10010, 0.03),
    (10014, 10014, 0.03),
    (10010, 10013, 1.00),
    (10012, 10012, 1.00)
GO

print '' print '*** creating sp_select_part_droprates_by_materialid'
GO
CREATE PROCEDURE [dbo].[sp_select_part_droprates_by_materialid]
(
	@material_id	int
)
AS
	BEGIN
		SELECT [material_id], [PartMaterialDropRate].[part_id], [Part].[part_name], [droprate]
		FROM [PartMaterialDropRate] join [Part] on [PartMaterialDropRate].[part_id] = [Part].[part_id]
		WHERE @material_id = [material_id]
	END
GO

print '' print '*** creating sp_insert_part_droprate'
GO
CREATE PROCEDURE [dbo].[sp_insert_part_droprate]
(
	@material_id	int,
	@part_id 		int,
	@droprate       [decimal](3,2)
)
AS
	BEGIN
		INSERT INTO [dbo].[PartMaterialDropRate]
			([material_id],[part_id],[droprate])
		VALUES
			(@material_id, @part_id, @droprate)
		RETURN @@ROWCOUNT
	END
GO

/* Terrain table */
print '' print '*** creating Terrain table'
GO
CREATE TABLE [dbo].[Terrain] (
	[terrain_id]		[int]IDENTITY(10000, 1)     NOT NULL,
	[terrain_name]		[nvarchar](30)				NOT NULL,
	
	CONSTRAINT [pk_terrain_id] PRIMARY KEY([terrain_id])
)
GO

/* Terrain test records */
print '' print '*** creating Terrain test records'
GO
INSERT INTO [dbo].[Terrain]
		([terrain_name])		
	VALUES
	("Shrine Ruins"), --1
	("Frost Islands"), --2
	("Sandy Plains"), --3
	("Flooded Forest"), --4
	("Lava Caverns"), --5
	("Citadel"), --6
	("Jungle") --7
GO



/* Area table */
print '' print '*** creating Area table'
GO
CREATE TABLE [dbo].[Area] (
	[monster_id]	[int]	      	NOT NULL,
	[terrain_id]	[int]   	  	NOT NULL,
	
	CONSTRAINT [fk_Area_monster_id]FOREIGN KEY ([monster_id])
		REFERENCES [dbo].[Monster]([monster_id]),
	CONSTRAINT [fk_Area_terrain_id]FOREIGN KEY ([terrain_id])
		REFERENCES [dbo].[Terrain]([terrain_id]),
	CONSTRAINT [pk_Area] PRIMARY KEY ([monster_id], [terrain_id])
)
GO

/* Area test records */
print '' print '*** creating Area test records'
GO
INSERT INTO [dbo].[Area]
		([monster_id], [terrain_id])		
	VALUES
	-- "Arzuros" Areas
	(10000, 10000),
    (10000, 10003),
	(10000, 10005),
    -- "Great Izuchi" Areas
    (10001, 10000),
    (10001, 10001),
    -- "Barroth" Areas
    (10002, 10002)
GO

print '' print '*** creating sp_select_terrain_by_monster_id'
GO
CREATE PROCEDURE [dbo].[sp_select_terrain_by_monster_id]
(
	@monster_id		int
)
AS
	BEGIN
		SELECT [monster_id], [Area].[terrain_id], [Terrain].[terrain_name]
		FROM [Area] join [Terrain] on [Terrain].[terrain_id] = [Area].[terrain_id] 
		WHERE [monster_id] = @monster_id	
	END
GO

print '' print '*** creating sp_select_terrains'
GO
CREATE PROCEDURE [dbo].[sp_select_terrains]
AS
	BEGIN
		SELECT [terrain_id], [terrain_name]
		FROM [Terrain] 
	END
GO

print '' print '*** creating sp_select_terrain_by_id'
GO
CREATE PROCEDURE [dbo].[sp_select_terrain_by_id]
(
	@terrain_id int
)
AS
	BEGIN
		SELECT [terrain_id], [terrain_name]
		FROM [Terrain]
		WHERE @terrain_id = [terrain_id]
	END
GO

print '' print '*** creating sp_insert_terrain'
GO
CREATE PROCEDURE [dbo].[sp_insert_terrain]
(
	@terrain_name	nvarchar(30)
)
AS
	BEGIN
		INSERT INTO [dbo].[Terrain]
			([terrain_name])
		VALUES
			(@terrain_name)
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** sp_insert_area_by_monster_id_and_terrain_id'
GO
CREATE PROCEDURE [dbo].[sp_insert_area_by_monster_id_and_terrain_id]
(
	@monster_id		int,
	@terrain_id		int
)
AS
	BEGIN
		INSERT INTO [dbo].[Area]
			([monster_id], [terrain_id])
		VALUES
			(@monster_id, @terrain_id)
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** sp_delete_area_by_monster_id_and_terrain_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_area_by_monster_id_and_terrain_id]
(
	@monster_id		int,
	@terrain_id		int
)
AS
	BEGIN
		DELETE FROM [dbo].[Area]
		WHERE @monster_id = [Area].[monster_id] AND
			  @terrain_id = [Area].[terrain_id]
		RETURN @@ROWCOUNT
	END
GO