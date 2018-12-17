CREATE TABLE [dbo].[Rota]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Engineer_Id] INT NOT NULL, 
    [Shift] DATETIME NOT NULL, 
    CONSTRAINT [FK_Rota_ToTable] FOREIGN KEY ([Engineer_Id]) REFERENCES [Engineers]([Id]) 
)
