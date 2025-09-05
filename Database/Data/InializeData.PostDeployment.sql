SET IDENTITY_INSERT [Book] ON;

-- BOOKS
INSERT INTO [Book] (BookId, Title, ReleaseYear)
	VALUES (1, 'Ça', '1986-09-16'),
	    (2, 'Fondation', '1957-01-24'),
	    (3, 'La Ligne verte', '1996-02-18' ),
	    (4, 'Le Guide du voyageur galactique', '1978-04-08'),
	    (5, 'Le Roi de fer', '1955-07-07'),
	    (6, 'Les robots', '2026-08-22');

SET IDENTITY_INSERT [Book] OFF;

-- AUTHORS
SET IDENTITY_INSERT [Author] ON;
INSERT INTO [Author] (AuthorId, Firstname, Lastname, BirthDate)
	VALUES (1, 'Stephen', 'King', '1947-09-21'),
	    (2, 'Isaac', 'Asimov', '1920-01-02'),
		(3, 'Douglas', 'Adams', '1952-03-11'),
		(4, 'Maurice', 'Druon', '1918-04-23');

SET IDENTITY_INSERT [Author] OFF;

-- Auteur Book
INSERT INTO [MM_Book_Author] ([AuthorId], [BookId])
 VALUES (1, 1),
		(1, 3),
		(2, 2),
		(2, 6),
		(3, 4),
		(4, 5);