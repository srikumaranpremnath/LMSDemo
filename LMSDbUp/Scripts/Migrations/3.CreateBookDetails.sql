/* Migration Script */
CREATE Table [LMS.BookDetails](
BookDetailsId UniqueIdentifier NOT NULL,
BookCode varchar(10) NOT NULL,
BookName varchar(100) NOT NULL,
RackId int NOT NULL,
RowId int NOT NULL,
StatusId int NOT NULL,
IsDeleted bit NULL,
CreatedBy varchar(50) NOT NULL,
CreatedDate DateTime2 NOT NULL,
EditedBy varchar(50) NULL,
EditedDate DateTime2 NULL)

GO

ALTER TABLE [LMS.BookDetails]
ADD CONSTRAINT PK_BookDetailsId PRIMARY KEY(BookDetailsId)

ALTER TABLE [LMS.BookDetails]
ADD CONSTRAINT U_BookCode UNIQUE (BookCode)

GO
