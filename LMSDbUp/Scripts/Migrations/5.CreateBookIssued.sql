/* Migration Script */
CREATE Table [LMS.BookIssued](
BookIssuedId UniqueIdentifier NOT NULL,
BookDetailsId UniqueIdentifier NOT NULL,
UserDetailsId UniqueIdentifier NOT NULL,
IssuedDate DateTime2 NOT NULL,
ExpectedReturnDate DateTime NOT NULL,
RenewedDate	DateTime2 NULL,
ReturnedDate DateTime2 NULL,
Penality int NULL,
PenalityStatusId int NULL,
PenalityPaidDate DateTime2 NULL,
StatusId int NOT NULL,
CreatedBy varchar(50) NOT NULL,
CreatedDate DateTime2 NOT NULL,
EditedBy varchar(50) NULL,
EditedDate DateTime2 NULL)

GO

ALTER TABLE [LMS.BookIssued]
ADD CONSTRAINT PK_BookIssued PRIMARY KEY(BookIssuedId)

ALTER TABLE [LMS.BookIssued]
ADD CONSTRAINT FK_BookDetails_BookIssueId FOREIGN KEY (BookDetailsId) REFERENCES [LMS.BookDetails](BookDetailsId)
