CREATE TABLE [dbo].AccountUpvotes
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [AccountId] INT NOT NULL, 
    [LocationId] INT NOT NULL,
    CONSTRAINT FK_AccountUpvotes_Account FOREIGN KEY (AccountId) REFERENCES Account(Id),
    CONSTRAINT FK_AccountUpvotes_Location FOREIGN KEY (LocationId) REFERENCES Location(Id)
)