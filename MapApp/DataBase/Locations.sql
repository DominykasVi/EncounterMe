﻿CREATE TABLE [dbo].Location
(
	[Id] INT NOT NULL PRIMARY KEY DEFAULT 0000, 
    [Name] NVARCHAR(50) NOT NULL , 
    [Latitude] FLOAT NOT NULL DEFAULT 54.729866061364845, 
    [Longitude] FLOAT NOT NULL DEFAULT 25.263373943798094
)
