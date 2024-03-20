CREATE TABLE [dbo].[tErrorLog]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [ErrNumber] INT NULL, 
    [ErrState] INT NULL, 
    [ErrProc] VARCHAR(128) NULL, 
    [ErrLine] INT NULL, 
    [ErrMessage] VARCHAR(MAX) NULL, 
    [Dtm] DATETIME2 NOT NULL DEFAULT getdate()
)
