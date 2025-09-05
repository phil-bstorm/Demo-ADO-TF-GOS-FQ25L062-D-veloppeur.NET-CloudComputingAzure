CREATE TABLE [dbo].[MM_Book_Author]
(
	[AuthorId] INT NOT NULL, 
    [BookId] INT NOT NULL,
	CONSTRAINT [FK_MM_Author] FOREIGN KEY ([AuthorId]) REFERENCES Author([AuthorId]), 
    CONSTRAINT [FK_MM_Book] FOREIGN KEY ([BookId]) REFERENCES [Book]([BookId]) 
)
