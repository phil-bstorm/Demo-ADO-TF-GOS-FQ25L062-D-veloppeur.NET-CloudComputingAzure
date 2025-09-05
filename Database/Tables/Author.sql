CREATE TABLE [dbo].[Author]
(
	[AuthorId] INT NOT NULL IDENTITY, 
    [Firstname] VARCHAR(60) NOT NULL,
	[Lastname] VARCHAR(50) NOT NULL,
	[BirthDate] DATETIME NULL, 
    CONSTRAINT [PK_Author] PRIMARY KEY ([AuthorId]) 
)
