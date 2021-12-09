CREATE TABLE [dbo].Account
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Username] VARCHAR(50) NOT NULL, 
    [Email] VARCHAR(50) NULL, 
    [Password] VARBINARY(MAX) NOT NULL 
)
