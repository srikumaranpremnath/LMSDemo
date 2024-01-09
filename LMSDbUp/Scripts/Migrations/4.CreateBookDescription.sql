/* Migration Script */
CREATE TABLE [LMS.BookDescription](
BookDescriptionId UniqueIdentifier NOT NULL,
BookDetailsId UniqueIdentifier unique NOT NULL,
AuthorName varchar(50) NOT NULL,
PublicationsName varchar(50) NOT NULL,
EditionNumber tinyint NOT NULL,
CreatedBy varchar(50) NOT NULL,
CreatedDate DateTime2 NOT NULL,
EditedBy varchar(50) NULL,
EditedDate DateTime2 NULL)

GO

ALTER TABLE [LMS.BookDescription]
ADD CONSTRAINT PK_BookDescriptionId PRIMARY KEY(BookDescriptionId)

ALTER TABLE [LMS.BookDescription]
ADD CONSTRAINT FK_BookDetailsId FOREIGN KEY (BookDetailsId) REFERENCES [LMS.BookDetails](BookDetailsId) 

ALTER TABLE [LMS.BookDescription]
ADD CONSTRAINT U_BookDetailsId UNIQUE (BookDetailsId)


