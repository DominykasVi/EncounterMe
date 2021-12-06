CREATE TABLE [dbo].AccountDownvotes
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [AccountId] INT NOT NULL, 
    [LocationId] INT NOT NULL, 
    CONSTRAINT FK_AccountDownvotes_Account FOREIGN KEY (AccountId) REFERENCES Account(Id),
    CONSTRAINT FK_AccountDownvotes_Location FOREIGN KEY (LocationId) REFERENCES Location(Id),
)
