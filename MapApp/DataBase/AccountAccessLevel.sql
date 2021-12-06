CREATE TABLE [dbo].[AccountAccessLevel]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [AccountId] INT NOT NULL, 
    [AccessLevelId] INT NOT NULL,
    CONSTRAINT FK_AccountAccessLevel_Account FOREIGN KEY (AccountId) REFERENCES Account(Id),
    CONSTRAINT FK_AccountAccessLevel_AccessLevel FOREIGN KEY (AccessLevelId) REFERENCES AccessLevel(Id)
)
