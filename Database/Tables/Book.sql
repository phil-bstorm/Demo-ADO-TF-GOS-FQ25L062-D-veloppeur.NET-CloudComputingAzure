CREATE TABLE [dbo].[Book]
(
	[BookId] INT NOT NULL IDENTITY, 
    [Title] VARCHAR(100) NOT NULL, 
    [ReleaseYear] DATETIME NULL, 
    CONSTRAINT [PK_Book] PRIMARY KEY ([BookId])
)
